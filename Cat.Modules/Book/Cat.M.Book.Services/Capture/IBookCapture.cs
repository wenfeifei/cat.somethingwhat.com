using Cat.M.Book.Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services.Capture
{
    public interface IBookCapture
    {
        /// <summary>
        /// 搜索书本
        /// </summary>
        /// <param name="q"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        List<BookInfo> GetBookList(string q, int pn);
        /// <summary>
        /// 获取小说章节目录信息
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        BookChapter GetBookChapter(string link);
        /// <summary>
        /// 获取小说的内容
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        BookContent GetBookContent(string link);
    }
}
