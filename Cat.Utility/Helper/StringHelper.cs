using System;

namespace Cat.Utility
{
    public static class StringHelper
    {
        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <returns></returns>  
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            string res = string.Format("{0:x}", i - DateTime.Now.Ticks);
            return res.PadRight(16, '0'); //如果不足16位就用0补齐
        }

        /// <summary>
        /// 获取排序号（1970/01/01 到现在的秒数）
        /// </summary>
        /// <returns></returns>
        public static long GetSortNum()
        {
            DateTime beginTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime endTime = DateTime.Now;
            double second = (endTime - beginTime).TotalSeconds;
            return (long)second;
        }

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

        /// <summary>
        /// 根据GUID获取19位的唯一数字序列
        /// </summary>
        /// <returns></returns>
        public static Int64 GetUUID()
        {
            return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
        }
    }
}
