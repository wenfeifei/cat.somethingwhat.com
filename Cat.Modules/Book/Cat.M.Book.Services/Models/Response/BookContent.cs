using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services.Models.Response
{
    public class BookContent
    {
        public string BookName { get; set; }
        //public string BookLink { get; set; }
        public string ChapterName { get; set; }
        public string ChapterLink { get; set; }
        public string Content { get; set; }
        public string NextChapterLink { get; set; }
        public string PrevChapterLink { get; set; }
        public int Number_Of_Words { get; set; }
        public bool AlreadyCollected { get; set; }
        //public string Book_Chapter_Read_Record_Id { get; set; }
        /// <summary>
        /// 用户剩余喵币
        /// </summary>
        public int Currency { get; set; }
    }
}
