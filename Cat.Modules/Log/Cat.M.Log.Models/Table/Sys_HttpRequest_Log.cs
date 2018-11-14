using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Models.Table
{
    /// <summary>
    /// http请求日志
    /// </summary>
    public class Sys_HttpRequest_Log : BaseEntity
    {
        public string Host { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public string Referer { get; set; }
        public string ContentType { get; set; }
        public List<Dictionary<string, string>> RequestParams { get; set; }
        //public string Cookie { get; set; }
        public string User_Agent { get; set; }
        public string RemoteIpAddress { get; set; }
        public string Remark { get; set; }
    }
}
