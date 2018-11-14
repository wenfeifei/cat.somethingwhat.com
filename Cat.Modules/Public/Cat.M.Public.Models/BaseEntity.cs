using Cat.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cat.M.Public.Models
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
        /// 更新时间
        /// </summary>
        public DateTime? Update_Time { get; set; }
    }
}
