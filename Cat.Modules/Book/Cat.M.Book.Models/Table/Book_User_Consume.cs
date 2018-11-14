using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cat.M.Book.Models.Table
{
    /// <summary>
    /// 用户扣费记录表
    /// </summary>
    public partial class Book_User_Consume : BaseEntity
    {
        [NotMapped]
        public new string Sort_Num { get; set; }
        [NotMapped]
        public new DateTime? Update_Time { get; set; }

        /// <summary>
        /// 书本章节阅读记录表Id
        /// </summary>
        public string Book_Chapter_Read_Record_Id { get; set; }
        /// <summary>
        /// 消费喵币数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建者的Openid
        /// </summary>
        public string Openid { get; set; }
    }
}
