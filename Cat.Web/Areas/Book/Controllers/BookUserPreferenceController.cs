using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Book.Models.Table;
using Cat.M.Book.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Book.Controllers
{
    /// <summary>
    /// 用户偏好设置 控制器
    /// </summary>
    public class BookUserPreferenceController : BaseController
    {
        /// <summary>
        /// 新增或更新用户偏好
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public ActionRes AddOrUpdateUserPreference(Book_User_Preference model)
        {
            model.Openid = base.Openid;
            AllServices.BookUserPreferenceService.AddOrUpdateUserPreference(model);

            return ActionRes.Success();
        }

        /// <summary>
        /// 获取用户偏好
        /// </summary>
        public ActionRes GetUserPreference()
        {
            try
            {
                var instance = AllServices.BookUserPreferenceService.GetSingleByOpenid(base.Openid);

                return ActionRes.Success(instance);
            }
            catch (Exception ex)
            {
                return ActionRes.Success("获取偏好失败:" + ex.Message);
            }
        }

    }
}