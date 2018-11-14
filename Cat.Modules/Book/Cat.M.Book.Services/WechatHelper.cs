using Cat.M.Book.Services.Models.Wechat.Template;
using Cat.Utility;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services
{
    public class WechatHelper
    {
        /// <summary>
        /// 发送模板消息：充值成功通知
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="prepay_id"></param>
        /// <param name="account"></param>
        /// <param name="rechargeType"></param>
        /// <param name="rechargeAmt"></param>
        /// <param name="rechargeScore"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public static string SendTemplateMessage01(string openid, string prepay_id, string account, string rechargeType, decimal rechargeAmt, int rechargeScore, string remark)
        {
            var accessToken = Cat.M.Public.Services.AllServices.MiniProgramService.GetAccessToken();

            var payload = new Template()
            {
                template_id = "XcadIKr3FXkeUtaZVysz-0kPD1BLPG5_4lyI9YdHc9Q",
                page = string.Empty,
                form_id = prepay_id,
                data = new
                {
                    //帐号
                    keyword1 = new { value = "" },
                    //充值类型
                    keyword2 = new { value = "" },
                    //充值金额
                    keyword3 = new { value = "" },
                    //充值积分
                    keyword4 = new { value = "" },
                    //说明
                    keyword5 = new { value = "注意，这里是模拟测试支付，并不会做扣款操作。" },
                },
                emphasis_keyword = "keyword4.DATA",
            };

            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token={0}", accessToken);
            var response = HttpHelper.Get(url);
            return response;
        }
    }
}
