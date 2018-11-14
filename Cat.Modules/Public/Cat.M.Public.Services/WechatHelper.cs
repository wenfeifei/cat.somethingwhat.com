using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Services
{
    public class WechatHelper
    {
        /// <summary>
        /// 登录凭证校验
        /// </summary>
        /// <param name="js_code"></param>
        /// <returns></returns>
        public string Jscode2session(Enums.Wechat.AppKey appKey, string js_code)
        {
            var wechatAppConfig = AllServices.WechatAppConfigService.GetSingle(appKey);

            if (wechatAppConfig == null) throw new Exception("数据库中没有找到配置");

            var url = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", wechatAppConfig.Appid, wechatAppConfig.Secret, js_code);
            var response = HttpHelper.Get(url);
            return response;
        }
    }
}
