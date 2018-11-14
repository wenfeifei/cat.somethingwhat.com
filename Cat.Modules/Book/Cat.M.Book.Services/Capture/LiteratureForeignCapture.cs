using System;
using System.Collections.Generic;
using System.Text;
using Cat.M.Book.Services.Models.Response;
using Cat.Utility;
using System.Linq;
using Cat.Foundation;

namespace Cat.M.Book.Services.Capture
{
    /// <summary>
    /// 抓取来源：http://spider.somethingwhat.com/
    /// </summary>
    public class LiteratureForeignCapture : IBookCapture
    {
        /// <summary>
        /// 小说来源的数据请求地址
        /// </summary>
        private readonly string SpiderRemoteUrl = Cat.Foundation.ConfigManager.BookSettings.SpiderRemoteUrl;

        public List<BookInfo> GetBookList(string q, int pn)
        {
            string url = $"{SpiderRemoteUrl}/Book/LiteratureForeign/Search?q={q}&pn={pn}";
            string jsonRes = HttpHelper.Get(url);
            var jsonObj = Serializer.JsonDeserialize<BookListResModel>(jsonRes);

            if (jsonObj.Code < 0) throw new Exception(jsonObj.Msg);
            if (jsonObj.Data == null) return null;

            return jsonObj.Data.Select(s => new BookInfo()
            {
                BookName = s.Book_Name,
                Author = s.Author,
                Coverimg = null,
                BookType = s.Book_Classify,
                BookLink = $"{SpiderRemoteUrl}/Book/LiteratureForeign/GetChapters?id={s.Id}",
                Intro = s.Book_Intro,
                Last_Update_Time = s.Update_Time.ToString("yyyy-MM-dd"),
                Last_Update_ChapterName = s.Last_Update_ChapterName,
                Last_Update_ChapterLink = s.Last_Update_ChapterLink
            }).ToList();
        }

        public BookChapter GetBookChapter(string link)
        {
            string url = link;
            string jsonRes = HttpHelper.Get(url);
            var jsonObj = Serializer.JsonDeserialize<BookChapterResModel>(jsonRes);

            if (jsonObj.Code < 0) throw new Exception(jsonObj.Msg);
            if (jsonObj.Data == null) return null;

            var responseModel = new BookChapter()
            {
                BookName = jsonObj.Data.Book_Name,
                BookLink = link,
                Author = jsonObj.Data.Author,
                Status = "--",
                Last_Update_Time = jsonObj.Data.Update_Time.ToString("yyyy-MM-dd HH:mm:ss"),
                Last_Update_ChapterName = jsonObj.Data.Last_Update_ChapterName,
                Last_Update_ChapterLink = jsonObj.Data.Last_Update_ChapterLink,
                Intro = jsonObj.Data.Book_Intro
            };

            if (jsonObj.Data.Chapters != null && jsonObj.Data.Chapters.Any())
            {
                responseModel.Chapterlist = jsonObj.Data.Chapters.Select(s => new BookChapter.ChapterlistModel()
                {
                    ChapterName = s.Name,
                    ChapterLink = $"{SpiderRemoteUrl}/Book/LiteratureForeign/GetContent?id={s.Id}"
                }).ToList();
            }

            return responseModel;
        }

        public BookContent GetBookContent(string link)
        {
            string url = link;
            string jsonRes = HttpHelper.Get(url);
            var jsonObj = Serializer.JsonDeserialize<BookContentResModel>(jsonRes);

            if (jsonObj.Code < 0) throw new Exception(jsonObj.Msg);
            if (jsonObj.Data == null) return null;

            var responseModel = new BookContent()
            {
                BookName = jsonObj.Data.Book_Name,
                ChapterName = jsonObj.Data.Chapter_Name,
                ChapterLink = link,
                Content = jsonObj.Data.Content,
                NextChapterLink = $"{SpiderRemoteUrl}/Book/LiteratureForeign/GetContent?id={jsonObj.Data.Next_Chapter_Id}",
                PrevChapterLink = $"{SpiderRemoteUrl}/Book/LiteratureForeign/GetContent?id={jsonObj.Data.Prev_Chapter_Id}",
                Number_Of_Words = jsonObj.Data.Words
            };

            return responseModel;
        }


