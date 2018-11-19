using Cat.Foundation;
using Cat.M.Public.Models.ModelBinder.ReturnModels.Wechat;
using Cat.M.Public.Models.ModelBinder.ReturnModels.WechatAnalysis;
using Cat.M.Public.Models.Table;
using Cat.M.Public.Services.Helper;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Text;

using AllCacheServices = Cat.M.Cache.Services.AllServices;

namespace Cat.M.Public.Services
{
    /// <summary>
    /// 小程序相关接口 服务类
    /// </summary>
    public class MiniProgramService
    {
        private const string CACHE_ACCESSTOKEN_KEY = "cat.m.public.services.miniprogramservice.access_token";
        private Wechat_App_Config WechatAppConfig => AllServices.WechatAppConfigService.GetSingle(Enums.Wechat.AppKey.喵喵看书);


        /// <summary>
        /// 获取小程序 access_token （access_token缓存一小时）
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken()
        {
            string access_token;

            if (Cat.Foundation.CatContext.HttpContext.IsLocalHostRequest())
            {
                //如果是本地调试，则从线上的接口中获取 access_token，避免本地调试后线上的access_token过期
                var token = ApiHelper.AuthToken;
                string url = $"http://book.somethingwhat.com/Api/MiniProgram/GetAccessToken?token={token}";
                var res = HttpHelper.Get(url);
                ActionRes actionRes = Serializer.JsonDeserialize<ActionRes>(res);

                if (actionRes.Code < 0) throw new Exception(actionRes.Msg);

                access_token = actionRes.Data.ToString();
            }
            else
            {
                if (AllCacheServices.CacheService.Exists(CACHE_ACCESSTOKEN_KEY))
                {
                    access_token = AllCacheServices.CacheService.GetCache(CACHE_ACCESSTOKEN_KEY).ToString();
                }
                else
                {
                    string url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={WechatAppConfig.Appid}&secret={WechatAppConfig.Secret}";
                    var res = HttpHelper.Get(url);

                    CheckResponse(res);

                    var instance = Serializer.JsonDeserialize<AccessToken>(res);
                    access_token = instance.access_token;

                    AllCacheServices.CacheService.SetCache(CACHE_ACCESSTOKEN_KEY, access_token);
                }
            }

            return access_token;
        }

        /// <summary>
        /// 检查响应的数据（调用小程序接口是否返回异常）
        /// </summary>
        /// <param name="res"></param>
        public void CheckResponse(string res)
        {
            if (res.Contains("errcode"))
            {
                var errorRequest = Serializer.JsonDeserialize<ErrorRequest>(res);
                throw new Exception($"接口返回异常，errcode：{errorRequest.errcode}，errmsg：{errorRequest.errmsg}");
            }
        }

        #region tAnalysis

        private string GetBody(string begin_date, string end_date)
        {
            string body = "{\"begin_date\":\"" + begin_date + "\",\"end_date\":\"" + end_date + "\"}";
            return body;
        }

        /// <summary>
        /// 获取用户访问小程序数据周趋势
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">开始日期，为周一日期。格式为 yyyymmdd</param>
        /// <param name="end_date">结束日期，为周日日期，限定查询一周数据。格式为 yyyymmdd</param>
        /// <returns></returns>
        public WeeklyVisitTrend GetAnalysisWeeklyVisitTrend(string begin_date, string end_date)
        {
            string access_token = GetAccessToken();
            string url = $"https://api.weixin.qq.com/datacube/getweanalysisappidweeklyvisittrend?access_token={access_token}";
            string body = GetBody(begin_date, end_date);

            var res = HttpHelper.Post(url, body);

            //系统错误的话就直接返回“初始”数据
            if (res.Contains("\"errcode\":-1"))
            {
                return new WeeklyVisitTrend() { list = new List<DailyVisitTrend.TempModel>() };
            }

            CheckResponse(res);

            var instance = Serializer.JsonDeserialize<WeeklyVisitTrend>(res);
            return instance;
        }

