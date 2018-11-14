using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cat.M.Book.Models.Table
{
    /// <summary>
    /// 喵币充值记录表
    /// </summary>
    public partial class Book_User_Recharge : BaseEntity
    {
        [NotMapped]
        public new string Sort_Num { get; set; }
        [NotMapped]
        public new DateTime? Update_Time { get; set; }

        /// <summary>
        /// Openid
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// 充值类型，1：活动赠送；2：微信支付充值；3：积分兑换充值
        /// </summary>
        public int Recharge_Type { get; set; }
        /// <summary>
        /// 充值喵币数
        /// </summary>
        public int Recharge_Currency { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
