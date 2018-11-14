using Cat.M.Public.Models.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Services
{
    /// <summary>
    /// 微信小程序配置 服务类
    /// </summary>
    public class WechatAppConfigService : AppBaseService<Wechat_App_Config>
    {
        public Wechat_App_Config GetSingle(Cat.Enums.Wechat.AppKey appKey)
        {
            string mark_Key = appKey.GetHashCode().ToString();
            return base.GetSingle(w => w.Mark_Key == mark_Key);
        }

    }
}
