using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services.Models.Response
{
    /// <summary>
    /// 小说搜索来源
    /// </summary>
    public class BookSearchSource
    {
        public TempModel Default { get; set; }
        public List<TempModel> List { get; set; }
        public class TempModel
        {
            public string Value { get; set; }
            public int Key { get; set; }
        }
    }
}
