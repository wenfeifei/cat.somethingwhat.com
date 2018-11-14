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
    /// 用户阅读偏好
    /// </summary>
    public class BookUserPreferenceController : BaseController
    {
        /// <summary>
        /// 搜索用户偏好数据
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public ActionRes GetListByPage(QueryPager_Book_User_Preference requestParams)
        {
            var pageResult = AllServices.BookUserPreferenceService.GetByPage(requestParams);

            return ActionRes.Success(pageResult);
        }
    }
}