        /// <summary>
        /// 获取用户访问小程序数据概况
        /// </summary>
        /// <param name="begin_date">开始日期。格式为 yyyymmdd</param>
        /// <param name="end_date">结束日期，限定查询1天数据，允许设置的最大值为昨日。格式为 yyyymmdd</param>
        /// <returns></returns>
        public DailySummary GetAnalysisDailySummary(string begin_date, string end_date)
        {
            string access_token = GetAccessToken();
            string url = $"https://api.weixin.qq.com/datacube/getweanalysisappiddailysummarytrend?access_token={access_token}";
            string body = GetBody(begin_date, end_date);

            var res = HttpHelper.Post(url, body);

            //系统错误的话就直接返回“初始”数据
            if (res.Contains("\"errcode\":-1"))
            {
                return new DailySummary() { list = new List<DailySummary.TempModel>() };
            }

            CheckResponse(res);

            var instance = Serializer.JsonDeserialize<DailySummary>(res);
            return instance;
        }

        /// <summary>
        /// 获取用户访问小程序数据日趋势
        /// </summary>
        /// <param name="begin_date">开始日期。格式为 yyyymmdd</param>
        /// <param name="end_date">结束日期，限定查询1天数据，允许设置的最大值为昨日。格式为 yyyymmdd</param>
        /// <returns></returns>
        public DailyVisitTrend GetAnalysisDailyVisitTrend(string begin_date, string end_date)
        {
            string access_token = GetAccessToken();
            string url = $"https://api.weixin.qq.com/datacube/getweanalysisappiddailyvisittrend?access_token={access_token}";
            string body = GetBody(begin_date, end_date);

            var res = HttpHelper.Post(url, body);

            //系统错误的话就直接返回“初始”数据
            if (res.Contains("\"errcode\":-1"))
            {
                return new DailyVisitTrend() { list = new List<DailyVisitTrend.TempModel>() };
            }

            CheckResponse(res);

            var instance = Serializer.JsonDeserialize<DailyVisitTrend>(res);
            return instance;
        }

        /// <summary>
        /// 获取用户访问小程序月留存
        /// </summary>
        /// <param name="begin_date">开始日期，为自然月第一天。格式为 yyyymmdd</param>
        /// <param name="end_date">结束日期，为自然月最后一天，限定查询一个月数据。格式为 yyyymmdd</param>
        /// <returns></returns>
        public MonthlyRetain GetAnalysisMonthlyRetain(string begin_date, string end_date)
        {
            string access_token = GetAccessToken();
            string url = $"https://api.weixin.qq.com/datacube/getweanalysisappidmonthlyretaininfo?access_token={access_token}";
            string body = GetBody(begin_date, end_date);

            var res = HttpHelper.Post(url, body);

            //系统错误的话就直接返回“初始”数据
            if (res.Contains("\"errcode\":-1"))
            {
                return new MonthlyRetain();
            }

            CheckResponse(res);

            var instance = Serializer.JsonDeserialize<MonthlyRetain>(res);
            return instance;
        }

        /// <summary>
        /// 获取用户访问小程序日留存
        /// </summary>
        /// <param name="begin_date">开始日期。格式为 yyyymmdd</param>
        /// <param name="end_date">结束日期，限定查询1天数据，允许设置的最大值为昨日。格式为 yyyymmdd</param>
        /// <returns></returns>
        public DailyRetain GetAnalysisDailyRetain(string begin_date, string end_date)
        {
            string access_token = GetAccessToken();
            string url = $"https://api.weixin.qq.com/datacube/getweanalysisappiddailyretaininfo?access_token={access_token}";
            string body = GetBody(begin_date, end_date);

            var res = HttpHelper.Post(url, body);

            //系统错误的话就直接返回“初始”数据
            if (res.Contains("\"errcode\":-1"))
            {
                return new DailyRetain();
            }

            CheckResponse(res);

            var instance = Serializer.JsonDeserialize<DailyRetain>(res);
            return instance;
        }

        /// <summary>
        /// 获取小程序新增或活跃用户的画像分布数据
        /// 时间范围支持昨天、最近7天、最近30天。其中，新增用户数为时间范围内首次访问小程序的去重用户数，活跃用户数为时间范围内访问过小程序的去重用户数。
        /// </summary>
        /// <param name="begin_date">开始日期。格式为 yyyymmdd</param>
        /// <param name="end_date">结束日期，开始日期与结束日期相差的天数限定为0/6/29，分别表示查询最近1/7/30天数据，允许设置的最大值为昨日。格式为 yyyymmdd</param>
        /// <returns></returns>
        public UserPortrait GetAnalysisUserPortrait(string begin_date, string end_date)
        {
            string access_token = GetAccessToken();
            string url = $"https://api.weixin.qq.com/datacube/getweanalysisappiduserportrait?access_token={access_token}";
            string body = GetBody(begin_date, end_date);

            var res = HttpHelper.Post(url, body);

            //系统错误的话就直接返回“初始”数据
            if (res.Contains("\"errcode\":-1"))
            {
                return new UserPortrait();
            }

            CheckResponse(res);

            var instance = Serializer.JsonDeserialize<UserPortrait>(res);
            return instance;
        }

