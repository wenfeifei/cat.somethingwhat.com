using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cat.Foundation;

using AllBookServices = Cat.M.Book.Services.AllServices;

namespace Cat.Web.Areas.Book.Controllers
{
    public class WechatPayController : BaseController
    {
        /// <summary>
        /// 支付统一下单接口，进行预支付
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="title"></param>
        /// <param name="total_fee"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionRes PrePay(string appid, string title, int total_fee)
        {
            var data = AllBookServices.WechatPayService.Prepay(base.Openid, appid, title, total_fee);
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 支付结果通知
        /// </summary>
        /// <returns></returns>
        public ActionRes Notify()
        {
            var data = AllBookServices.WechatPayService.Notify();

            try
            {
                if (data.Contains("<return_code><![CDATA[SUCCESS]]></return_code>"))
                {

                }
            }
            catch(Exception ex)
            {
                Cat.M.Log.Services.AllServices.SysExceptionLogService.AddLog(M.Log.Services.Enum.ExceptionType.ServiceException, ex, "发送模板消息[支付结果通知]时异常");
            }

            return ActionRes.Success(data);
        }
    }
}