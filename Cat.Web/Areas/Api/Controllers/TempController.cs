using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Book.Services;
using Cat.M.Public.Models.Table;
using Cat.M.Public.Services.Constants;
using Cat.Utility;
using Cat.Web.Support.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using AllCacheServices = Cat.M.Cache.Services.AllServices;

namespace Cat.Web.Areas.Api.Controllers
{
    public class TempController : BaseController
    {
        //public class TempModel
        //{
        //    public string userName { get; set; }
        //    public string password { get; set; }
        //    public string type { get; set; }
        //}

        //[HttpPost]
        //[Route("/api/account/login")]
        //public IActionResult Index([FromBody]TempModel tempModel)
        //{
        //    JsonResult jsonResult = new JsonResult(new
        //    {
        //        status = "ok",
        //        type = "account",
        //        currentAuthority = "admin",
        //        token = "30624700306247705342089345682451"
        //    });

        //    return jsonResult;
        //}


        [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Administrator)]
        public string A()
        {
            return "管理员登录";
        }

        [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Guest)]
        public string B()
        {
            return "访客登录";
        }

        [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Anonymous)]
        public string C()
        {
            return "匿名登录";
        }

        public string D()
        {

            //var a = CheckAuthority("Temp", "A");
            //var b = CheckAuthority("Temp", "B");
            //var c = CheckAuthority("Temp", "C");
            //var d = CheckAuthority("Temp", "D");

            return "who?";
        }



        /// <summary>
        /// 管理员赠送、扣除喵币
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Administrator)]
        public ActionRes UpdateCurrency(string openid, int currency)
        {
            if (!base.IsAdministrator) return ActionRes.Fail("当前登录用户没有权限进行操作");

            if (string.IsNullOrEmpty(openid)) return ActionRes.Fail("[openid]不能为空");
            if (currency == 0) return ActionRes.Fail("喵币不能等于0");
            string tip = currency >= 0 ? "赠送" : "偷走";

            var user = AllServices.BookUserService.GetSingleByOpenId(openid);
            //新增充值记录
            AllServices.BookUserRechargeService.Add(user.Openid, (int)Cat.Enums.Book.RechargeType.管理员赠送, (int)currency, $"管理员{tip}喵币");
            //调整用户账户余额
            user.Currency = user.Currency + currency;
            AllServices.BookUserService.Update(user);

            return ActionRes.Success();
        }

        ///// <summary>
        ///// 临时方法：低于5000喵币的用户，赠送其9000喵币
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public ActionRes UpdateCurrencyAll_01(string key)
        //{
        //    int currency = 9000;

        //    if (!base.IsAdministrator) return ActionRes.Fail("当前登录用户没有权限进行操作");
        //    if (currency == 0) return ActionRes.Fail("喵币不能等于0");
        //    string tip = currency >= 0 ? "赠送" : "偷走";

        //    var users = AllServices.BookUserService.GetAll(w => w.Currency < 5000);

        //    foreach(var user in users)
        //    {
        //        //新增充值记录
        //        AllServices.BookUserRechargeService.Add(user.Openid, (int)Cat.Enums.Book.RechargeType.管理员赠送, (int)currency, $"管理员{tip}喵币");
        //        //调整用户账户余额
        //        user.Currency = user.Currency + currency;
        //        AllServices.BookUserService.Update(user);
        //    }

        //    return ActionRes.Success();
        //}

    }
}