using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Models.Table
{
    /// <summary>
    /// 日志实体类
    /// </summary>
    public class Sys_Log : BaseEntity
    {
        /// <summary>
        /// 日志级别（枚举）
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 详细信息
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// Exception
        /// </summary>
        public Exception ex { get; set; }
    }
}
