using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.Table
{
    /// <summary>
    /// 接口白名单
    /// </summary>
    public partial class Sys_Interface_WhiteList : BaseEntity
    {
        /// <summary>
        /// Appid
        /// </summary>
        public string Appid { get; set; }
        /// <summary>
        /// 白名单的有效期
        /// </summary>
        public DateTime Validity_Time { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
