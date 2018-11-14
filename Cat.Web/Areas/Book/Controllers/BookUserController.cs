using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Book.Models.ModelBinder.ActionModels;
using Cat.M.Book.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Book.Controllers
{
    public class BookUserController : BaseController
    {
        /// <summary>
        /// 获取用户信息（不存在用户则自动创建）
        /// </summary>
        /// <returns></returns>
        public ActionRes GetUserInfo(Add_Book_User_Input entity)
        {
            return ActionRes.Success(AllServices.BookUserService.GetUserInfo(entity));
        }

        /// <summary>
        /// 获取用户账户信息
        /// </summary>
        /// <returns></returns>
        public ActionRes GetUserAccount()
        {
            return ActionRes.Success(AllServices.BookUserService.GetUserAccount(base.Openid));
        }
    }
}