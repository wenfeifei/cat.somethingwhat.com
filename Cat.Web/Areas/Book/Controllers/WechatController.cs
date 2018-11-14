using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Utility;
using Microsoft.AspNetCore.Mvc;
using Cat.Web.Support.Filter;

using AllPublicServices = Cat.M.Public.Services.AllServices;
using AllLogServices = Cat.M.Log.Services.AllServices;

namespace Cat.Web.Areas.Book.Controllers
{
    [BookAuthorizeFilter(Anonymous = true)]
    public class WechatController : BaseController
    {
        /// <summary>
        /// 登录凭证校验
        /// </summary>
        /// <param name="appKey">对应数据库表Wechat_App_Config中的Mark_Key配置值</param>
        /// <param name="js_code"></param>
        /// <returns>{"session_key":"gAtKIuM6mn8F50iSUCZE9w==","openid":"o0LDq4njwCCuQuj3FOIZeLFDrc9o"}</returns>
        public IActionResult Jscode2session(int appKey, string js_code)
        {
            var response = AllPublicServices.WechatHelper.Jscode2session((Enums.Wechat.AppKey)appKey, js_code);
            if (response.IndexOf("invalid") > -1)
            {
                //记录错误日志
                AllLogServices.SysExceptionLogService.AddLog(M.Log.Services.Enum.ExceptionType.Exception, new Exception(response), "CatErrorFilterAttribute");
                //记录请求日志
                AllLogServices.SysHttpRequestLogService.AddLog();
            }

            if (response.IndexOf("session_key") > -1 && response.IndexOf("openid") > -1)
            {
                var tempModel = Serializer.JsonDeserialize<TempModel>(response);
                tempModel.token = AesHelper.AesEncrypt(new Cat.M.Book.Models.ModelBinder.ReturnModels.BookAuth() { Openid = tempModel.openid, LoginTime = DateTime.Now }.ToJson());
                response = Serializer.JsonSerialize(tempModel);
            }

            return Content(response);
        }

        /// <summary>
        /// 登录凭证校验（用作测试的）
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="js_code"></param>
        /// <returns>{"session_key":"gAtKIuM6mn8F50iSUCZE9w==","openid":"o0LDq4njwCCuQuj3FOIZeLFDrc9o"}</returns>
        public IActionResult Jscode2session_Test(string appid, string secret, string js_code)
        {
            var url = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", appid, secret, js_code);
            var response = HttpHelper.Get(url);
            //return response;


            //var response = AllPublicServices.WechatHelper.Jscode2session((Enums.Wechat.AppKey)appKey, js_code);
            if (response.IndexOf("invalid") > -1)
            {
                //记录错误日志
                AllLogServices.SysExceptionLogService.AddLog(M.Log.Services.Enum.ExceptionType.Exception, new Exception(response), "CatErrorFilterAttribute");
                //记录请求日志
                AllLogServices.SysHttpRequestLogService.AddLog();
            }

            if (response.IndexOf("session_key") > -1 && response.IndexOf("openid") > -1)
            {
                var tempModel = Serializer.JsonDeserialize<TempModel>(response);
                tempModel.token = AesHelper.AesEncrypt(new Cat.M.Book.Models.ModelBinder.ReturnModels.BookAuth() { Openid = tempModel.openid, LoginTime = DateTime.Now }.ToJson());
                response = Serializer.JsonSerialize(tempModel);
            }

            return Content(response);
        }

        private class TempModel
        {
            public string session_key { get; set; }
            public string openid { get; set; }
            public string token { get; set; }
        }
    }
}