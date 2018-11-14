using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Book.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Api.Controllers
{
    /// <summary>
    /// 报表统计 控制器
    /// </summary>
    public class StatisticsController : BaseController
    {
        /// <summary>
        /// 根据列表数据，获取x坐标轴的集合
        /// </summary>
        /// <param name="dateType"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<string> GetColumns<T>(string dateType, IList<T> list) where T : M.Book.Services.Models.Response.Statistics.DateStatistics
        {
            var columns = new List<string>();
            if (dateType == "y")
            {
                foreach (var item in list)
                {
                    columns.Add(string.Format("{0}年", item.y));
                }
            }
            else if (dateType == "m")
            {
                foreach (var item in list)
                {
                    columns.Add(string.Format("{0}.{1}", item.y, item.m.ToString().PadLeft(2, '0')));
                }
            }
            else if (dateType == "d")
            {
                foreach (var item in list)
                {
                    columns.Add(string.Format("{1}.{2}", item.y, item.m.ToString().PadLeft(2, '0'), item.d.ToString().PadLeft(2, '0')));
                }
            }

            return columns;
        }

        /// <summary>
        /// 新增用户统计、新增小说用户统计
        /// </summary>
        /// <param name="dateType">统计类型，y、m、d</param>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public ActionRes BookUserStatistics(string dateType, DateTime begin_time, DateTime end_time)
        {
            end_time = end_time.AddDays(1).AddMilliseconds(-1);

            var list = AllServices.StatisticsService.BookUserStatistics(dateType, begin_time, end_time);
            var list2 = AllServices.StatisticsService.BookUser2Statistics(dateType, begin_time, end_time);

            List<string> columns = GetColumns(dateType, list);

            var data = new
            {
                columns,
                data_new = list.Select(s => s.total).ToArray(),
                data_active = list2.Select(s => s.total).ToArray(),
            };

            return ActionRes.Success(data);
        }

        /// <summary>
        /// 时间段（每小时）新增用户统计
        /// </summary>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public ActionRes BookUserTimeSlotStatistics()
        {
            DateTime begin_time = DateTime.MinValue.AddMilliseconds(1);
            DateTime end_time = DateTime.MaxValue.AddMilliseconds(-1);
            var list = AllServices.StatisticsService.BookUserTimeSlotStatistics(begin_time, end_time);
            return ActionRes.Success(list);
        }

        /// <summary>
        /// 新增用户 （男女分组）统计
        /// </summary>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public ActionRes BookUserGenderStatistics()
        {
            DateTime begin_time = DateTime.MinValue.AddMilliseconds(1);
            DateTime end_time = DateTime.MaxValue.AddMilliseconds(-1);
            var list = AllServices.StatisticsService.BookUserGenderStatistics(begin_time, end_time);
            return ActionRes.Success(list);
        }

        /// <summary>
        /// 僵尸粉与有阅读记录的用户 统计
        /// </summary>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public ActionRes GetByReadUserStatistics()
        {
            DateTime begin_time = DateTime.MinValue.AddMilliseconds(1);
            DateTime end_time = DateTime.MaxValue.AddMilliseconds(-1);
            //有阅读记录的用户数
            int readCount = AllServices.StatisticsService.GetCountByReadUser(begin_time, end_time);
            //用户总数
            int total = AllServices.BookUserService.GetCount();
            //无有阅读记录的用户数
            int no_read_count = total - readCount;

            var list = new List<object>();
            list.Add(new { flag = "正常用户", total = readCount });
            list.Add(new { flag = "僵尸用户", total = no_read_count });

            return ActionRes.Success(list);
        }

        /// <summary>
        /// 用户阅读统计（参与阅读用户数、阅读章节数）
        /// </summary>
        /// <param name="dateType">统计类型，y、m、d</param>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public ActionRes BookUserReadStatistics(string dateType, DateTime begin_time, DateTime end_time)
        {
            end_time = end_time.AddDays(1).AddMilliseconds(-1);

            var list = AllServices.StatisticsService.BookUserReadStatistics(dateType, begin_time, end_time);

            List<string> columns = GetColumns(dateType, list);

            var data = new
            {
                columns,
                data_user = list.Select(s => s.users).ToArray(),
                data_chapter = list.Select(s => s.chapters).ToArray(),
            };

            return ActionRes.Success(data);
        }

        /// <summary>
        /// 时间段（每小时）阅读小说数、时间段（每小时）阅读小说章节数 统计
        /// </summary>
        /// <param name="dateType">统计类型，y、m、d</param>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public ActionRes BookReadTimeSlotStatistics()
        {
            DateTime begin_time = DateTime.MinValue.AddMilliseconds(1);
            DateTime end_time = DateTime.MaxValue.AddMilliseconds(-1);
            //时间段（每小时）阅读小说数 统计
            var list = AllServices.StatisticsService.BookReadTimeSlotStatistics(begin_time, end_time);
            //时间段（每小时）阅读小说章节数 统计
            var list2 = AllServices.StatisticsService.BookReadChapterTimeSlotStatistics(begin_time, end_time);
            //时间段（每小时）参与阅读的用户数 统计
            var list3 = AllServices.StatisticsService.BookUserReadTimeSlotStatistics(begin_time, end_time);

            var data = new
            {
                columns = list.Select(s => s.h).ToArray(),
                data_book = list.Select(s => s.total).ToArray(),
                data_chapter = list2.Select(s => s.total).ToArray(),
                data_user = list3.Select(s => s.total).ToArray(),
            };

            return ActionRes.Success(data);
        }

        /// <summary>
        /// 最受欢迎小说排行 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public ActionRes GetMostPopularBook(int top = 50)
        {
            var list = AllServices.StatisticsService.GetMostPopularBook(top);
            return ActionRes.Success(list);
        }

        /// <summary>
        /// 阅读排行榜 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public ActionRes GetReadRanking(int top = 50)
        {
            var list = AllServices.StatisticsService.GetReadRanking(top);
            return ActionRes.Success(list);
        }
    }
}