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
    /// 系统信息 控制器
    /// </summary>
    public class BookUserMessageController : BaseController
    {
        /// <summary>
        /// 获取信息列表
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="chapterid"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public ActionRes GetMessages(int top = 20)
        {
            var data = AllServices.BookUserMessageService.GetListByOpenid(base.Openid, top);
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 获取信息列表（用户未阅读过的）
        /// </summary>
        /// <param name="chapterid"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public ActionRes GetListByNoRead(int top = 20)
        {
            var data = AllServices.BookUserMessageService.GetListByNoRead(base.Openid, top);
            return ActionRes.Success(data);
        }

        /// <summary>
        /// 新增用户阅读系统信息记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionRes AddReadRecord(string ids)
        {
            if (string.IsNullOrEmpty(ids)) throw new Exception("ids is null");

            var data = AllServices.BookUserMessageReadRecordService.Add(ids.Split(",").ToList(), base.Openid);
            return ActionRes.Success(data);
        }
    }
}