using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Cat.Enums.Book;
using Cat.M.Book.Services;
using Cat.Utility;
using Cat.Web.Support.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Book.Controllers
{
    [AccessInterfaceRecord(RecordType = Enums.RecordType.wechat_old_mmbook)]
    public class TempController : BaseController
    {
        readonly bool CloseOldInterface = Cat.Foundation.ConfigManager.BookSettings.CloseOldInterface.ToBoolean(true);
        Microsoft.AspNetCore.Http.HttpContext httpContext = Cat.Foundation.CatContext.HttpContext;


        [Route("/book/GetBookList")]
        public ActionRes GetBookList(string q)
        {
            if (CloseOldInterface) return ActionRes.Fail("接口已被关闭，请自行搭建后台接口站点以便测试。如有疑问，可QQ找我：514158268");
            if (IsInOnlineEvn()) return ActionRes.Fail("接口不能直接用于线上环境，请自行部署。或QQ找我：514158268(有偿帮助)");

            var list = AllServices.BookSearchService.GetBookList(WebSiteSearch.笔趣阁.GetHashCode(), q, 1);
            var newList = new List<BookItem>();
            foreach (var item in list)
            {
                newList.Add(new BookItem()
                {
                    bookname = item.BookName,
                    author = item.Author,
                    coverimg = item.Coverimg,
                    booktype = item.BookType,
                    booklink = item.BookLink,
                    intro = item.Intro,
                    last_update_time = item.Last_Update_Time,
                    last_update_chapter = item.Last_Update_ChapterName,
                    chapterlink = item.Last_Update_ChapterLink
                });
            }
            return ActionRes.Success(new { list = newList, pn = 1, count = newList.Count });
        }

        [Route("/book/GetBookChapter")]
        public ActionRes GetBookChapter(string url)
        {
            if (CloseOldInterface) return ActionRes.Fail("接口已被关闭，请自行搭建后台接口站点以便测试。如有疑问，可QQ找我：514158268");
            if (IsInOnlineEvn()) return ActionRes.Fail("接口不能直接用于线上环境，请自行部署。或QQ找我：514158268(有偿帮助)");

            var res = AllServices.BookSearchService.GetBookChapter(WebSiteSearch.笔趣阁.GetHashCode(), url);
            var data = new BookChapter()
            {
                count = res.Chapterlist.Count,
                bookname = res.BookName,
                author = res.Author,
                status = res.Status,
                last_update_time = res.Last_Update_Time,
                last_update_chapter = res.Last_Update_ChapterName,
                last_update_chapterlink = res.Last_Update_ChapterLink,
                intro = res.Intro,
                list = res.Chapterlist.Select(s => new BookChapter.chapterlist() { chaptername = s.ChapterName, chapterlink = s.ChapterLink }).ToList()
            };
            return ActionRes.Success(data);
        }

        [Route("/book/GetBookContent")]
        public ActionRes GetBookContent(string url)
        {
            if (CloseOldInterface) return ActionRes.Fail("接口已被关闭，请自行搭建后台接口站点以便测试。如有疑问，可QQ找我：514158268");
            if (IsInOnlineEvn()) return ActionRes.Fail("接口不能直接用于线上环境，请自行部署。或QQ找我：514158268(有偿帮助)");

            var link = HttpUtility.UrlDecode(url);

            var data = AllServices.BookSearchService.GetBookContent(WebSiteSearch.笔趣阁.GetHashCode(), link);
            var res = new BookContent()
            {
                bookname = data.BookName,
                chaptername = data.ChapterName,
                chapterlink = data.ChapterLink,
                content = data.Content,
                nextlink = data.NextChapterLink,
                prevlink = data.PrevChapterLink
            };
            return ActionRes.Success(res);
        }


        private bool IsInOnlineEvn()
        {
            if (httpContext.Request.Headers["Referer"].ToStr().IndexOf("servicewechat.com") == -1)
                return false;
            if (httpContext.Request.Headers["Referer"].ToStr().IndexOf("devtools") > -1)
                return false;
            if (httpContext.Request.Headers["Referer"].ToStr().IndexOf("/0/page-frame.html") > -1)
                return false;
            return true;
        }


#pragma warning disable IDE1006 // 命名样式
        public class ActionRes
        {
            public int code { get; set; }
            public string msg { get; set; }
            public object data { get; set; }
            public string tips { get; set; }

            public ActionRes(int code = 1, string msg = "", object data = null)
            {
                this.code = code;
                this.msg = msg;
                this.data = data;
                this.tips = "这是临时用的接口，随时会被我关闭，所以。。。千万不要用来做线上用！！！";
            }
            public static ActionRes Success(object data)
            {
                return new ActionRes(1, "", data);
            }
            public static ActionRes Fail(string msg)
            {
                return new ActionRes(-1, msg, null);
            }
            public static ActionRes Result(int code = 1, string msg = "", object data = null)
            {
                return new ActionRes(code, msg, data);
            }
        }

        public class BookItem
        {
            public string bookname { get; set; }
            public string author { get; set; }
            public string coverimg { get; set; }
            public string booktype { get; set; }
            public string booklink { get; set; }
            public string intro { get; set; }
            public string last_update_time { get; set; }
            public string last_update_chapter { get; set; }
            public string chapterlink { get; set; }
        }

        public class BookChapter
        {
            public int count { get; set; }
            public string bookname { get; set; }
            public string author { get; set; }
            public string status { get; set; }
            public string last_update_time { get; set; }
            public string last_update_chapter { get; set; }
            public string last_update_chapterlink { get; set; }
            public string intro { get; set; }
            public List<chapterlist> list { get; set; }
            public class chapterlist
            {
                public string chaptername { get; set; }
                public string chapterlink { get; set; }
            }
        }

        public class BookContent
        {
            public string bookname { get; set; }
            public string chaptername { get; set; }
            public string chapterlink { get; set; }
            public string content { get; set; }
            public string nextlink { get; set; }
            public string prevlink { get; set; }
        }
#pragma warning restore IDE1006 // 命名样式

    }
}