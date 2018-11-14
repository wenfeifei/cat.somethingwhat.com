using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cat.M.Book.Models.Table
{
    /// <summary>
    /// 用户阅读系统信息记录表
    /// </summary>
    public partial class Book_User_Message_ReadRecord : BaseEntity
    {
        [NotMapped]
        public new string Sort_Num { get; set; }
        [NotMapped]
        public new DateTime? Update_Time { get; set; }

        /// <summary>
        /// 系统信息表id
        /// </summary>
        public string Book_User_Message_Id { get; set; }
        /// <summary>
        /// Openid
        /// </summary>
        public string Openid { get; set; }
    }
}
