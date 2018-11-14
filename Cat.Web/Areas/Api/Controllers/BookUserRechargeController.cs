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
    /// 用户充值/赠送记录
    /// </summary>
    public class BookUserRechargeController : BaseController
    {
        /// <summary>
        /// 获取用户充值/赠送记录
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public ActionRes GetListByPage(QueryPager_Book_User_Recharge requestParams)
        {
            var pageResult = AllServices.BookUserRechargeService.GetByPage(requestParams);

            return ActionRes.Success(pageResult);
        }
    }
}