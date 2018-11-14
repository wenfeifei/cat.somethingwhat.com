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
    /// 书本阅读记录 控制器
    /// </summary>
    public class BookReadRecordController : BaseController
    {
        /// <summary>
        /// 新增书本阅读记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionRes Add(Book_Read_Record entity)
        {
            entity.Openid = base.Openid;
            var instance = AllServices.BookReadRecordService.GetSingle(entity.Openid, entity.Book_Name, entity.Author, entity.Rule);
            if (instance != null)
            {
                return ActionRes.Success(instance);
            }
            else
            {
                AllServices.BookReadRecordService.Add(entity);
                return ActionRes.Success(entity);
            }
        }

        /// <summary>
        /// 获取书本阅读记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionRes GetSingle(string id)
        {
            AllServices.BookReadRecordService.GetSingle(id);
            return ActionRes.Success();
        }

        /// <summary>
        /// 更新收藏状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="alreadyCollected">是否收藏</param>
        /// <returns></returns>
        public ActionRes UpdateCollectedState(string id, bool alreadyCollected)
        {
            AllServices.BookReadRecordService.UpdateCollectedState(id, alreadyCollected);
            return ActionRes.Success();
        }

        /// <summary>
        /// 获取最近的阅读
        /// </summary>
        /// <returns></returns>
        public ActionRes GetRecentReading(int top = 10)
        {
            int count;
            var list = AllServices.BookReadRecordService.GetRecentReading(base.Openid, 1, top, out count);

            return ActionRes.Success(list);
        }

        /// <summary>
        /// 获取最近的阅读
        /// </summary>
        /// <returns></returns>
        public ActionRes GetRecentReadingByPage(int pn = 1, int ps = 10)
        {
            var list = AllServices.BookReadRecordService.GetRecentReadingByPage(base.Openid, pn, ps);

            return ActionRes.Success(list);
        }

        /// <summary>
        /// 获取用户阅读记录概要信息
        /// </summary>
        /// <returns></returns>
        public ActionRes GetBookReadingRecordSummary()
        {
            var bookCount = AllServices.BookReadRecordService.GetCountByOpenid(base.Openid);
            var chapterCount = AllServices.BookChapterReadRecordService.GetCountByOpenid(base.Openid);

            return ActionRes.Success(new { BookCount = bookCount, ChapterCount = chapterCount });
        }

        /// <summary>
        /// 获取用户收藏的小说数
        /// </summary>
        /// <returns></returns>
        public ActionRes GetBookUserCollectionSummary()
        {
            var count = AllServices.BookReadRecordService.GetCollectionCount(base.Openid);

            return ActionRes.Success(new { Count = count });
        }

        /// <summary>
        /// 获取用户收藏的小说
        /// </summary>
        /// <returns></returns>
        public ActionRes GetCollectedList()
        {
            var list = AllServices.BookReadRecordService.GetCollectedList(base.Openid);

            return ActionRes.Success(list);
        }

    }
}