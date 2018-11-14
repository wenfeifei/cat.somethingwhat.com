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
    /// 用户阅读记录
    /// </summary>
    public class BookReadRecordController : BaseController
    {
        /// <summary>
        /// 获取最近的阅读
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public ActionRes GetRecentReadingByPage(string openid, int pn = 1, int ps = 10)
        {
            if (string.IsNullOrEmpty(openid)) return ActionRes.Fail("[openid]不能为空");

            ////用户信息
            //var user = AllServices.BookUserService.GetSingleByOpenId(openid);

            var list = AllServices.BookReadRecordService.GetRecentReadingByPage(openid, pn, ps);

            return ActionRes.Success(list);
        }

        /// <summary>
        /// 获取用户阅读记录（小说基本信息）
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public ActionRes GetListByPage(QueryPager_Book_Read_Record requestParams)
        {
            var pageResult = AllServices.BookReadRecordService.GetByPage(requestParams);

            return ActionRes.Success(pageResult);
        }
    }
}