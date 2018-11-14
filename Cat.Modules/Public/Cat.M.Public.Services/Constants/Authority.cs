using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Services.Constants
{
    public class Authority
    {
        /// <summary>
        /// 获取所有权限分组
        /// </summary>
        public static List<string> AllAuthority
        {
            get
            {
                var list = new List<string>();
                list.Add("administrator");
                list.Add("guest");
                return list;
            }
        }

        public static string administrator => "administrator";
        public static string guest => "guest";
    }

    /// <summary>
    /// 身份验证类型
    /// </summary>
    public enum AuthorityIdentityEnum
    {
        /// <summary>
        /// 匿名用户
        /// </summary>
        Anonymous,
        /// <summary>
        /// 访客
        /// </summary>
        Guest,
        /// <summary>
        /// 管理员
        /// </summary>
        Administrator
    }
    
}
