using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cat.M.Book.Models.Table
{
    /// <summary>
    /// 书本阅读记录表
    /// </summary>
    public partial class Book_Read_Record : BaseEntity
    {
        [NotMapped]
        public new string Sort_Num { get; set; }

        /// <summary>
        /// 搜索来源
        /// </summary>
        public int Rule { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 小说名称
        /// </summary>
        public string Book_Name { get; set; }
        /// <summary>
        /// 小说分类
        /// </summary>
        public string Book_Classify { get; set; }
        /// <summary>
        /// 小说介绍页链接地址
        /// </summary>
        public string Book_Link { get; set; }
        /// <summary>
        /// 小说封面图片
        /// </summary>
        public string Cover_Image { get; set; }
        /// <summary>
        /// 小说简介
        /// </summary>
        public string Book_Intro { get; set; }
        /// <summary>
        /// 创建者的Openid
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// 最近一次阅读的记录（阅读记录表Id）
        /// </summary>
        public string Last_Reading_Record_Id { get; set; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? IsHidden { get; set; }
        /// <summary>
        /// 是否已收藏
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? AlreadyCollected { get; set; }



        /// <summary>
        /// 最近一次阅读的章节名称
        /// </summary>
        [NotMapped]
        public string Last_Reading_ChapterName { get; set; }
        /// <summary>
        /// 最近一次阅读的章节页链接地址
        /// </summary>
        [NotMapped]
        public string Last_Reading_ChapterLink { get; set; }
    }
}
