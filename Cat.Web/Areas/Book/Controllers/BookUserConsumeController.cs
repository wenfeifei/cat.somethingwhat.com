using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Book.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Book.Controllers
{
    /// <summary>
    /// 用户消费记录 控制器
    /// </summary>
    public class BookUserConsumeController : BaseController
    {
        /// <summary>
        /// 阅读章节的扣费操作
        /// </summary>
        /// <param name="chapterid"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionRes AddByReading(string chapterid, string remark)
        {
            if (string.IsNullOrEmpty(chapterid)) throw new Exception("chapterid is null");

            var user = AllServices.BookUserService.GetSingle(base.Openid);
            if (user == null) throw new Exception("找不到用户");

            //新增扣费记录
            AllServices.BookUserConsumeService.Add(new M.Book.Models.Table.Book_User_Consume()
            {
                Openid = base.Openid,
                Book_Chapter_Read_Record_Id = chapterid,
                Amount = 1,
                Remark = remark
            });
            //调整用户账户余额
            user.Currency = user.Currency - 1;
            AllServices.BookUserService.Update(user);

            return ActionRes.Success();
        }
    }
}