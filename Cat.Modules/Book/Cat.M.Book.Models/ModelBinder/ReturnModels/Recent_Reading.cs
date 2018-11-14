using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Models.ModelBinder.ReturnModels
{
    /// <summary>
    /// 最近阅读
    /// </summary>
    public class Recent_Reading
    {
        /// <summary>
        /// Book_Info_Id
        /// </summary>
        public string Book_Info_Id { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 小说名称
        /// </summary>
        public string Book_Name { get; set; }
        /// <summary>
        /// 小说介绍页链接地址
        /// </summary>
        public string Book_Link { get; set; }
        /// <summary>
        /// 章节名称
        /// </summary>
        public string Chapter_Name { get; set; }
        /// <summary>
        /// 小说章节页链接地址
        /// </summary>
        public string Chapter_Link { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Create_Time { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? Update_Time { get; set; }
    }
}
