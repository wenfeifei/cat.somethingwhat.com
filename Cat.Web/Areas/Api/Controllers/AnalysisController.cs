using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Public.Models.ModelBinder.ReturnModels.WechatAnalysis;
using Cat.M.Public.Services;
using Cat.M.Public.Services.Constants;
using Cat.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Api.Controllers
{
    /// <summary>
    /// 微信小程序提供的数据分析
    /// </summary>
    public class AnalysisController : BaseController
    {
        /// <summary>
        /// yyyyMMdd
        /// </summary>
        private string DateFormat = "yyyyMMdd";
        /// <summary>
        /// yyyyMM
        /// </summary>
        private string MonthFormat = "yyyyMM";

        /// <summary>
        /// 将类似 yyyyMMdd 这种日期转换为 yyyy.MM.dd
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        private string ConvertDateStr(string dateStr)
        {
            if (string.IsNullOrEmpty(dateStr) || dateStr.Length > 8 || dateStr.Length % 2 > 0) return dateStr;

            if (dateStr.Length == 4) dateStr += "/01/01";
            if (dateStr.Length == 6) dateStr += "01";
            var dateTime = DateTime.ParseExact(dateStr, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);

            if (dateStr.Length == 4)
                return dateTime.ToString("yyyy");
            if (dateStr.Length == 6)
                return dateTime.ToString("yyyy.MM");
            else
                return dateTime.ToString("yyyy.MM.dd");
        }

        /// <summary>
        /// 获取当前响应结果的查询日期
        /// </summary>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        private static string GetQueryDate(DateTime begin_time, DateTime end_time)
        {
            string query_data = string.Empty;
            if (begin_time == end_time) query_data = begin_time.ToString("yyyy-MM-dd");
            else query_data = $"{begin_time.ToString("yyyy-MM-dd")} ～ {end_time.ToString("yyyy-MM-dd")}";
            return query_data;
        }

        /// <summary>
        /// 获取用户访问小程序数据概况
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisDailySummary(DateTime begin_time, DateTime end_time, int type = 0, int query_type = 0)
        {
            if (query_type == 0)
            {
                if (begin_time == DateTime.MinValue || end_time == DateTime.MinValue) throw new Exception("请输入要查询的日期");
                if (end_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询今天之前的数据");

                int day = (int)(end_time - begin_time).TotalDays + 1;
                if (day > 30) throw new Exception("考虑到查询效率，避免等待过久，故最多查询30天的数据");
            }

            //凌晨3点过后才能查询“昨天”的统计数据
            if (DateTime.Now.Hour <= 3)
            {
                if (begin_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat) ||
                    end_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat))
                {
                    throw new Exception("凌晨3点过后才能查询“昨天”的统计数据");
                }
            }

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
                //自定义范围
                case 0:
                    break;
                default:
                    throw new Exception($"query_type is error：{query_type}");
            }

            if (type == 1)
            {
                begin_time = begin_time.AddDays(-7);
                end_time = end_time.AddDays(-7);
            }
            if (type == 2)
            {
                begin_time = begin_time.AddMonths(-1);
                end_time = end_time.AddMonths(-1);
            }

            List<DailySummary.TempModel> listData = new List<DailySummary.TempModel>();
            for (var i = 0; i < (end_time - begin_time).TotalDays + 1; i++)
            {
                DailySummary oneData = AllServices.MiniProgramService.GetAnalysisDailySummary(begin_time.AddDays(i).ToString(DateFormat), begin_time.AddDays(i).ToString(DateFormat));
                if (!oneData.list.Any())
                {
                    oneData.list.Add(new DailySummary.TempModel()
                    {
                        ref_date = begin_time.AddDays(i).ToString(DateFormat)
                    });
                }
                listData.Add(oneData.list.FirstOrDefault() ?? new DailySummary.TempModel());
            }

            //return ActionRes.Success(listData);
            var data = new
            {
                columns = listData.Select(s => ConvertDateStr(s.ref_date)).ToList(),
                data_visit_total = listData.Select(s => s.visit_total).ToList(),
                data_share_pv = listData.Select(s => s.share_pv).ToList(),
                data_share_uv = listData.Select(s => s.share_uv).ToList(),
                query_data = GetQueryDate(begin_time, end_time),
            };

            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序数据日趋势
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisDailyVisitTrend(DateTime begin_time, DateTime end_time)
        {
            if (begin_time == DateTime.MinValue || end_time == DateTime.MinValue) throw new Exception("请输入要查询的日期");
            if (end_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询今天之前的数据");

            int day = (int)(end_time - begin_time).TotalDays + 1;
            if (day > 14) throw new Exception("考虑到查询效率，避免等待过久，故最多查询14天的数据");

            //凌晨3点过后才能查询“昨天”的统计数据
            if (DateTime.Now.Hour <= 3)
            {
                if (begin_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat) ||
                    end_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat))
                {
                    throw new Exception("凌晨3点过后才能查询“昨天”的统计数据");
                }
            }

            List<DailyVisitTrend.TempModel> listData = new List<DailyVisitTrend.TempModel>();
            for (var i = 0; i < day; i++)
            {
                DailyVisitTrend oneData = AllServices.MiniProgramService.GetAnalysisDailyVisitTrend(begin_time.AddDays(i).ToString(DateFormat), begin_time.AddDays(i).ToString(DateFormat));
                if (!oneData.list.Any())
                {
                    oneData.list.Add(new DailyVisitTrend.TempModel()
                    {
                        ref_date = begin_time.AddDays(i).ToString(DateFormat)
                    });
                }
                listData.Add(oneData.list.FirstOrDefault() ?? new DailyVisitTrend.TempModel());
            }

            //return ActionRes.Success(listData);
            var data = new
            {
                columns = listData.Select(s => ConvertDateStr(s.ref_date)).ToList(),
                data_session_cnt = listData.Select(s => s.session_cnt).ToList(),
                data_visit_pv = listData.Select(s => s.visit_pv).ToList(),
                data_visit_uv = listData.Select(s => s.visit_uv).ToList(),
                data_visit_uv_new = listData.Select(s => s.visit_uv_new).ToList(),
                data_stay_time_uv = listData.Select(s => s.stay_time_uv).ToList(),
                data_stay_time_session = listData.Select(s => s.stay_time_session).ToList(),
                data_visit_depth = listData.Select(s => s.visit_depth).ToList(),
                query_data = GetQueryDate(begin_time, end_time),
            };

            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序数据周趋势
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisWeeklyVisitTrend(DateTime begin_time, DateTime end_time)
        {
            if (begin_time == DateTime.MinValue || end_time == DateTime.MinValue) throw new Exception("请输入要查询的日期");
            if (end_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询今天之前的数据");
            if (begin_time.DayOfWeek != DayOfWeek.Monday) throw new Exception("限定开始时间必须是周一");
            if (end_time.DayOfWeek != DayOfWeek.Sunday) throw new Exception("限定结束时间必须是周日");

            int weeek = (int)((end_time - begin_time).TotalDays + 1) / 7;

            if (weeek < 1) throw new Exception("限定查询一个自然周的数据");
            if (weeek > 9) throw new Exception("限定最多查询连续9个星期的数据");

            //凌晨3点过后才能查询“昨天”的统计数据
            if (DateTime.Now.Hour <= 3)
            {
                if (begin_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat) ||
                    end_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat))
                {
                    throw new Exception("凌晨3点过后才能查询“昨天”的统计数据");
                }
            }

            List<WeeklyVisitTrend.TempModel> listData = new List<WeeklyVisitTrend.TempModel>();
            for (var i = 0; i < weeek; i++)
            {
                WeeklyVisitTrend oneData = AllServices.MiniProgramService.GetAnalysisWeeklyVisitTrend(begin_time.AddDays(i * 7).ToString(DateFormat), begin_time.AddDays((i + 1) * 7 - 1).ToString(DateFormat));
                if (!oneData.list.Any())
                {
                    oneData.list.Add(new WeeklyVisitTrend.TempModel()
                    {
                        ref_date = begin_time.AddDays(i * 7).ToString(DateFormat) + "-" + begin_time.AddDays((i + 1) * 7 - 1).ToString(DateFormat)
                    });
                }
                listData.Add(oneData.list.FirstOrDefault() ?? new WeeklyVisitTrend.TempModel());
            }

            //return ActionRes.Success(listData);
            var data = new
            {
                columns = listData.Select(s => ConvertDateStr(s.ref_date)).ToList(),
                data_session_cnt = listData.Select(s => s.session_cnt).ToList(),
                data_visit_pv = listData.Select(s => s.visit_pv).ToList(),
                data_visit_uv = listData.Select(s => s.visit_uv).ToList(),
                data_visit_uv_new = listData.Select(s => s.visit_uv_new).ToList(),
                data_stay_time_uv = listData.Select(s => s.stay_time_uv).ToList(),
                data_stay_time_session = listData.Select(s => s.stay_time_session).ToList(),
                data_visit_depth = listData.Select(s => s.visit_depth).ToList(),
                query_data = GetQueryDate(begin_time, end_time),
            };

            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序数据月趋势
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisMonthlyVisitTrend(DateTime begin_time, DateTime end_time)
        {
            if (begin_time == DateTime.MinValue || end_time == DateTime.MinValue) throw new Exception("请输入要查询的日期");
            if (end_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"))) throw new Exception("限定只能查询当月之前的数据");
            if (begin_time.Day != 1) throw new Exception("限定开始时间必须是自然月第一天");
            if (end_time.AddDays(1).Day != 1) throw new Exception("限定结束时间必须是自然月最后一天");

            int totalMonth = (end_time.Year * 12 + end_time.Month) - (begin_time.Year * 12 + begin_time.Month) + 1;

            if (totalMonth < 1) throw new Exception("限定查询一个自然月的数据");
            if (totalMonth > 12) throw new Exception("限定最多查询连续12个月的数据");

            //凌晨3点过后才能查询“昨天”的统计数据
            if (DateTime.Now.Hour <= 3)
            {
                if (begin_time.ToString(MonthFormat) == DateTime.Now.AddDays(-1).ToString(MonthFormat) ||
                    end_time.ToString(MonthFormat) == DateTime.Now.AddDays(-1).ToString(MonthFormat))
                {
                    throw new Exception("凌晨3点过后才能查询“昨天”的统计数据");
                }
            }

            List<MonthlyVisitTrend.TempModel> listData = new List<MonthlyVisitTrend.TempModel>();
            for (var i = 0; i < totalMonth; i++)
            {
                MonthlyVisitTrend oneData = AllServices.MiniProgramService.GetAnalysisMonthlyVisitTrend(begin_time.AddMonths(i).ToString(DateFormat), begin_time.AddMonths(i + 1).AddDays(-1).ToString(DateFormat));
                if (!oneData.list.Any())
                {
                    oneData.list.Add(new MonthlyVisitTrend.TempModel()
                    {
                        ref_date = begin_time.AddMonths(i).ToString("yyyyMM")
                    });
                }
                listData.Add(oneData.list.FirstOrDefault());
            }

            //return ActionRes.Success(listData);
            var data = new
            {
                columns = listData.Select(s => ConvertDateStr(s.ref_date)).ToList(),
                data_session_cnt = listData.Select(s => s.session_cnt).ToList(),
                data_visit_pv = listData.Select(s => s.visit_pv).ToList(),
                data_visit_uv = listData.Select(s => s.visit_uv).ToList(),
                data_visit_uv_new = listData.Select(s => s.visit_uv_new).ToList(),
                data_stay_time_uv = listData.Select(s => s.stay_time_uv).ToList(),
                data_stay_time_session = listData.Select(s => s.stay_time_session).ToList(),
                data_visit_depth = listData.Select(s => s.visit_depth).ToList(),
                query_data = GetQueryDate(begin_time, end_time),
            };

            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序日留存
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisDailyRetain(DateTime begin_time, DateTime end_time)
        {
            if (begin_time == DateTime.MinValue || end_time == DateTime.MinValue) throw new Exception("请输入要查询的日期");
            if (end_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询今天之前的数据");

            int day = (int)(end_time - begin_time).TotalDays + 1;
            if (day > 14) throw new Exception("考虑到查询效率，避免等待过久，故最多查询14天的数据");

            //凌晨3点过后才能查询“昨天”的统计数据
            if (DateTime.Now.Hour <= 3)
            {
                if (begin_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat) ||
                    end_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat))
                {
                    throw new Exception("凌晨3点过后才能查询“昨天”的统计数据");
                }
            }

            List<DailyRetain> listData = new List<DailyRetain>();
            for (var i = 0; i < day; i++)
            {
                DailyRetain oneData = AllServices.MiniProgramService.GetAnalysisDailyRetain(begin_time.AddDays(i).ToString(DateFormat), begin_time.AddDays(i).ToString(DateFormat));
                if (oneData == null || string.IsNullOrEmpty(oneData.ref_date))
                {
                    oneData = new DailyRetain()
                    {
                        ref_date = begin_time.AddDays(i).ToString(DateFormat)
                    };
                    if (oneData.visit_uv == null) oneData.visit_uv = new List<DailyRetain.KeyValueModel>();
                    if (oneData.visit_uv_new == null) oneData.visit_uv_new = new List<DailyRetain.KeyValueModel>();
                }
                listData.Add(oneData);
            }

            //重新构造响应的数据格式
            var resList = new List<VisitRetain>();
            foreach (var item in listData)
            {
                resList.Add(new VisitRetain()
                {
                    ref_date = ConvertDateStr(item.ref_date),
                    visit_uv = item.visit_uv.Select(s => s.value).ToList(),
                    visit_uv_new = item.visit_uv_new.Select(s => s.value).ToList(),
                });
            }

            var data = new
            {
                columns = listData.OrderByDescending(o => o.visit_uv_new.Count).FirstOrDefault().visit_uv_new.Select(s => s.key).ToList(),
                data = resList,
                query_data = GetQueryDate(begin_time, end_time),
            };
            
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序周留存
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisWeeklyRetain(DateTime begin_time, DateTime end_time)
        {
            if (begin_time == DateTime.MinValue || end_time == DateTime.MinValue) throw new Exception("请输入要查询的日期");
            if (end_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询今天之前的数据");
            if (begin_time.DayOfWeek != DayOfWeek.Monday) throw new Exception("限定开始时间必须是周一");
            if (end_time.DayOfWeek != DayOfWeek.Sunday) throw new Exception("限定结束时间必须是周日");

            int weeek = (int)((end_time - begin_time).TotalDays + 1) / 7;

            if (weeek < 1) throw new Exception("限定查询一个自然周的数据");
            if (weeek > 9) throw new Exception("限定最多查询连续9个星期的数据");

            //凌晨3点过后才能查询“昨天”的统计数据
            if (DateTime.Now.Hour <= 3)
            {
                if (begin_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat) ||
                    end_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat))
                {
                    throw new Exception("凌晨3点过后才能查询“昨天”的统计数据");
                }
            }

            List<WeeklyRetain> listData = new List<WeeklyRetain>();
            for (var i = 0; i < weeek; i++)
            {
                WeeklyRetain oneData = AllServices.MiniProgramService.GetAnalysisWeeklyRetain(begin_time.AddDays(i * 7).ToString(DateFormat), begin_time.AddDays((i + 1) * 7 - 1).ToString(DateFormat));
                if (oneData == null || string.IsNullOrEmpty(oneData.ref_date))
                {
                    oneData = new WeeklyRetain()
                    {
                        ref_date = begin_time.AddDays(i * 7).ToString(DateFormat) + "-" + begin_time.AddDays((i + 1) * 7 - 1).ToString(DateFormat)
                    };
                    if (oneData.visit_uv == null) oneData.visit_uv = new List<DailyRetain.KeyValueModel>();
                    if (oneData.visit_uv_new == null) oneData.visit_uv_new = new List<DailyRetain.KeyValueModel>();
                }
                listData.Add(oneData);
            }

            //重新构造响应的数据格式
            var resList = new List<VisitRetain>();
            foreach (var item in listData)
            {
                resList.Add(new VisitRetain()
                {
                    ref_date = ConvertDateStr(item.ref_date),
                    visit_uv = item.visit_uv.Select(s => s.value).ToList(),
                    visit_uv_new = item.visit_uv_new.Select(s => s.value).ToList(),
                });
            }

            var data = new
            {
                columns = listData.OrderByDescending(o => o.visit_uv_new.Count).FirstOrDefault().visit_uv_new.Select(s => s.key).ToList(),
                data = resList,
                query_data = GetQueryDate(begin_time, end_time),
            };

            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户访问小程序月留存
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisMonthlyRetain(DateTime begin_time, DateTime end_time)
        {
            if (begin_time == DateTime.MinValue || end_time == DateTime.MinValue) throw new Exception("请输入要查询的日期");
            if (end_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"))) throw new Exception("限定只能查询当月之前的数据");
            if (begin_time.Day != 1) throw new Exception("限定开始时间必须是自然月第一天");
            if (end_time.AddDays(1).Day != 1) throw new Exception("限定结束时间必须是自然月最后一天");

            int totalMonth = (end_time.Year * 12 + end_time.Month) - (begin_time.Year * 12 + begin_time.Month) + 1;

            if (totalMonth < 1) throw new Exception("限定查询一个自然月的数据");
            if (totalMonth > 12) throw new Exception("限定最多查询连续12个月的数据");

            //凌晨3点过后才能查询“昨天”的统计数据
            if (DateTime.Now.Hour <= 3)
            {
                if (begin_time.ToString(MonthFormat) == DateTime.Now.AddDays(-1).ToString(MonthFormat) ||
                    end_time.ToString(MonthFormat) == DateTime.Now.AddDays(-1).ToString(MonthFormat))
                {
                    throw new Exception("凌晨3点过后才能查询“昨天”的统计数据");
                }
            }

            List<MonthlyRetain> listData = new List<MonthlyRetain>();
            for (var i = 0; i < totalMonth; i++)
            {
                MonthlyRetain oneData = AllServices.MiniProgramService.GetAnalysisMonthlyRetain(begin_time.AddMonths(i).ToString(DateFormat), begin_time.AddMonths(i + 1).AddDays(-1).ToString(DateFormat));
                if (oneData == null || string.IsNullOrEmpty(oneData.ref_date))
                {
                    oneData = new MonthlyRetain()
                    {
                        ref_date = begin_time.AddMonths(i).ToString("yyyyMM"),
                        visit_uv = new List<DailyRetain.KeyValueModel>() { new DailyRetain.KeyValueModel() { key = 0, value = 0 } },
                        visit_uv_new = new List<DailyRetain.KeyValueModel>() { new DailyRetain.KeyValueModel() { key = 0, value = 0 } },
                    };
                    if (oneData.visit_uv == null) oneData.visit_uv = new List<DailyRetain.KeyValueModel>();
                    if (oneData.visit_uv_new == null) oneData.visit_uv_new = new List<DailyRetain.KeyValueModel>();
                }
                listData.Add(oneData);
            }

            //重新构造响应的数据格式
            var resList = new List<VisitRetain>();
            foreach (var item in listData)
            {
                resList.Add(new VisitRetain()
                {
                    ref_date = ConvertDateStr(item.ref_date),
                    visit_uv = item.visit_uv.Select(s => s.value).ToList(),
                    visit_uv_new = item.visit_uv_new.Select(s => s.value).ToList(),
                });
            }

            var data = new
            {
                columns = listData.OrderByDescending(o => o.visit_uv_new.Count).FirstOrDefault().visit_uv_new.Select(s => s.key).ToList(),
                data = resList,
                query_data = GetQueryDate(begin_time, end_time),
            };

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

            //凌晨3点过后才能查询“昨天”的统计数据
            if (DateTime.Now.Hour <= 3)
            {
                if (begin_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat) ||
                    end_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat))
                {
                    throw new Exception("凌晨3点过后才能查询“昨天”的统计数据");
                }
            }

            var data = AllServices.MiniProgramService.GetAnalysisUserPortrait(begin_time.ToString(DateFormat), end_time.ToString(DateFormat));

            //排序
            if (data != null && !string.IsNullOrEmpty(data.ref_date))
            {
                if (data.visit_uv.province.Any()) data.visit_uv.province = data.visit_uv.province.OrderByDescending(o => o.value).ToList();
                if (data.visit_uv.city.Any()) data.visit_uv.city = data.visit_uv.city.OrderByDescending(o => o.value).ToList();
                if (data.visit_uv.devices.Any()) data.visit_uv.devices = data.visit_uv.devices.OrderByDescending(o => o.value).ToList();

                if (data.visit_uv_new.province.Any()) data.visit_uv_new.province = data.visit_uv_new.province.OrderByDescending(o => o.value).ToList();
                if (data.visit_uv_new.city.Any()) data.visit_uv_new.city = data.visit_uv_new.city.OrderByDescending(o => o.value).ToList();
                if (data.visit_uv_new.devices.Any()) data.visit_uv_new.devices = data.visit_uv_new.devices.OrderByDescending(o => o.value).ToList();
            }

            //改变日期格式
            data.ref_date = ConvertDateStr(data.ref_date);
            data.query_data = GetQueryDate(begin_time, end_time);

            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取用户小程序访问分布数据
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisVisitDistribution(int query_type = 1)
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

            //凌晨3点过后才能查询“昨天”的统计数据
            if (DateTime.Now.Hour <= 3)
            {
                if (begin_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat) ||
                    end_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat))
                {
                    throw new Exception("凌晨3点过后才能查询“昨天”的统计数据");
                }
            }

            List<VisitDistributionMulti> list = new List<VisitDistributionMulti>();

            for (var i = 0; i < (end_time - begin_time).TotalDays + 1; i++)
            {
                VisitDistribution instance = AllServices.MiniProgramService.GetAnalysisVisitDistribution(begin_time.AddDays(i).ToString(DateFormat), begin_time.AddDays(i).ToString(DateFormat));

                //重新构造返回数据的格式
                var single = new VisitDistributionMulti();
                single.ref_date = ConvertDateStr(instance.ref_date);
                instance.list.ForEach(item =>
                  {
                      switch (item.index)
                      {
                          case "access_source_session_cnt":
                              single.access_source_visit_pv = item.item_list.Select(s => new VisitDistributionMulti.KeyValueModel()
                              {
                                  key = s.key,
                                  value = s.value,
                                  name = SceneByAnalysis.GetName(s.key)
                              }).ToList();
                              break;
                          case "access_source_visit_uv":
                              single.access_source_visit_uv = item.item_list.Select(s => new VisitDistributionMulti.KeyValueModel()
                              {
                                  key = s.key,
                                  value = s.value,
                                  name = SceneByAnalysis.GetName(s.key)
                              }).ToList();
                              break;
                          case "access_staytime_info":
                              single.access_staytime_info = item.item_list.Select(s => new VisitDistributionMulti.KeyValueModel()
                              {
                                  key = s.key,
                                  value = s.value,
                                  name = SceneByAnalysis.GetNameByStaytime(s.key)
                              }).OrderByDescending(o => o.key).ToList();
                              break;
                          case "access_depth_info":
                              single.access_depth_info = item.item_list.Select(s => new VisitDistributionMulti.KeyValueModel()
                              {
                                  key = s.key,
                                  value = s.value,
                                  name = SceneByAnalysis.GetNameByDepth(s.key)
                              }).OrderByDescending(o => o.key).ToList();
                              break;
                      }
                  });


                list.Add(single);
            }

            string query_data = GetQueryDate(begin_time, end_time);

            return ActionRes.Success(new { query_data, list });
        }

        /// <summary>
        /// 访问页面。目前只提供按 page_visit_pv 排序的 top200。
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAnalysisVisitPage(DateTime begin_time, DateTime end_time, int query_type = 0)
        {
            //DateTime begin_time; DateTime end_time;
            if (query_type == 0)
            {
                if (begin_time == DateTime.MinValue || end_time == DateTime.MinValue) throw new Exception("请输入要查询的日期");
                if (end_time >= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) throw new Exception("限定只能查询今天之前的数据");

                int day = (int)(end_time - begin_time).TotalDays + 1;
                if (day > 30) throw new Exception("考虑到查询效率，避免等待过久，故最多查询30天的数据");
            }

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
                //自定义范围
                case 0:
                    break;
                default:
                    throw new Exception($"query_type is error：{query_type}");
            }

            //凌晨3点过后才能查询“昨天”的统计数据
            if (DateTime.Now.Hour <= 3)
            {
                if (begin_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat) ||
                    end_time.ToString(DateFormat) == DateTime.Now.AddDays(-1).ToString(DateFormat))
                {
                    throw new Exception("凌晨3点过后才能查询“昨天”的统计数据");
                }
            }

            List<VisitPage> list = new List<VisitPage>();

            for (var i = 0; i < (end_time - begin_time).TotalDays + 1; i++)
            {
                VisitPage instance = AllServices.MiniProgramService.GetAnalysisVisitPage(begin_time.AddDays(i).ToString(DateFormat), begin_time.AddDays(i).ToString(DateFormat));
                instance.ref_date = ConvertDateStr(instance.ref_date);
                list.Add(instance);
            }

            string query_data = GetQueryDate(begin_time, end_time);

            return ActionRes.Success(new { query_data, list });
        }
    }
}