using System;

namespace Cat.Utility
{
    /// <summary>
    /// 日期时间扩展类
    /// </summary>
    public static class DateTimeExtension
    {

        /// <summary>
        /// 返回是星期几(汉字)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetWeekByChinese(this DateTime dt)
        {
            string week = string.Empty;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    week = "星期四";
                    break;
                case DayOfWeek.Friday:
                    week = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    week = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    week = "星期日";
                    break;
            }
            return week;
        }

    }
}
