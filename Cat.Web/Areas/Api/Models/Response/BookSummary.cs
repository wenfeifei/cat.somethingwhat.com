using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cat.Web.Areas.Api.Models.Response
{
    /// <summary>
    /// 用户的概括信息：阅读时长、阅读记录、收藏书本
    /// </summary>
    public class BookSummary
    {
        /// <summary>
        /// 阅读时长（秒）
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 阅读时长（描述信息）
        /// </summary>
        public string DurationExplain { get; set; }
        /// <summary>
        /// 阅读记录（小说数）
        /// </summary>
        public int BookRecordCount { get; set; }
        /// <summary>
        /// 阅读记录（章节数）
        /// </summary>
        public int ChapterRecordCount { get; set; }
        /// <summary>
        /// 收藏记录（小说数）
        /// </summary>
        public int CollectionCount { get; set; }
    }
}
