using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services.Models.Response
{
    public class BookInfo
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Coverimg { get; set; }
        public string BookType { get; set; }
        public string BookLink { get; set; }
        public string Intro { get; set; }
        public string Last_Update_Time { get; set; }
        public string Last_Update_ChapterName { get; set; }
        public string Last_Update_ChapterLink { get; set; }
    }
}
