using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Book.Models.ModelBinder.QueryModels;
using Cat.M.Book.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Api.Controllers
{
    /// <summary>
    /// 用户消费喵币记录
    /// </summary>
    public class BookUserConsumeController : BaseController
    {
        /// <summary>
        /// 获取用户消费喵币记录
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public ActionRes GetListByPage(QueryPager_Book_User_Consume requestParams)
        {
            var pageResult = AllServices.BookUserConsumeService.GetByPage(requestParams);

            return ActionRes.Success(pageResult);
        }
    }
}