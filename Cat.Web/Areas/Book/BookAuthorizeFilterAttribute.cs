using Cat.Foundation;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Utility;

namespace Cat.Web.Areas.Book
{
    /// <summary>
    /// 认证&授权 筛选器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class BookAuthorizeFilterAttribute : Attribute, IAuthorizationFilter
    {

        /// <summary>
        /// 匿名登陆
        /// </summary>
        public bool Anonymous { get; set; } = false;

        FilterContextInfo filterContextInfo;

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            filterContextInfo = new FilterContextInfo(filterContext);

            if (filterContextInfo.ControllerName == "Base")
                throw new Exception("禁止访问基类控制器");

            //不用检查
            if (Anonymous)
            {
                return;
            }

            #region  检查认证
            try
            {
                string token = filterContext.HttpContext.Request.Headers["cat-token"];
                try
                {
                    if (string.IsNullOrEmpty(token)) throw new Exception("用户身份认证未通过[token不能为空]，请求数据失败");

                    token = AesHelper.AesDecrypt(token);
                    var auth = Serializer.JsonDeserialize<Cat.M.Book.Models.ModelBinder.ReturnModels.BookAuth>(token);
                    if (string.IsNullOrEmpty(auth.Openid)) throw new Exception("用户身份认证未通过[找不到指定的openid]，请求数据失败");
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(ex.Message))
                        throw new Exception("用户身份认证未通过，请求数据失败");
                    else
                        throw ex;
                }
            }
            catch (Exception ex)
            {
                Microsoft.AspNetCore.Mvc.ContentResult contentResult = new Microsoft.AspNetCore.Mvc.ContentResult();
                contentResult.Content = ActionRes.Fail(ex.Message).ToJson();
                filterContext.Result = contentResult;
                return;
            }
            #endregion
        }

        public class FilterContextInfo
        {
            public FilterContextInfo(AuthorizationFilterContext filterContext)
            {
                //获取 ControllerName 名称
                ControllerName = filterContext.RouteData.Values["controller"].ToString();
                //获取 Action 名称
                ActionName = filterContext.RouteData.Values["action"].ToString();
            }
            /// <summary>
            /// 获取 ControllerName 名称
            /// </summary>
            public string ControllerName { get; set; }
            /// <summary>
            /// 获取 Action 名称
            /// </summary>
            public string ActionName { get; set; }
        }

    }
}