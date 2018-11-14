using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services.Models.Response
{
    public class PageResult
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Pageindex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int Pagesize { get; set; }
        /// <summary>
        /// 数据量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int Pages { get; set; }
        /// <summary>
        /// 列表数据
        /// </summary>
        public object List { get; set; }
    }
}
