using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Public.Services.Constants;
using Cat.Web.Areas.Api.Models.Param;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Api.Controllers
{
    /// <summary>
    /// 权限（用户身份）验证相关
    /// </summary>
    public class AuthController : BaseController
    {
        /// <summary>
        /// 获取所有权限分组
        /// </summary>
        /// <returns></returns>
        public ActionRes GetAllAuthority()
        {
            var list = Cat.M.Public.Services.Constants.Authority.AllAuthority;
            return ActionRes.Success(list);
        }

        #region 检查当前登录用户对于指定的控制器和方法有无访问权限

        /// <summary>
        /// 检查当前登录用户对于指定的控制器和方法有无访问权限
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionRes CheckAuthority([FromBody]List_Input<CheckAuthorityModel> model)
        {
            if (model.list == null || model.list.Count == 0) throw new Exception("参数有误");

            foreach (var item in model.list)
            {
                item.result = CheckAuthorityMethod(item.controller, item.method);
            }

            return ActionRes.Success(model.list);
        }

        private bool CheckAuthorityMethod(string controller, string method)
        {
            try
            {
                if (base.IsAdministrator)
                    return true;
                else
                {
                    var authAttr = GetApiAuthAttr(controller, method);
                    if (authAttr != null && authAttr.AuthorityIdentity == AuthorityIdentityEnum.Administrator)
                        return false;
                }
            }
            catch
            {
                return true;
            }
            return true;
        }

        private ApiAuthorizeFilterAttribute GetApiAuthAttr(string controller, string method)
        {
            var assembly = Assembly.Load("Cat.Web").CreateInstance($"Cat.Web.Areas.Api.Controllers.{controller}Controller");
            if (assembly == null) throw new Exception("没有找到匹配的控制器");

            var t = assembly.GetType();

            var m = t.GetMethod(method);
            if (m == null) throw new Exception("没有找到匹配的方法");

            var attr = m.GetCustomAttributes(Type.GetType("Cat.Web.Areas.Api.ApiAuthorizeFilterAttribute")).FirstOrDefault();
            if (attr == null) throw new Exception("根据控制器和方法没有找到指定的特性");

            return attr as ApiAuthorizeFilterAttribute;
        }

        public class CheckAuthorityModel
        {
            public string controller { get; set; }
            public string method { get; set; }
            public bool result { get; set; }
        }

        #endregion

    }
}