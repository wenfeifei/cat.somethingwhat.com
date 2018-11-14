using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cat.Web.Areas.Api.Models.Response
{
    /// <summary>
    /// 异常代码
    /// </summary>
    public enum ErrorCode
    {
        Default = -1,

        /// <summary>
        /// 用户没有权限
        /// </summary>
        user_no_authority = -1000,
        /// <summary>
        /// 用户找不到了
        /// </summary>
        user_not_found = -1001,
        /// <summary>
        /// 登录凭证已过期，您需要重新登录
        /// </summary>
        user_logon_overdue = -1101,
        /// <summary>
        /// 用户密码被修改了
        /// </summary>
        user_pwd_modified = -1102,
        /// <summary>
        /// 用户被禁用了
        /// </summary>
        user_disabled = -1103,

    }
}
