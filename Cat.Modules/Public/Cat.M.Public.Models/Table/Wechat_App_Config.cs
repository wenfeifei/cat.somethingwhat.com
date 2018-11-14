using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.Table
{
    /// <summary>
    /// 微信小程序配置
    /// </summary>
    public partial class Wechat_App_Config : BaseEntity
    {
        /// <summary>
        /// 用作标记的键（唯一）
        /// </summary>
        public string Mark_Key { get; set; }
        /// <summary>
        /// 小程序appid
        /// </summary>
        public string Appid { get; set; }
        /// <summary>
        /// 小程序secret
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string Mch_id { get; set; }
        /// <summary>
        /// 商户号密钥
        /// </summary>
        public string Mch_id_secret { get; set; }
    }
}
