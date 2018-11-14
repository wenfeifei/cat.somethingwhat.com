using Cat.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cat.M.Log.Models
{
    public class BaseEntity
    {
        /// <summary>
        /// 主键列
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Create_Time { get; set; }
        /// <summary>
        /// 为追踪日志（Trace）提供针对当前请求的唯一标识
        /// </summary>
        public string TraceIdentifier { get; set; }
    }
}
