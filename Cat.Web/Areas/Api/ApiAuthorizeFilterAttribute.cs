using Cat.Foundation;
using Cat.M.Public.Services.Constants;
using Cat.M.Public.Services.Helper;
using Cat.Utility;
using Cat.Web.Areas.Api.Models.Response;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cat.Web.Areas.Api
{
    /// <summary>
    /// api 认证&授权 筛选器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ApiAuthorizeFilterAttribute : Attribute, IAuthorizationFilter
    {

        ///// <summary>
        ///// 匿名登陆
        ///// </summary>
        //public bool Anonymous { get; set; } = false;

        /// <summary>
        /// 身份验证类型（默认为验证guest登录）
        /// </summary>
        public AuthorityIdentityEnum AuthorityIdentity { get; set; }



        FilterContextInfo filterContextInfo;

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            filterContextInfo = new FilterContextInfo(filterContext);

            if (filterContextInfo.ControllerName == "Base")
                throw new Exception("禁止访问基类控制器");

            ////不用检查
            //if (Anonymous)
            //{
            //    return;
            //}

            //当类和方法都被标记【ApiAuthorizeFilterAttribute】，只取最后一个筛选器配置。即如果类和方法都被标记，则取的是方法上的筛选器
            var thisClassObj = filterContext.Filters.Where(w => w.ToString().EndsWith("ApiAuthorizeFilterAttribute")).Last() as ApiAuthorizeFilterAttribute;
            AuthorityIdentity = thisClassObj.AuthorityIdentity;

            //标记为“匿名”的方法或类不用检查
            if (AuthorityIdentity == AuthorityIdentityEnum.Anonymous)
            {
                return;
            }


            ErrorCode errorCode = ErrorCode.Default;

            #region
            try
            {
                try
                {
                    //检查认证
                    //string authority = filterContext.HttpContext.Request.Headers["cat-book-antd-pro-authority"];
                    //string userid = filterContext.HttpContext.Request.Headers["cat-book-antd-pro-userid"];
                    string token = ApiHelper.AuthToken;
                    if (string.IsNullOrEmpty(token))
                    {
                        errorCode = ErrorCode.user_no_authority;
                        throw new Exception("用户身份认证未通过[token不能为空]，请求数据失败");
                    }

                    token = AesHelper.AesDecrypt(token);
                    var auth = Serializer.JsonDeserialize<Cat.M.Book.Models.ModelBinder.ReturnModels.ApiAuth>(token);
                    if (string.IsNullOrEmpty(auth.User_Id))
                    {
                        errorCode = ErrorCode.user_no_authority;
                        throw new Exception("");
                    }
                    //if (auth.User_Id != userid) throw new Exception();

                    //检查用户状态
                    var user = Cat.M.Public.Services.AllServices.SysAccountService.GetSingle(w => w.User_Id == auth.User_Id);
                    if (user == null)
                    {
                        errorCode = ErrorCode.user_not_found;
                        throw new Exception("没有找到用户，可能已被删除");
                    }
                    if (user.Disable == true)
                    {
                        errorCode = ErrorCode.user_disabled;
                        throw new Exception("当前登录用户已被禁用，请找超级管理员解除");
                    }
                    if ((user.Password.Substring(0, 5) + user.Password.Substring(user.Password.Length - 5, 5)) != auth.Pwd_Incomplete)
                    {
                        errorCode = ErrorCode.user_pwd_modified;
                        throw new Exception("当前登录用户密码已修改，请重新登录");
                    }
                    if ((DateTime.Now - auth.LoginTime).TotalDays > Cat.Foundation.ConfigManager.CatSettings.LogonCredentialSaveDay)
                    {
                        errorCode = ErrorCode.user_logon_overdue;
                        throw new Exception("登录凭证已过期，您需要重新登录");
                    }

                    //检查授权
                    if (!user.Authority.Split(",", StringSplitOptions.RemoveEmptyEntries).Contains(AuthorityIdentityEnum.Administrator.ToString().ToLower()))
                    {
                        //当前登录用户没有管理员权限
                        if (AuthorityIdentity == AuthorityIdentityEnum.Administrator)
                        {
                            //当前访问的类或方法被标记为管理员
                            throw new Exception("当前登录用户没有权限进行此操作");
                        }
                    }

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
                contentResult.Content = ActionRes.Fail((int)errorCode, ex.Message).ToJson();
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