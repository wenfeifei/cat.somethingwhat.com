using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ReturnModels.Wechat
{
    /// <summary>
    /// 微信小程序调用接口后返回的异常信息
    /// </summary>
    public class ErrorRequest
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
}
