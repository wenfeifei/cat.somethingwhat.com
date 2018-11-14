using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cat.Utility
{
    /// <summary>
    /// 排序帮助类
    /// </summary>
    public class SortHelper
    {
        /// <summary>
        /// 自定义排序
        /// 升序排序，如：1.jpg > 2.jpg ... > 10.jpg
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int CompareByFileName(string x, string y)
        {
            if (string.IsNullOrEmpty(x) && string.IsNullOrEmpty(y)) return 0;
            else if (string.IsNullOrEmpty(x)) return -1;
            else if (string.IsNullOrEmpty(y)) return 1;
            else
            {
                string _x = x.Substring(x.LastIndexOf("/") + 1);
                _x = _x.Substring(0, _x.LastIndexOf("."));
                string _y = y.Substring(y.LastIndexOf("/") + 1);
                _y = _y.Substring(0, _y.LastIndexOf("."));
                if (IsInt(_x) && IsInt(_y))
                {
                    return (Convert.ToInt32(_x) > Convert.ToInt32(_y)) ? 1 : -1;
                }
                else
                {
                    return _x.CompareTo(_y);
                }
            }
        }

        /// <summary>
        /// 是否为Int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[-]?\d+[.]?\d*$");
        }
    }
}