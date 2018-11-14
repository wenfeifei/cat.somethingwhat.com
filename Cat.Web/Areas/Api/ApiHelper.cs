using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cat.Web.Areas.Api
{
    public class ApiHelper
    {
        /// <summary>
        /// 获取token（localStorage、cookie、url）
        /// </summary>
        public static string AuthToken
        {
            get
            {
                var key = $"{Cat.Foundation.ConfigManager.CatSettings.ProjectPrefix}-antd-pro-token";

                //step1: 先从header中获取
                var _str = Cat.Foundation.CatContext.HttpContext.Request.Headers[key];

                //step2: 再从cookie中获取
                if (string.IsNullOrEmpty(_str))
                {
                    _str = Cat.Foundation.CatContext.HttpContext.Request.Cookies[key];
                }

                //step3: 再从url参数中获取
                if (string.IsNullOrEmpty(_str))
                {
                    _str = Cat.Foundation.CatContext.HttpContext.Request.GetParam("token");
                }

                return _str;
            }
        }
    }
}
