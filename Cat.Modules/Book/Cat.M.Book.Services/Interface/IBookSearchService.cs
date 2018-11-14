using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services
{
    public interface IBookSearchService
    {
        /// <summary>
        /// 小说搜索（分页）
        /// </summary>
        /// <param name="q"></param>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        ActionRes Books(string q, int pn = 1, int ps = 10);
        /// <summary>
        /// 获取单个小说
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionRes Book(string id);
        /// <summary>
        /// 获取单个小说（不包含章节列表(只取前十章)）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionRes BookNoChapter(string id);
        /// <summary>
        /// 获取单个小说的章节列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionRes BookChapters(string id);
        /// <summary>
        /// 获取指定id的小说的内容（包含上一章、下一章）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionRes BookContent(string id);
    }
}
