using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Models.Table
{
    /// <summary>
    /// 接口访问记录
    /// </summary>
    public class Sys_Interface_Record : Sys_HttpRequest_Log
    {
        public long Millisecond { get; set; }
        public string RecordType { get; set; }
    }
}
