using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cat.M.Public.Models.Table
{
    /// <summary>
    /// 小程序支付订单表
    /// </summary>
    public class Wechat_PayOrder : BaseEntity
    {
        [NotMapped]
        public string Sort_Num { get; set; }

        /// <summary>
        /// 来自哪个小程序
        /// </summary>
        public string FromKey { get; set; }
        /// <summary>
        /// 微信小程序的appid
        /// </summary>
        public string Appid { get; set; }
        /// <summary>
        /// Openid
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// MchId
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// Body
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// RequestData
        /// </summary>
        public string RequestData { get; set; }
        /// <summary>
        /// 标价金额，单位：分
        /// </summary>
        public int TotalFee { get; set; }
        /// <summary>
        /// 是否支付成功
        /// </summary>
        public bool IsPaySuccessed { get; set; }
        /// <summary>
        /// 支付结果
        /// </summary>
        public string PayResult { get; set; }
        /// <summary>
        /// 微信生成的预支付ID
        /// </summary>
        public string PrepayId { get; set; }
    }
}
