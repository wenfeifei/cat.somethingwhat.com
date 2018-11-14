using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Models.Table
{
    /// <summary>
    /// 异常日志
    /// </summary>
    public class Sys_Exception_Log: BaseEntity
    {
        /// <summary>
        /// 异常分类（枚举）
        /// </summary>
        public string ExceptionType { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 堆栈信息
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 内部异常信息
        /// </summary>
        public string InnerException { get; set; }
        /// <summary>
        /// Source
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// TargetSite
        /// </summary>
        public string TargetSite { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
