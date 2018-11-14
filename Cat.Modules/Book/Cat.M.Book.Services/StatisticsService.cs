using Cat.M.Book.Models.Table;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cat.M.Book.Services
{
    /// <summary>
    /// 报表统计 服务类
    /// </summary>
    public class StatisticsService
    {
        /// <summary>
        /// sql帮助类
        /// </summary>
        SqlService SqlService = new SqlService();

        /// <summary>
        /// 检查入参
        /// </summary>
        /// <param name="dateType"></param>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        private void CheckImputParams(DateTime begin_time, DateTime end_time)
        {
            if (begin_time == DateTime.MinValue) throw new Exception("开始日期输入错误");
            if (end_time == DateTime.MinValue) throw new Exception("结束日期输入错误");
            if (begin_time > end_time) throw new Exception("开始日期不能大于结束日期");
        }

        /// <summary>
        /// 检查入参
        /// </summary>
        /// <param name="dateType"></param>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        private void CheckImputParams(string dateType, DateTime begin_time, DateTime end_time)
        {
            if (!(dateType == "y" || dateType == "m" || dateType == "d")) throw new Exception("选择的日期类型不正确，应为 y、m、d 的其中之一");

            CheckImputParams(begin_time, end_time);

            if (dateType == "y" && (end_time - begin_time).TotalDays / 365 > 100) throw new Exception("年份的开始时间于结束时间跨度过大，请缩小日期范围（不超过100年）");
            if (dateType == "m" && (end_time - begin_time).TotalDays / 365 > 3) throw new Exception("月份的开始时间于结束时间跨度过大，请缩小日期范围（不超过36个月）");
            if (dateType == "d" && (end_time - begin_time).TotalDays > 90) throw new Exception("开始时间于结束时间跨度过大，请缩小日期范围（不超过90天）");
        }

        /// <summary>
        /// 自动填充列表数据中的“欠缺的日期”
        /// 这个方法的作用是：因为获取到的日期（即统计图表的y轴）不是连续的，所以这里将缺失的日期补充完整，使其日期连续
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dateType"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private IList<T> FillDate<T>(string dateType, DateTime begin_time, DateTime end_time, IList<T> list) where T : Models.Response.Statistics.DateStatistics, new()
        {
            //if (list.Count < 2) return list;

            //var first = list.FirstOrDefault();
            //var last = list.LastOrDefault();

            //if (first.m == 0) first.m = 1;
            //if (first.d == 0) first.d = 1;
            //if (last.m == 0) last.m = 1;
            //if (last.d == 0) last.d = 1;

            //DateTime minDate = Convert.ToDateTime(string.Format("{0}-{1}-{2}", first.y, first.m, first.d));
            //DateTime maxDate = Convert.ToDateTime(string.Format("{0}-{1}-{2}", last.y, last.m, last.d));

            var newList = new List<T>();
            var curDate = begin_time;
            do
            {
                newList.Add(new T()
                {
                    y = curDate.Year,
                    m = curDate.Month,
                    d = curDate.Day
                });

                if (dateType == "y") curDate = curDate.AddYears(1);
                else if (dateType == "m") curDate = curDate.AddMonths(1);
                else if (dateType == "d") curDate = curDate.AddDays(1);

            } while (curDate <= end_time);

            foreach (var item in newList)
            {
                var _tempObj = list.Where(w => w.y == item.y && w.m == item.m && w.d == item.d).FirstOrDefault();
                if (_tempObj != null)
                    item.total = _tempObj.total;
            }

            return newList;
        }

        /// <summary>
        /// 新增用户统计
        /// </summary>
        /// <param name="dateType">统计类型，y、m、d</param>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public IList<Models.Response.Statistics.DateStatistics> BookUserStatistics(string dateType, DateTime begin_time, DateTime end_time)
        {
            CheckImputParams(dateType, begin_time, end_time);

            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            switch (dateType)
            {
                case "y":
                    //--按年统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(Create_Time, '%Y') as 'y',");
                    sbSql.Append(" count(1) as total");
                    sbSql.Append(" from book_user");
                    sbSql.Append(" where Create_Time between @begin_time and @end_time");
                    sbSql.Append(" group by date_format(Create_Time, '%Y')");
                    sbSql.Append(" order by Create_Time asc;");
                    break;
                case "m":
                    //--按月统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(Create_Time, '%Y') as 'y',");
                    sbSql.Append(" date_format(Create_Time, '%m') as 'm',");
                    sbSql.Append(" count(1) as total");
                    sbSql.Append(" from book_user");
                    sbSql.Append(" where Create_Time between @begin_time and @end_time");
                    sbSql.Append(" group by date_format(Create_Time, '%Y%m')");
                    sbSql.Append(" order by Create_Time asc;");
                    break;
                case "d":
                    //--按日统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(Create_Time, '%Y') as 'y',");
                    sbSql.Append(" date_format(Create_Time, '%m') as 'm',");
                    sbSql.Append(" date_format(Create_Time, '%d') as 'd',");
                    sbSql.Append(" count(1) as total");
                    sbSql.Append(" from book_user");
                    sbSql.Append(" where Create_Time between @begin_time and @end_time");
                    sbSql.Append(" group by date_format(Create_Time, '%Y%m%d')");
                    sbSql.Append(" order by Create_Time asc;");
                    break;
            }
            parameters.Add(new MySqlParameter("@begin_time", begin_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            parameters.Add(new MySqlParameter("@end_time", end_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            var list = SqlService.SqlQuery<Models.Response.Statistics.DateStatistics>(sbSql.ToString(), parameters.ToArray());
            var newList = FillDate(dateType, begin_time, end_time, list);
            return newList;
        }

        /// <summary>
        /// 新增小说用户统计
        /// </summary>
        /// <param name="dateType">统计类型，y、m、d</param>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public IList<Models.Response.Statistics.DateStatistics> BookUser2Statistics(string dateType, DateTime begin_time, DateTime end_time)
        {
            CheckImputParams(dateType, begin_time, end_time);

            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            switch (dateType)
            {
                case "y":
                    //--按年统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(Create_Time,'%Y') as 'y',");
                    sbSql.Append(" count(distinct(Openid)) as total");
                    sbSql.Append(" from book_user");
                    sbSql.Append(" where Create_Time between @begin_time and @end_time");
                    sbSql.Append(" and Openid in (select distinct(Openid) from book_chapter_read_record)");
                    sbSql.Append(" group by date_format(Create_Time,'%Y')");
                    sbSql.Append(" order by Create_Time asc;");
                    break;
                case "m":
                    //--按月统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(Create_Time,'%Y') as 'y',");
                    sbSql.Append(" date_format(Create_Time,'%m') as 'm',");
                    sbSql.Append(" count(distinct(Openid)) as total ");
                    sbSql.Append(" from book_user ");
                    sbSql.Append(" where Create_Time between @begin_time and @end_time");
                    sbSql.Append(" and Openid in (select distinct(Openid) from book_chapter_read_record)");
                    sbSql.Append(" group by date_format(Create_Time,'%Y%m')");
                    sbSql.Append(" order by Create_Time asc;");
                    break;
                case "d":
                    //--按日统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(Create_Time,'%Y') as 'y',");
                    sbSql.Append(" date_format(Create_Time,'%m') as 'm',");
                    sbSql.Append(" date_format(Create_Time,'%d') as 'd',");
                    sbSql.Append(" count(distinct(Openid)) as total");
                    sbSql.Append(" from book_user");
                    sbSql.Append(" where Create_Time between @begin_time and @end_time");
                    sbSql.Append(" and Openid in (select distinct(Openid) from book_chapter_read_record)");
                    sbSql.Append(" group by date_format(Create_Time,'%Y%m%d')");
                    sbSql.Append(" order by Create_Time asc;");
                    break;
            }
            parameters.Add(new MySqlParameter("@begin_time", begin_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            parameters.Add(new MySqlParameter("@end_time", end_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            var list = SqlService.SqlQuery<Models.Response.Statistics.DateStatistics>(sbSql.ToString(), parameters.ToArray());
            var newList = FillDate(dateType, begin_time, end_time, list);
            return newList;
        }

        /// <summary>
        /// 时间段（每小时）新增用户统计
        /// </summary>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public IList<Models.Response.Statistics.HourStatistics> BookUserTimeSlotStatistics(DateTime begin_time, DateTime end_time)
        {
            CheckImputParams(begin_time, end_time);

            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            sbSql.Append(" select * from (");
            sbSql.Append(" select");
            sbSql.Append(" date_format(Create_Time,'%H') as 'h',");
            sbSql.Append(" count(Openid) as total");
            sbSql.Append(" from book_user");
            sbSql.Append(" where Create_Time between @begin_time and @end_time");
            sbSql.Append(" group by date_format(Create_Time,'%H')");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.h asc;");

            parameters.Add(new MySqlParameter("@begin_time", begin_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            parameters.Add(new MySqlParameter("@end_time", end_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            var list = SqlService.SqlQuery<Models.Response.Statistics.HourStatistics>(sbSql.ToString(), parameters.ToArray());
            var newList = new List<Models.Response.Statistics.HourStatistics>();

            for (var i = 0; i < 24; i++)
            {
                newList.Add(new Models.Response.Statistics.HourStatistics()
                {
                    h = i,
                    total = (list.Where(w => w.h == i).FirstOrDefault() ?? new Models.Response.Statistics.HourStatistics()).total
                });
            }

            return newList.OrderBy(o => o.h).ToList();
        }

        /// <summary>
        /// 新增用户 （男女分组）统计
        /// </summary>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public IList<Models.Response.Statistics.FlagStatistics> BookUserGenderStatistics(DateTime begin_time, DateTime end_time)
        {
            CheckImputParams(begin_time, end_time);

            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            sbSql.Append(" select Gender as flag, count(1) as total");
            sbSql.Append(" from book_user");
            sbSql.Append(" where Create_Time between @begin_time and @end_time");
            sbSql.Append(" group by Gender;");

            parameters.Add(new MySqlParameter("@begin_time", begin_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            parameters.Add(new MySqlParameter("@end_time", end_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            var list = SqlService.SqlQuery<Models.Response.Statistics.FlagStatistics>(sbSql.ToString(), parameters.ToArray());
            return list.OrderBy(o => o.flag).ToList();
        }

        /// <summary>
        /// 有阅读记录的用户数
        /// </summary>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public int GetCountByReadUser(DateTime begin_time, DateTime end_time)
        {
            CheckImputParams(begin_time, end_time);

            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            sbSql.Append(" select '有阅读记录的用户数' as flag, count(1) as total");
            sbSql.Append(" from book_user");
            sbSql.Append(" where Create_Time between @begin_time and @end_time");
            sbSql.Append(" and Openid in (select distinct(Openid) from book_read_record);");

            parameters.Add(new MySqlParameter("@begin_time", begin_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            parameters.Add(new MySqlParameter("@end_time", end_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            var list = SqlService.SqlQuery<Models.Response.Statistics.FlagStatistics>(sbSql.ToString(), parameters.ToArray());
            return list.FirstOrDefault().total;
        }

        /// <summary>
        /// 用户阅读统计（参与阅读用户数、阅读章节数）
        /// </summary>
        /// <param name="dateType">统计类型，y、m、d</param>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public IList<Models.Response.Statistics.BookUserReadStatistics> BookUserReadStatistics(string dateType, DateTime begin_time, DateTime end_time)
        {
            CheckImputParams(dateType, begin_time, end_time);

            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            switch (dateType)
            {
                case "y":
                    //--按年统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(Create_Time,'%Y') as 'y',");
                    sbSql.Append(" count(distinct(Openid)) as users,");
                    sbSql.Append(" count(Openid) as chapters");
                    sbSql.Append(" from book_chapter_read_record");
                    sbSql.Append(" where Create_Time between @begin_time and @end_time");
                    sbSql.Append(" group by date_format(Create_Time,'%Y')");
                    sbSql.Append(" order by Create_Time asc;");
                    break;
                case "m":
                    //--按月统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(Create_Time,'%Y') as 'y',");
                    sbSql.Append(" date_format(Create_Time,'%m') as 'm',");
                    sbSql.Append(" count(distinct(Openid)) as users,");
                    sbSql.Append(" count(Openid) as chapters");
                    sbSql.Append(" from book_chapter_read_record");
                    sbSql.Append(" where Create_Time between @begin_time and @end_time");
                    sbSql.Append(" group by date_format(Create_Time,'%Y%m')");
                    sbSql.Append(" order by Create_Time asc;");
                    break;
                case "d":
                    //--按日统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(Create_Time,'%Y') as 'y',");
                    sbSql.Append(" date_format(Create_Time,'%m') as 'm',");
                    sbSql.Append(" date_format(Create_Time,'%d') as 'd',");
                    sbSql.Append(" count(distinct(Openid)) as users,");
                    sbSql.Append(" count(Openid) as chapters");
                    sbSql.Append(" from book_chapter_read_record");
                    sbSql.Append(" where Create_Time between @begin_time and @end_time");
                    sbSql.Append(" group by date_format(Create_Time,'%Y%m%d')");
                    sbSql.Append(" order by Create_Time asc;");
                    break;
            }
            parameters.Add(new MySqlParameter("@begin_time", begin_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            parameters.Add(new MySqlParameter("@end_time", end_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            var list = SqlService.SqlQuery<Models.Response.Statistics.BookUserReadStatistics>(sbSql.ToString(), parameters.ToArray());
            var newList = FillDate(dateType, begin_time, end_time, list);

            foreach (var item in newList)
            {
                var _tempObj = list.Where(w => w.y == item.y && w.m == item.m && w.d == item.d).FirstOrDefault();
                if (_tempObj != null)
                {
                    item.users = _tempObj.users;
                    item.chapters = _tempObj.chapters;
                }
            }

            return newList;
        }

        /// <summary>
        /// 时间段（每小时）阅读小说数 统计
        /// </summary>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public IList<Models.Response.Statistics.HourStatistics> BookReadTimeSlotStatistics(DateTime begin_time, DateTime end_time)
        {
            CheckImputParams(begin_time, end_time);

            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            sbSql.Append(" select * from (");
            sbSql.Append(" select");
            sbSql.Append(" date_format(Create_Time,'%H') as 'h',");
            sbSql.Append(" count(distinct(Book_Read_Record_Id)) as total");
            sbSql.Append(" from book_chapter_read_record");
            sbSql.Append(" where Create_Time between @begin_time and @end_time");
            sbSql.Append(" group by date_format(Create_Time,'%H')");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.h asc;");

            parameters.Add(new MySqlParameter("@begin_time", begin_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            parameters.Add(new MySqlParameter("@end_time", end_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            var list = SqlService.SqlQuery<Models.Response.Statistics.HourStatistics>(sbSql.ToString(), parameters.ToArray());
            var newList = new List<Models.Response.Statistics.HourStatistics>();

            for (var i = 0; i < 24; i++)
            {
                newList.Add(new Models.Response.Statistics.HourStatistics()
                {
                    h = i,
                    total = (list.Where(w => w.h == i).FirstOrDefault() ?? new Models.Response.Statistics.HourStatistics()).total
                });
            }

            return newList.OrderBy(o => o.h).ToList();
        }

        /// <summary>
        /// 时间段（每小时）阅读小说章节数 统计
        /// </summary>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public IList<Models.Response.Statistics.HourStatistics> BookReadChapterTimeSlotStatistics(DateTime begin_time, DateTime end_time)
        {
            CheckImputParams(begin_time, end_time);

            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            sbSql.Append(" select * from (");
            sbSql.Append(" select");
            sbSql.Append(" date_format(Create_Time,'%H') as 'h',");
            sbSql.Append(" count(1) as total");
            sbSql.Append(" from book_chapter_read_record");
            sbSql.Append(" where Create_Time between @begin_time and @end_time");
            sbSql.Append(" group by date_format(Create_Time,'%H')");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.h asc;");

            parameters.Add(new MySqlParameter("@begin_time", begin_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            parameters.Add(new MySqlParameter("@end_time", end_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            var list = SqlService.SqlQuery<Models.Response.Statistics.HourStatistics>(sbSql.ToString(), parameters.ToArray());
            var newList = new List<Models.Response.Statistics.HourStatistics>();

            for (var i = 0; i < 24; i++)
            {
                newList.Add(new Models.Response.Statistics.HourStatistics()
                {
                    h = i,
                    total = (list.Where(w => w.h == i).FirstOrDefault() ?? new Models.Response.Statistics.HourStatistics()).total
                });
            }

            return newList.OrderBy(o => o.h).ToList();
        }

        /// <summary>
        /// 时间段（每小时）参与阅读的用户数 统计
        /// </summary>
        /// <param name="begin_time"></param>
        /// <param name="end_time"></param>
        /// <returns></returns>
        public IList<Models.Response.Statistics.HourStatistics> BookUserReadTimeSlotStatistics(DateTime begin_time, DateTime end_time)
        {
            CheckImputParams(begin_time, end_time);

            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            sbSql.Append(" select * from (");
            sbSql.Append(" select");
            sbSql.Append(" date_format(Create_Time,'%H') as 'h',");
            sbSql.Append(" count(distinct(Openid)) as total");
            sbSql.Append(" from book_chapter_read_record");
            sbSql.Append(" where Create_Time between @begin_time and @end_time");
            sbSql.Append(" group by date_format(Create_Time,'%H')");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.h asc;");

            parameters.Add(new MySqlParameter("@begin_time", begin_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            parameters.Add(new MySqlParameter("@end_time", end_time.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            var list = SqlService.SqlQuery<Models.Response.Statistics.HourStatistics>(sbSql.ToString(), parameters.ToArray());
            var newList = new List<Models.Response.Statistics.HourStatistics>();

            for (var i = 0; i < 24; i++)
            {
                newList.Add(new Models.Response.Statistics.HourStatistics()
                {
                    h = i,
                    total = (list.Where(w => w.h == i).FirstOrDefault() ?? new Models.Response.Statistics.HourStatistics()).total
                });
            }

            return newList.OrderBy(o => o.h).ToList();
        }

        /// <summary>
        /// 最受欢迎小说排行 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<Models.Response.Statistics.FlagStatistics> GetMostPopularBook(int top = 50)
        {
            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            sbSql.Append(" select * from (");
            sbSql.Append(" select Book_Name as flag, count(distinct(Openid)) as total");
            sbSql.Append(" from book_read_record");
            sbSql.Append(" group by Book_Name");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.total desc");
            sbSql.Append(" limit @top");

            parameters.Add(new MySqlParameter("@top", top));

            var list = SqlService.SqlQuery<Models.Response.Statistics.FlagStatistics>(sbSql.ToString(), parameters.ToArray());
            return list.OrderByDescending(o => o.total).ThenBy(t => t.flag.GetHashCode()).ToList();
        }

        /// <summary>
        /// 阅读排行榜 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<Models.Response.Statistics.ReadRankingStatistics> GetReadRanking(int top = 50)
        {
            StringBuilder sbSql = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            sbSql.Append(" select *,");
            sbSql.Append(" (select NickName from book_user where book_user.Openid = t.Openid limit 1) as NickName");
            sbSql.Append(" from (");
            sbSql.Append(" select Openid, count(Openid) as total");
            sbSql.Append(" from book_chapter_read_record");
            sbSql.Append(" group by Openid");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.total desc");
            sbSql.Append(" limit @top");

            parameters.Add(new MySqlParameter("@top", top));

            var list = SqlService.SqlQuery<Models.Response.Statistics.ReadRankingStatistics>(sbSql.ToString(), parameters.ToArray());
            return list.OrderByDescending(o => o.total).ThenBy(t => t.Openid).ToList();
        }

    }
}
