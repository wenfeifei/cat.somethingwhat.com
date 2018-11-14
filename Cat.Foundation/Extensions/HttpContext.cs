using System;
using System.Collections.Generic;
using System.Text;

namespace Cat
{
    public static class HttpContext
    {
        /// <summary>
        /// 是否为LocalHost请求（GET）
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsLocalHostRequest(this Microsoft.AspNetCore.Http.HttpContext context)
        {
            return context.Request.Host.Host.ToString().ToLower().Equals("localhost");
        }
        /// <summary>
        /// 是否为ajax请求（GET）
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this Microsoft.AspNetCore.Http.HttpContext context)
        {
            return "XMLHttpRequest".Equals(context.Request.Headers["X-Requested-With"]);
        }
        /// <summary>
        /// 是否为微信小程序请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsMiniprogramRequest(this Microsoft.AspNetCore.Http.HttpContext context)
        {
            return "miniprogram".Equals(context.Request.Headers["App-From"]);
        }
    }
}
