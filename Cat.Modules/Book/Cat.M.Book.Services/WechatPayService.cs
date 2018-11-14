using Cat.Foundation;
using Cat.M.Public.Models.ModelBinder.ReturnModels.Wechat;
using Cat.M.Public.Models.Table;
using Cat.Utility;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Text;

using AllPublicService = Cat.M.Public.Services.AllServices;
using AllLogService = Cat.M.Log.Services.AllServices;

namespace Cat.M.Book.Services
{
    /// <summary>
    /// 微信支付相关 服务类
    /// </summary>
    public class WechatPayService
    {
        private Wechat_App_Config WechatAppConfig => AllPublicService.WechatAppConfigService.GetSingle(Enums.Wechat.AppKey.喵喵看书);

        /// <summary>
        /// 调用商户服务器支付统一下单接口，进行预支付
        /// </summary>
        public RequestPayment Prepay(string openid, string appid, string title, int total_fee)
        {
            bool isMock = !Cat.Foundation.ConfigManager.BookSettings.OpenWxPay.ToBoolean(true);

            //注释下面这个判断，就是一个正常的微信支付流程了。 或配置OpenWxPay的值为false
            if (isMock)
            {
                var user = AllServices.BookUserService.GetSingle(openid);
                if (user == null) throw new Exception("找不到用户");
                var MM_Currency_Ratio = Foundation.ConfigManager.BookSettings.Currency_Ratio;
                var MM_Currency = total_fee * 0.01 * MM_Currency_Ratio;
                //新增充值记录
                AllServices.BookUserRechargeService.Add(user.Openid, (int)Cat.Enums.Book.RechargeType.微信支付充值, (int)MM_Currency, "模拟充值");
                //调整用户账户余额
                user.Currency = user.Currency + (int)MM_Currency;
                AllServices.BookUserService.Update(user);
                // throw new Exception("充值成功！惊不惊喜？意不意外？");
            }
            else
            {
                //string openid = "o0LDq4njwCCuQuj3FOIZeLFDrc9o";
                string key = WechatAppConfig.Mch_id_secret; //商户平台设置的密钥key

                //int price = 120;
                //int total_fee = price * 100;

                string outTradeNo = StringHelper.GetUUID().ToString(); //商户订单号

                TenPayV3UnifiedorderRequestData requestData = new TenPayV3UnifiedorderRequestData(
                    appid,
                    WechatAppConfig.Mch_id,
                    title,
                    outTradeNo,
                    total_fee,
                    IPHelper.GetClientIP(),
                    CatContext.HttpContext.Request.Scheme + "://" + CatContext.HttpContext.Request.Host + "/Book/WechatPay/Notify",
                    TenPayV3Type.JSAPI,
                    openid,
                    key,
                    TenPayV3Util.GetNoncestr()
                    );
                var result = TenPayV3.Unifiedorder(requestData);

                AllLogService.SysActionLogService.AddLog(Log.Services.Enum.LogLevel.INFO, requestData.ToJson(), "微信支付统一下单");

                //接口请求失败
                if (result.return_code != "SUCCESS")
                    throw new Exception(result.return_msg);
                //业务失败
                else if (result.result_code != "SUCCESS")
                    throw new Exception(string.Format("[{0}]{1}", result.err_code, result.err_code_des));
                //成功
                else if (result.IsResultCodeSuccess())
                {
                    //再次签名接口，返回支付数据
                    string timeStamp = TenPayV3Util.GetTimestamp();
                    string nonceStr = TenPayV3Util.GetNoncestr();
                    string package = "prepay_id=" + result.prepay_id;
                    string paySign = TenPayV3.GetJsPaySign(result.appid, timeStamp, nonceStr, package, key);
                    var data = new RequestPayment()
                    {
                        AppId = result.appid,
                        NonceStr = nonceStr,
                        Package = package,
                        SignType = "MD5",
                        TimeStamp = timeStamp,
                        PaySign = paySign
                    };

                    //数据库记录订单信息
                    AllPublicService.WechatPayOrderService.Add(Enums.Wechat.AppKey.喵喵看书, appid, openid, WechatAppConfig.Mch_id, outTradeNo, title, requestData.ToJson(), total_fee, false, "预支付", result.prepay_id);

                    return data;
                }
            }

            if (isMock)
            {
                throw new Exception("充值成功！惊不惊喜？意不意外？");
            }

            throw new Exception("微信预支付失败");
        }

        /// <summary>
        /// 支付结果通知
        /// </summary>
        /// <returns></returns>
        public string Notify()
        {
            ResponseHandler responseHandler = new ResponseHandler(Cat.Foundation.CatContext.HttpContext);
            string return_code = responseHandler.GetParameter("return_code");
            string return_msg = responseHandler.GetParameter("return_msg");

            AllLogService.SysActionLogService.AddLog(Log.Services.Enum.LogLevel.INFO, string.Format("return_msg={0},return_msg={1}", return_code, return_msg), "支付结果通知");

            try
            {
                responseHandler.SetKey(WechatAppConfig.Mch_id_secret);
                if (responseHandler.IsTenpaySign())
                {
                    if (return_code.ToUpper() == "SUCCESS")
                    {
                        string out_trade_no = responseHandler.GetParameter("out_trade_no");
                        //检查数据库中订单状态
                        var instance = AllPublicService.WechatPayOrderService.GetByOutTradeNo(out_trade_no);
                        if (instance != null && instance.IsPaySuccessed != true)
                        {
                            instance.IsPaySuccessed = true;
                            instance.PayResult = "requestPayment:ok";
                            instance.Update_Time = DateTime.Now;
                            AllPublicService.WechatPayOrderService.Update(instance);
                            //ServiceRepository.SysActionLogRepository.AddLog("支付结果通知", "SUCCESS", string.Empty);
                            var user = AllServices.BookUserService.GetSingle(instance.Openid);
                            var MM_Currency_Ratio = Foundation.ConfigManager.BookSettings.Currency_Ratio;
                            var MM_Currency = instance.TotalFee * 0.01 * MM_Currency_Ratio;
                            //新增充值记录
                            AllServices.BookUserRechargeService.Add(user.Openid, (int)Cat.Enums.Book.RechargeType.微信支付充值, (int)MM_Currency, "微信支付充值");
                            //调整用户账户余额
                            user.Currency = user.Currency + (int)MM_Currency;
                            AllServices.BookUserService.Update(user);
                        }
                        else
                        {
                            AllLogService.SysActionLogService.AddLog(Log.Services.Enum.LogLevel.ERROR, "找不到订单out_trade_no:" + out_trade_no, "支付结果通知-失败");
                        }
                    }
                    else
                    {
                        AllLogService.SysActionLogService.AddLog(Log.Services.Enum.LogLevel.ERROR, string.Format("return_msg={0},return_msg={1}", return_code, return_msg), "支付结果通知-失败");
                    }
                }
                else
                {
                    AllLogService.SysActionLogService.AddLog(Log.Services.Enum.LogLevel.ERROR, "签名不对", "支付结果通知-失败");
                }
            }
            catch (Exception ex)
            {
                AllLogService.SysExceptionLogService.AddLog(Log.Services.Enum.ExceptionType.BussinessException, ex, "支付结果通知-异常");
            }

            var res = @"<xml>
                          <return_code><![CDATA[SUCCESS]]></return_code>
                          <return_msg><![CDATA[OK]]></return_msg>
                        </xml>";
            return res;
        }
    }
}
