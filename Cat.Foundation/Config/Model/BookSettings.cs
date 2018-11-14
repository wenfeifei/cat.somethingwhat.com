using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Foundation.Config.Model
{
    /// <summary>
    /// BookSettings，喵喵看书配置相关项
    /// </summary>
    public class BookSettings
    {
        /// <summary>
        /// 创建用户时用户id的初始值
        /// </summary>
        public int InitIdNum { get; set; }
        /// <summary>
        /// 用户首次登录喵喵看书时送多少喵币
        /// </summary>
        public int InitCurrency { get; set; }
        /// <summary>
        /// 1元可以兑换多少喵币
        /// </summary>
        public int Currency_Ratio { get; set; }
        /// <summary>
        /// 是否关闭旧的接口
        /// </summary>
        public bool CloseOldInterface { get; set; }
        /// <summary>
        /// 是否开启微信支付
        /// </summary>
        public bool OpenWxPay { get; set; }
        /// <summary>
        /// “外国文学”等小说来源的数据请求地址
        /// </summary>
        public string SpiderRemoteUrl { get; set; }
    }
}