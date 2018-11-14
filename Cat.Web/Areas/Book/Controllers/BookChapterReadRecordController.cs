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
    /// 书本章节阅读记录 控制器
    /// </summary>
    public class BookChapterReadRecordController : BaseController
    {
        /// <summary>
        /// 更新阅读时长
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Duration"></param>
        public ActionRes UpdateDuration(string Id, int Duration)
        {
            try
            {
                AllServices.BookChapterReadRecordService.UpdateDuration(Id, Duration);
            }
            catch { }
            return ActionRes.Success();
        }

        /// <summary>
        /// 新增或更新章节阅读记录
        /// </summary>
        /// <param name="entity"></param>
        [HttpPost]
        public ActionRes AddOrUpdateReadingRecord(Book_Chapter_Read_Record entity)
        {
            entity.Openid = base.Openid;
            var res = AllServices.BookChapterReadRecordService.AddOrUpdate(entity);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 获取指定用户的总的阅读时长（秒）
        /// </summary>
        public ActionRes GetDuration()
        {
            var res = AllServices.BookChapterReadRecordService.GetDuration(base.Openid);

            return ActionRes.Success(0, string.Empty, res);
        }

    }
}