        #region 响应的实体
        public class Result_Literature_Foreign
        {
            public string Id { get; set; }
            /// <summary>
            /// 作者
            /// </summary>
            public string Author { get; set; }
            /// <summary>
            /// 小说名称
            /// </summary>
            public string Book_Name { get; set; }
            /// <summary>
            /// 小说分类
            /// </summary>
            public string Book_Classify { get; set; }
            /// <summary>
            /// 小说简介
            /// </summary>
            public string Book_Intro { get; set; }
            /// <summary>
            /// 章节数
            /// </summary>
            public int ChapterCount { get; set; }
            /// <summary>
            /// 更新时间
            /// </summary>
            public DateTime Update_Time { get; set; }

            public string Last_Update_ChapterName { get; set; }
            public string Last_Update_ChapterLink { get; set; }

        }
        /// <summary>
        /// 小说列表信息
        /// </summary>
        private class BookListResModel
        {
            public int Code { get; set; }
            public string Msg { get; set; }
            public List<Result_Literature_Foreign> Data { get; set; }
        }
        /// <summary>
        /// 小说章节信息
        /// </summary>
        private class BookChapterResModel
        {
            public int Code { get; set; }
            public string Msg { get; set; }
            public Result_Literature_Foreign_Chapter Data { get; set; }
            public class Result_Literature_Foreign_Chapter : Result_Literature_Foreign
            {
                /// <summary>
                /// 章节目录（必须正序保存）
                /// </summary>
                public List<ChaptersModel> Chapters { get; set; }
                public class ChaptersModel
                {
                    /// <summary>
                    /// 章节id
                    /// </summary>
                    public string Id { get; set; }
                    /// <summary>
                    /// 章节名称
                    /// </summary>
                    public string Name { get; set; }
                    /// <summary>
                    /// 字数
                    /// </summary>
                    public int Words { get; set; }
                    /// <summary>
                    /// 章节属于哪部分的，如：第一部、第二部、第三部......
                    /// </summary>
                    public string Part { get; set; }
                }
            }
        }
        /// <summary>
        /// 小说内容信息
        /// </summary>
        private class BookContentResModel
        {
            public int Code { get; set; }
            public string Msg { get; set; }
            public Result_Chapter_Content Data { get; set; }
            public class Result_Chapter_Content
            {
                public string BookId { get; set; }
                /// <summary>
                /// 作者
                /// </summary>
                public string Author { get; set; }
                /// <summary>
                /// 小说名称
                /// </summary>
                public string Book_Name { get; set; }
                /// <summary>
                /// 章节属于哪部分的，如：第一部、第二部、第三部......
                /// </summary>
                public string Part { get; set; }
                /// <summary>
                /// 章节id
                /// </summary>
                public string Chapter_Id { get; set; }
                /// <summary>
                /// 章节名称
                /// </summary>
                public string Chapter_Name { get; set; }
                /// <summary>
                /// 章节内容
                /// </summary>
                public string Content { get; set; }
                /// <summary>
                /// 字数
                /// </summary>
                public int Words { get; set; }

                /// <summary>
                /// 上一章的章节内容id
                /// </summary>
                public string Prev_Chapter_Id { get; set; }
                /// <summary>
                /// 上一章的章节名称
                /// </summary>
                public string Prev_Chapter_Name { get; set; }
                /// <summary>
                /// 下一章的章节内容id
                /// </summary>
                public string Next_Chapter_Id { get; set; }
                /// <summary>
                /// 下一章的章节名称
                /// </summary>
                public string Next_Chapter_Name { get; set; }
            }
        }


        #endregion
    }
}
