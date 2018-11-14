using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Book.Models.ModelBinder.QueryModels;
using Cat.M.Book.Services;
using Cat.M.Public.Models.ModelBinder.QueryModels;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Api.Controllers
{
    /// <summary>
    /// 小说章节阅读记录
    /// </summary>
    public class BookChapterReadRecordController : BaseController
    {
        /// <summary>
        /// 获取小说章节阅读记录
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public ActionRes GetListByPage(QueryPager_Book_Chapter_Read_Record requestParams, string Book_Name, string Author)
        {
            var pageResult = AllServices.BookChapterReadRecordService.GetByPage(requestParams, Book_Name, Author);

            return ActionRes.Success(pageResult);
        }
    }
}