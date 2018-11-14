using Cat.Enums.Book;
using Cat.M.Book.Services.Capture;
using Cat.M.Book.Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services
{
    /// <summary>
    /// 书本相关搜索 服务类
    /// </summary>
    public class BookSearchService
    {
        /// <summary>
        /// 获取所有搜索来源分类
        /// </summary>
        /// <returns></returns>
        public List<Models.Response.BookCategory> GetBookCategoryList()
        {
            var list = new List<Models.Response.BookCategory>()
                {
                    new Models.Response.BookCategory(){ Value = WebSiteSearch.笔趣阁.GetHashCode(),Key = WebSiteSearch.笔趣阁.ToString() },
                    new Models.Response.BookCategory(){ Value = WebSiteSearch.外国文学.GetHashCode(),Key = WebSiteSearch.外国文学.ToString() },
                    //new Models.Response.BookCategory(){ Value = WebSiteSearch.全本小说网.GetHashCode(),Key = WebSiteSearch.全本小说网.ToString() },
                    //new Models.Response.BookCategory(){ Value = WebSiteSearch.书旗网.GetHashCode(),Key = WebSiteSearch.书旗网.ToString() },
                    //new Models.Response.BookCategory(){ Value = WebSiteSearch.言情小说吧.GetHashCode(),Key = WebSiteSearch.言情小说吧.ToString() },
                    //new Models.Response.BookCategory(){ Value = WebSiteSearch.古典文学网.GetHashCode(),Key = WebSiteSearch.古典文学网.ToString() }
                };
            return list;
        }

        /// <summary>
        /// 获取书本抓取服务实例
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        private IBookCapture GetBookCaptureService(int rule)
        {
            WebSiteSearch webSiteSearchEnum = (WebSiteSearch)rule;
            IBookCapture bookCapture;
            switch (webSiteSearchEnum)
            {
                case WebSiteSearch.笔趣阁:
                    bookCapture = new BiqugeCapture();
                    break;
                case WebSiteSearch.外国文学:
                    bookCapture = new LiteratureForeignCapture();
                    break;





                case WebSiteSearch.占个坑:
                default:
                    bookCapture = new BiqugeCapture();
                    break;
            }
            return bookCapture;
        }

        /// <summary>
        /// 搜索书本
        /// </summary>
        /// <param name="q"></param>
        /// <param name="pn"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public List<BookInfo> GetBookList(int rule, string q, int pn)
        {
            var service = this.GetBookCaptureService(rule);
            var data = service.GetBookList(q, pn);
            return data;
        }

        /// <summary>
        /// 获取小说章节目录信息
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public BookChapter GetBookChapter(int rule, string link)
        {
            var service = this.GetBookCaptureService(rule);
            var data = service.GetBookChapter(link);
            return data;
        }

        /// <summary>
        /// 获取小说的内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public BookContent GetBookContent(int rule, string link)
        {
            var service = this.GetBookCaptureService(rule);
            var data = service.GetBookContent(link);
            return data;
        }

    }
}
