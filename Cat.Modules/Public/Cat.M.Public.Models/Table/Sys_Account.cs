using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cat.M.Public.Models.Table
{
    /// <summary>
    /// 后台账户信息
    /// </summary>
    public partial class Sys_Account : BaseEntity
    {
        /// <summary>
        /// 权限分组，多个用逗号分割
        /// </summary>
        public string Authority { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string User_Id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Password Salt
        /// </summary>
        public string Password_Salt { get; set; }
        /// <summary>
        /// Avatar
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 账户是否被禁用
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? Disable { get; set; }
    }
}
