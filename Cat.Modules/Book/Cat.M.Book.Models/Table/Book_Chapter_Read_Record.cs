using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cat.M.Book.Models.Table
{
    public partial class Book_Chapter_Read_Record : BaseEntity
    {
        [NotMapped]
        public new string Sort_Num { get; set; }

        /// <summary>
        /// 书本阅读记录表Id
        /// </summary>
        public string Book_Read_Record_Id { get; set; }
        /// <summary>
        /// 创建者的Openid
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// 章节名称
        /// </summary>
        public string Chapter_Name { get; set; }
        /// <summary>
        /// 小说章节页链接地址
        /// </summary>
        public string Chapter_Link { get; set; }
        /// <summary>
        /// 当前章节的字数
        /// </summary>
        public int Number_Of_Words { get; set; }
        /// <summary>
        /// 阅读时长（秒）
        /// </summary>
        public int Duration { get; set; }


        [NotMapped]
        public string Flag { get; set; }
        [NotMapped]
        public string Author { get; set; }
        [NotMapped]
        public string Book_Name { get; set; }
        [NotMapped]
        public string Book_Link { get; set; }
    }
}
