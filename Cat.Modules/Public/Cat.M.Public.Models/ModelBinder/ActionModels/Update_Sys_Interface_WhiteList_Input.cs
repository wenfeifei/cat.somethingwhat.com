using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ActionModels
{
    /// <summary>
    /// 用于“更新”的参数实体
    /// </summary>
    public class Update_Sys_Interface_WhiteList_Input
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
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
