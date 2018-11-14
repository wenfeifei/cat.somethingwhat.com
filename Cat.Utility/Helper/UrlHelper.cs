using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Utility
{
    public static class UrlHelper
    {
        /// <summary>
        /// 获取http://www.baidu.com?a=1的http://www.baidu.com部分
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrlDomain(string url)
        {
            Uri uri = new Uri(url);
            return uri.Scheme + "://" + uri.Host;
        }
    }
}
