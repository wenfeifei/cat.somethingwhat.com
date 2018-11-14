using Cat.Foundation;
using Cat.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

using AllLogServices = Cat.M.Log.Services.AllServices;

namespace Cat.Web.Support.Filter
{
    /// <summary>
    /// 异常信息 筛选器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    sealed class CatErrorFilterAttribute : Attribute, IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            //获取异常对象
            Exception ex = context.Exception;

            //记录错误日志
            AllLogServices.SysExceptionLogService.AddLog(M.Log.Services.Enum.ExceptionType.Exception, ex, "CatErrorFilterAttribute");
            //记录请求日志
            AllLogServices.SysHttpRequestLogService.AddLog();

            //context.ExceptionHandled = true;
            //base.OnException(filterContext);

            /*
            if (context.HttpContext.IsAjaxRequest()
                || context.HttpContext.IsMiniprogramRequest()
                || context.HttpContext.Request.Method.ToUpper() == "POST")
            {
                //如果是ajax请求或post请求，返回值类型均为 ActionRes
                ContentResult content = new ContentResult();
                content.Content = ActionRes.Fail(ex).ToJson();
                content.ContentType = "application/json; charset=utf-8";
                context.Result = content;
            }
            else
            {
                ////直接跳转到错误页
                //context.Result = new RedirectResult("/Home/Error");
            }
            */

            ContentResult content = new ContentResult();
            content.Content = ActionRes.Fail(ex).ToJson();
            content.ContentType = "application/json; charset=utf-8";
            context.Result = content;
        }


    }
}
