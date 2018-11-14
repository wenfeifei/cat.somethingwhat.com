using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Public.Services;
using Cat.Web.Support.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Api.Controllers
{
    /// <summary>
    /// 小程序相关接口
    /// </summary>
    [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Administrator)]
    public class MiniProgramController : BaseController
    {
        private string DateFormat = "yyyyMMdd";

        /// <summary>
        /// 获取小程序 access_token
        /// </summary>
        /// <returns></returns>
        [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Anonymous)]
        public ActionRes GetAccessToken()
        {
            if (!base.IsAdministrator) return ActionRes.Fail("当前登录用户没有权限进行操作");
            return ActionRes.Success(AllServices.MiniProgramService.GetAccessToken());
        }

        /// <summary>
        /// 获取用户访问小程序数据周趋势
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisWeeklyVisitTrend(DateTime begin_time, DateTime end_time)
        {
            if (end_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询之前一周的数据");
            if (((end_time - begin_time).TotalDays + 1) != 7) throw new Exception("限定查询一个自然周的数据，时间必须按照自然周的方式输入");

            var data = AllServices.MiniProgramService.GetAnalysisWeeklyVisitTrend(begin_time.ToString(DateFormat), end_time.ToString(DateFormat));
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序数据概况
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisDailySummary(DateTime query_time)
        {
            if (query_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询今天之前的数据");

            var data = AllServices.MiniProgramService.GetAnalysisDailySummary(query_time.ToString(DateFormat), query_time.ToString(DateFormat));
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序数据日趋势
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisDailyVisitTrend(DateTime query_time)
        {
            if (query_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询今天之前的数据");

            var data = AllServices.MiniProgramService.GetAnalysisDailyVisitTrend(query_time.ToString(DateFormat), query_time.ToString(DateFormat));
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序月留存
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisMonthlyRetain(DateTime begin_time, DateTime end_time)
        {
            if (begin_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"))) throw new Exception("限定只能查询当月之前的数据");
            if (begin_time.Day != 1 || ((end_time - begin_time).TotalDays + 1) != DateTime.DaysInMonth(begin_time.Year, begin_time.Month)) throw new Exception("限定查询一个自然月的数据，时间必须按照自然月的方式输入");

            var data = AllServices.MiniProgramService.GetAnalysisMonthlyRetain(begin_time.ToString(DateFormat), end_time.ToString(DateFormat));
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序日留存
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisDailyRetain(DateTime query_time)
        {
            if (query_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询今天之前的数据");

            var data = AllServices.MiniProgramService.GetAnalysisDailyRetain(query_time.ToString(DateFormat), query_time.ToString(DateFormat));
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取小程序新增或活跃用户的画像分布数据
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisUserPortrait(int query_type = 1)
        {
            DateTime begin_time; DateTime end_time;

            switch (query_type)
            {
                //昨天
                case 1:
                    end_time = DateTime.Now.AddDays(-1);
                    begin_time = end_time;
                    break;
                //最近7天
                case 2:
                    end_time = DateTime.Now.AddDays(-1);
                    begin_time = end_time.AddDays(-6);
                    break;
                //最近30天
                case 3:
                    end_time = DateTime.Now.AddDays(-1);
                    begin_time = end_time.AddDays(-29);
                    break;
                default:
                    throw new Exception($"query_type is error：{query_type}");
            }

            var data = AllServices.MiniProgramService.GetAnalysisUserPortrait(begin_time.ToString(DateFormat), end_time.ToString(DateFormat));
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户小程序访问分布数据
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisVisitDistribution(DateTime query_time)
        {
            if (query_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询今天之前的数据");

            var data = AllServices.MiniProgramService.GetAnalysisVisitDistribution(query_time.ToString(DateFormat), query_time.ToString(DateFormat));
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 访问页面。目前只提供按 page_visit_pv 排序的 top200。
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisVisitPage(DateTime query_time)
        {
            if (query_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询今天之前的数据");

            var data = AllServices.MiniProgramService.GetAnalysisVisitPage(query_time.ToString(DateFormat), query_time.ToString(DateFormat));
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序周留存
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisWeeklyRetain(DateTime begin_time, DateTime end_time)
        {
            if (end_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询之前一周的数据");
            if (end_time.DayOfWeek != DayOfWeek.Sunday) throw new Exception("必须是查询周一到周日的数据");
            if (((end_time - begin_time).TotalDays + 1) != 7) throw new Exception("限定查询一个自然周的数据，时间必须按照自然周的方式输入");

            var data = AllServices.MiniProgramService.GetAnalysisWeeklyRetain(begin_time.ToString(DateFormat), end_time.ToString(DateFormat));
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序数据月趋势
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisMonthlyVisitTrend(DateTime begin_time, DateTime end_time)
        {
            if (begin_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"))) throw new Exception("限定只能查询当月之前的数据");
            if (begin_time.Day != 1 || ((end_time - begin_time).TotalDays + 1) != DateTime.DaysInMonth(begin_time.Year, begin_time.Month)) throw new Exception("限定查询一个自然月的数据，时间必须按照自然月的方式输入");

            var data = AllServices.MiniProgramService.GetAnalysisMonthlyVisitTrend(begin_time.ToString(DateFormat), end_time.ToString(DateFormat));
            return ActionRes.Success(data);
        }
    }
}