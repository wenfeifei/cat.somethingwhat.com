using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services.Models.Response.Statistics
{
    public class BookUserReadStatistics : DateStatistics
    {
        /// <summary>
        /// 用户数
        /// </summary>
        public int users { get; set; }
        /// <summary>
        /// 章节数
        /// </summary>
        public int chapters { get; set; }
    }
}
