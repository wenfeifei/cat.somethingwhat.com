using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ActionModels
{
    /// <summary>
    /// 用于“更新”的参数实体
    /// </summary>
    public class Update_Sys_Account_Input
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 权限分组，多个用逗号分割
        /// </summary>
        public List<string> Authority { get; set; }
        ///// <summary>
        ///// 用户id
        ///// </summary>
        //public string User_Id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Avatar
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 账户是否被禁用
        /// </summary>
        public bool? Disable { get; set; }
    }
}