        /// <summary>
        /// 获取用户小程序访问分布数据
        /// </summary>
        /// <param name="begin_date">开始日期。格式为 yyyymmdd</param>
        /// <param name="end_date">结束日期，限定查询 1 天数据，允许设置的最大值为昨日。格式为 yyyymmdd</param>
        /// <returns></returns>
        public VisitDistribution GetAnalysisVisitDistribution(string begin_date, string end_date)
        {
            string access_token = GetAccessToken();
            string url = $"https://api.weixin.qq.com/datacube/getweanalysisappidvisitdistribution?access_token={access_token}";
            string body = GetBody(begin_date, end_date);

            var res = HttpHelper.Post(url, body);

            //系统错误的话就直接返回“初始”数据
            if (res.Contains("\"errcode\":-1"))
            {
                return new VisitDistribution() { list = new List<VisitDistribution.item_list_model>() };
            }

            CheckResponse(res);

            var instance = Serializer.JsonDeserialize<VisitDistribution>(res);
            return instance;
        }

        /// <summary>
        /// 访问页面。目前只提供按 page_visit_pv 排序的 top200。
        /// </summary>
        /// <param name="begin_date">开始日期。格式为 yyyymmdd</param>
        /// <param name="end_date">结束日期，限定查询 1 天数据，允许设置的最大值为昨日。格式为 yyyymmdd</param>
        /// <returns></returns>
        public VisitPage GetAnalysisVisitPage(string begin_date, string end_date)
        {
            string access_token = GetAccessToken();
            string url = $"https://api.weixin.qq.com/datacube/getweanalysisappidvisitpage?access_token={access_token}";
            string body = GetBody(begin_date, end_date);

            var res = HttpHelper.Post(url, body);

            //系统错误的话就直接返回“初始”数据
            if (res.Contains("\"errcode\":-1"))
            {
                return new VisitPage() { list = new List<VisitPage.TempObj>() };
            }

            CheckResponse(res);

            var instance = Serializer.JsonDeserialize<VisitPage>(res);
            return instance;
        }

        /// <summary>
        /// 获取用户访问小程序周留存
        /// </summary>
        /// <param name="begin_date">开始日期，为周一日期。格式为 yyyymmdd</param>
        /// <param name="end_date">结束日期，为周日日期，限定查询一周数据。格式为 yyyymmdd</param>
        /// <returns></returns>
        public WeeklyRetain GetAnalysisWeeklyRetain(string begin_date, string end_date)
        {
            string access_token = GetAccessToken();
            string url = $"https://api.weixin.qq.com/datacube/getweanalysisappidweeklyretaininfo?access_token={access_token}";
            string body = GetBody(begin_date, end_date);

            var res = HttpHelper.Post(url, body);

            //系统错误的话就直接返回“初始”数据
            if (res.Contains("\"errcode\":-1"))
            {
                return new WeeklyRetain();
            }

            CheckResponse(res);

            var instance = Serializer.JsonDeserialize<WeeklyRetain>(res);
            return instance;
        }

        /// <summary>
        /// 获取用户访问小程序数据月趋势
        /// </summary>
        /// <param name="begin_date">开始日期，为自然月第一天。格式为 yyyymmdd</param>
        /// <param name="end_date">结束日期，为自然月最后一天，限定查询一个月的数据。格式为 yyyymmdd</param>
        /// <returns></returns>
        public MonthlyVisitTrend GetAnalysisMonthlyVisitTrend(string begin_date, string end_date)
        {
            string access_token = GetAccessToken();
            string url = $"https://api.weixin.qq.com/datacube/getweanalysisappidmonthlyvisittrend?access_token={access_token}";
            string body = GetBody(begin_date, end_date);

            var res = HttpHelper.Post(url, body);

            //系统错误的话就直接返回“初始”数据
            if (res.Contains("\"errcode\":-1"))
            {
                return new MonthlyVisitTrend() { list = new List<DailyVisitTrend.TempModel>() };
            }

            CheckResponse(res);

            var instance = Serializer.JsonDeserialize<MonthlyVisitTrend>(res);
            return instance;
        }

        #endregion

    }
}
