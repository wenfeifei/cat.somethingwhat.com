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
    public class BookUserRechargeController : BaseController
    {
        /// <summary>
        /// 获取购买/赠送记录
        /// </summary>
        /// <returns></returns>
        public ActionRes GetRecoreds(int rechargeType, int top = 10)
        {
            List<Book_User_Recharge> list = new List<Book_User_Recharge>();
            if (rechargeType == 1)
            {
                list.AddRange(AllServices.BookUserRechargeService.GetRecoreds(Enums.Book.RechargeType.活动赠送, base.Openid, top));
                list.AddRange(AllServices.BookUserRechargeService.GetRecoreds(Enums.Book.RechargeType.管理员赠送, base.Openid, top));
            }
            else if (rechargeType == 2)
            {
                list.AddRange(AllServices.BookUserRechargeService.GetRecoreds(Enums.Book.RechargeType.微信支付充值, base.Openid, top));
                list.AddRange(AllServices.BookUserRechargeService.GetRecoreds(Enums.Book.RechargeType.积分兑换充值, base.Openid, top));
            }

            return ActionRes.Success(list.OrderByDescending(o => o.Create_Time).ToList());
        }
    }
}