using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cat.M.Book.Models.Table
{
    /// <summary>
    /// 系统信息表
    /// </summary>
    public partial class Book_User_Message : BaseEntity
    {
        [NotMapped]
        public new string Sort_Num { get; set; }

        /// <summary>
        /// 不填写则默认发送给所有人，填写则指定openid发送
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
