using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Book.Models.ModelBinder.QueryModels;
using Cat.M.Book.Models.Table;
using Cat.M.Book.Services;
using Cat.M.Public.Models.ModelBinder.QueryModels;
using Cat.M.Public.Models.Table;
using Cat.Utility;
using Cat.Web.Support.Filter;
using Microsoft.AspNetCore.Mvc;

using AllPublicService = Cat.M.Public.Services.AllServices;

namespace Cat.Web.Areas.Api.Controllers
{
    /// <summary>
    /// book用户信息
    /// </summary>
    public class BookUserController : BaseController
    {
        /// <summary>
        /// 分页查询book用户数据
        /// </summary>
        /// <param name="queryBookUser"></param>
        /// <returns></returns>
        public ActionRes GetListByPage(QueryPager_Book_User queryBookUser)
        {
            //throw new Exception("测试异常提示");

            var pageResult = AllServices.BookUserService.GetByPage(queryBookUser);

            return ActionRes.Success(pageResult);
        }

        /// <summary>
        /// 查询book用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionRes GetSingleById(string openid)
        {
            if (string.IsNullOrEmpty(openid)) return ActionRes.Fail("[openid]不能为空");

            var res = AllServices.BookUserService.GetSingleByOpenId(openid);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 管理员赠送喵币
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Administrator)]
        public ActionRes AddCurrency(string openid, int currency)
        {
            if (string.IsNullOrEmpty(openid)) return ActionRes.Fail("[openid]不能为空");
            if (currency <= 0) return ActionRes.Fail("喵币应大于0");

            var user = AllServices.BookUserService.GetSingleByOpenId(openid);
            //新增充值记录
            AllServices.BookUserRechargeService.Add(user.Openid, (int)Cat.Enums.Book.RechargeType.管理员赠送, (int)currency, "管理员赠送喵币");
            //调整用户账户余额
            user.Currency = user.Currency + currency;
            AllServices.BookUserService.Update(user);

            return ActionRes.Success();
        }

        /// <summary>
        /// 获取用户的概括信息：阅读时长、阅读记录、收藏书本
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionRes GetBookSummary(string openid)
        {
            if (string.IsNullOrEmpty(openid)) return ActionRes.Fail("[openid]不能为空");

            ////用户信息
            //var user = AllServices.BookUserService.GetSingleById(id);

            //阅读时长
            var duration = AllServices.BookChapterReadRecordService.GetDuration(openid);
            //阅读记录
            var bookCount = AllServices.BookReadRecordService.GetCountByOpenid(openid);
            var chapterCount = AllServices.BookChapterReadRecordService.GetCountByOpenid(openid);
            //收藏书本
            var collectionCount = AllServices.BookReadRecordService.GetCollectionCount(openid);
            //
            var durationExplain = $"{duration / 60 / 60}小时{duration / 60 % 60}分";
            var instance = new Models.Response.BookSummary()
            {
                Duration = duration,
                DurationExplain = durationExplain,
                BookRecordCount = bookCount,
                ChapterRecordCount = chapterCount,
                CollectionCount = collectionCount
            };
            return ActionRes.Success(instance);
        }

        /// <summary>
        /// 获取用户订单记录
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public ActionRes GetWechatPayOrderByPage(QueryPager_Wechat_PayOrder requestParams)
        {
            var pageResult = AllPublicService.WechatPayOrderService.GetByPage(requestParams);

            //过滤敏感信息
            foreach (var item in pageResult.List as List<Wechat_PayOrder>)
            {
                item.MchId = "******";
                item.OutTradeNo = "******";
                item.RequestData = "";
                item.PrepayId = "******";
            }

            return ActionRes.Success(pageResult);
        }

    }
}