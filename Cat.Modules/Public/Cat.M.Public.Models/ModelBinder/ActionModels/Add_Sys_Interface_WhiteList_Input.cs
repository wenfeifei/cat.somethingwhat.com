using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ActionModels
{
    /// <summary>
    /// 用于“新增”的参数实体
    /// </summary>
    public class Add_Sys_Interface_WhiteList_Input
    {
        /// <summary>
        /// Appid
        /// </summary>
        public string Appid { get; set; }
        /// <summary>
        /// 白名单的有效期
        /// </summary>
        public DateTime? Validity_Time { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
