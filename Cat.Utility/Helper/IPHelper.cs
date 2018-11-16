using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Cat.Utility
{
    /// <summary>
    /// 新浪IP查询
    /// </summary>
    public static class IPHelper
    {
        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                return Cat.Foundation.CatContext.HttpContext;
            }
        }

        /// <summary>
        /// 取得客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string ip = string.Empty;

            //ip = Current.Request.Headers["X-Original-For"];

            ip = Current.Request.HttpContext.Connection.RemoteIpAddress.ToString();

            if (IsLocalIP(ip))
            {
                ip = Current.Request.Headers["X-Forwarded-For"];
            }

            return ip;
        }

        private static bool IsLocalIP(string ip)
        {
            return ip.Equals("::1") || ip.Equals("127.0.0.1") || ip.StartsWith("192.168.") || ip.StartsWith("172.16.") || ip.StartsWith("10.");
        }

        /// <summary>
        /// 取得客户端外网IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientWebIP()
        {
            try
            {
                const string ip138ComIcAsp = "http://scripts.hashemian.com/js/visitorIPHOST.js.php"; //查询外网IP
                var uri = new Uri(ip138ComIcAsp);
                WebRequest wr = WebRequest.Create(uri);
                Stream stream = wr.GetResponse().GetResponseStream();
                if (stream != null)
                {
                    var reader = new StreamReader(stream, Encoding.Default);
                    string result = reader.ReadToEnd();
                    var regex = new Regex("(?<First>2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.(?<Second>2[0-4]\\d" +
                                          "|25[0-5]|[01]?\\d\\d?)\\.(?<Third>2[0-4]\\d|25[0-5]|[01]?\\d" +
                                          "\\d?)\\.(?<Fourth>2[0-4]\\d|25[0-5]|[01]?\\d\\d?)",
                                          RegexOptions.IgnoreCase | RegexOptions.CultureInvariant |
                                          RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
                    Match ip = regex.Matches(result)[0];
                    return ip.ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }

        public static IpDetail GetIpDetail()
        {
            var model = new IpDetail();

            string ip = GetClientIP();
            if (ip.IsIP())
            {
                var _res = GetIpDetail(ip);
                if (_res != null)
                    model = _res;
            }
            model.ip = ip;
            return model ?? new IpDetail();
        }

        /// <summary>
        /// 获取IP地址的详细信息，调用的接口为
        /// http://ip.taobao.com/service/getIpInfo.php?ip={ip}
        /// </summary>
        /// <param name="ipAddress">请求分析得IP地址</param>
        /// <returns>IpUtils.IpDetail</returns>
        public static IpDetail GetIpDetail(string ipAddress)
        {
            var ipDetail = new IpDetail();
            try
            {
                string ip = ipAddress;
                Encoding sourceEncoding = Encoding.UTF8;
                using (
                    Stream receiveStream =
                        WebRequest.Create("http://ip.taobao.com/service/getIpInfo.php?ip=" +
                                          ipAddress).
                                   GetResponse().GetResponseStream())
                {
                    if (receiveStream != null)
                        using (var sr = new StreamReader(receiveStream, sourceEncoding))
                        {
                            var readbuffer = new char[256];
                            int n = sr.Read(readbuffer, 0, readbuffer.Length);
                            int realLen = 0;
                            while (n > 0)
                            {
                                realLen = n;
                                n = sr.Read(readbuffer, 0, readbuffer.Length);
                            }
                            ip = ConvertToGb(sourceEncoding.GetString(sourceEncoding.GetBytes(readbuffer, 0, realLen)));
                        }
                }
                try
                {
                    //ipDetail = Serializer.JsonDeserialize<IpDetail>(ip);IpDetailRes
                    //ipDetail.ip = ipAddress;
                    var entity = Serializer.JsonDeserialize<IpDetailRes>(ip);
                    if (entity.code == "0")
                    {
                        ipDetail = entity.data;
                        ipDetail.province = entity.data.region;
                    }
                }
                catch
                {
                    //ipDetail.city = "未知";
                }
            }
            catch (Exception ex)
            {
                //ipDetail.city = "未知";

            }
            return ipDetail;
        }

        /// <summary>
        /// 把Unicode解码为普通文字
        /// </summary>
        /// <param name="unicodeString">要解码的Unicode字符集</param>
        /// <returns>解码后的字符串</returns>
        public static string ConvertToGb(string unicodeString)
        {
            var regex = new Regex(@"\\u\w{4}");
            MatchCollection matchs = regex.Matches(unicodeString);
            foreach (Match match in matchs)
            {
                string tempvalue = char.ConvertFromUtf32(Convert.ToInt32(match.Value.Replace(@"\u", ""), 16));
                unicodeString = unicodeString.Replace(match.Value, tempvalue);
            }
            return unicodeString;
        }

        ///// <summary>
        ///// 取得本机Mac地址 
        ///// </summary>
        ///// <returns></returns>
        //public static string GetServerMac()
        //{
        //    string mac = "";
        //    var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //    ManagementObjectCollection mos = mc.GetInstances();
        //    foreach (ManagementObject mo in mos)
        //    {
        //        if ((bool)mo["IPEnabled"])
        //        {
        //            mac += mo["MACAddress"].ToString();
        //            return mac;
        //        }
        //    }
        //    return string.Empty;
        //}

        /// <summary>
        /// 取得客户端Mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientMac()
        {
            try
            {
                //待实现
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public class IpDetailRes
        {
            public string code { get; set; }
            public IpDetail data { get; set; }
        }

        public class IpDetail
        {
            /// <summary>
            /// ip
            /// </summary>
            public string ip { get; set; }
            /// <summary>
            /// 国家
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// 省份
            /// </summary>
            public string region { get; set; }
            /// <summary>
            /// 城市
            /// </summary>
            public string city { get; set; }
            /// <summary>
            /// 运营商
            /// </summary>
            public string isp { get; set; }

            public string area { get; set; }
            public string county { get; set; }
            public string country_id { get; set; }
            public string area_id { get; set; }
            public string region_id { get; set; }
            public string city_id { get; set; }
            public string county_id { get; set; }
            public string isp_id { get; set; }

            #region 以下属性只是为了兼容业务
            /// <summary>
            /// 省份
            /// </summary>
            public string province { get; set; }
            /// <summary>
            /// 地区（empty）
            /// </summary>
            public string district { get; set; }
            //
            public string desc { get; set; }
            #endregion

        }

    }

}