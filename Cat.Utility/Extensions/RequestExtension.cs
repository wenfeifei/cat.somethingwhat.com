using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat
{
    public static class RequestExtension
    {
        /// <summary>
        /// 获取get或post的参数值，post优先
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetParam(this Microsoft.AspNetCore.Http.HttpRequest httpRequest, string key)
        {
            if (httpRequest.Method == "GET")
                return httpRequest.Query[key].ToStr().Replace(" ", "+");
            else
                return httpRequest.Form[key].ToStr();
        }
    }
}
