using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ReturnModels.Wechat
{
    /// <summary>
    /// 前端发起微信支付时所需参数
    /// </summary>
    public class RequestPayment
    {
        public string AppId { get; set; }
        public string NonceStr { get; set; }
        public string Package { get; set; }
        public string SignType { get; set; }
        public string TimeStamp { get; set; }
        public string PaySign { get; set; }
    }
}
