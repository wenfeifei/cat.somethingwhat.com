using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Models.Table
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class Sys_Action_Log: BaseEntity
    {
        /// <summary>
        /// 日志级别（枚举）
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
