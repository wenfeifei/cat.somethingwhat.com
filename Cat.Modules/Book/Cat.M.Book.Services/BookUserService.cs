using Cat.M.Book.Models.ModelBinder.ActionModels;
using Cat.M.Book.Models.ModelBinder.QueryModels;
using Cat.M.Book.Models.Table;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cat.M.Book.Services
{
    /// <summary>
    /// book用户 服务类
    /// </summary>
    public class BookUserService : AppBaseService<Book_User>
    {
        private Object thisLock = new Object();

        public Book_User GetSingle(string openid)
        {
            return base.GetSingle(w => w.Openid == openid);
        }
        public Book_User GetSingleById(string id)
        {
            return base.GetSingle(w => w.Id == id);
        }
        public Book_User GetSingleByOpenId(string openid)
        {
            return base.GetSingle(w => w.Openid == openid);
        }

        public new int GetCount(Expression<Func<Book_User, bool>> exp = null)
        {
            if (exp == null) exp = w => true;
            return base.GetCount(exp);
        }
        public new List<Book_User> GetAll(Expression<Func<Book_User, bool>> expression = null)
        {
            return base.GetAll(expression);
        }

        #region 获取用户信息（不存在用户则自动创建）
        /// <summary>
        /// 获取用户信息（不存在用户则自动创建）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Book_User GetUserInfo(Add_Book_User_Input entity)
        {
            if (entity.Appid.IsNullOrEmpty()) { throw new Exception("Appid获取失败"); }
            if (entity.Openid.IsNullOrEmpty()) { throw new Exception("Openid获取失败"); }
            if (entity.Openid.Length < 20) { throw new Exception("Openid不合法"); }

            lock (thisLock)
            {
                //根据 Openid 查找，查到就返回
                var instance = base.GetSingle(w => w.Openid == entity.Openid);
                if (instance != null)
                {
                    //更新用户信息
                    instance.NickName = entity.NickName;
                    instance.AvatarUrl = entity.AvatarUrl;
                    instance.Gender = entity.Gender;
                    instance.Country = entity.Country;
                    instance.Province = entity.Province;
                    instance.City = entity.City;
                    instance.Language = entity.Language;
                    instance.Update_Time = DateTime.Now;

                    base.Update(instance);

                    return instance;
                }

                Book_User model = new Book_User();
                try
                {
                    //新增用户信息
                    //model.Id = StringHelper.GuidTo16String();
                    model.Create_Time = DateTime.Now;

                    model.Appid = entity.Appid;
                    model.User_Id = "mm" + (base.GetCount(w => true) + Cat.Foundation.ConfigManager.BookSettings.InitIdNum).ToString();
                    model.Openid = entity.Openid;
                    model.NickName = entity.NickName;
                    model.AvatarUrl = entity.AvatarUrl;
                    model.Gender = entity.Gender;
                    model.Country = entity.Country;
                    model.Province = entity.Province;
                    model.City = entity.City;
                    model.Language = entity.Language;
                    model.Currency = Cat.Foundation.ConfigManager.BookSettings.InitCurrency;
                    model.Read_Minute = 0;

                    base.Add(model);

                    //新增喵币记录
                    AllServices.BookUserRechargeService.Add(model.Openid, (int)Cat.Enums.Book.RechargeType.活动赠送, Cat.Foundation.ConfigManager.BookSettings.InitCurrency, "用户初次登录赠送喵币");
                }
                catch (Exception ex)
                {
                    //ServiceRepository.SysErrorLogRepository.Add(ErrorCategory.Server, "异常：Cat.Services.Repository.Book.BookUserRepository.BookUserRepository", entity.ToJson(), ex);
                    throw ex;
                }
                return model;
            }
        }
        #endregion

        #region 获取用户账户信息
        /// <summary>
        /// 获取用户账户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public Book.Models.ModelBinder.ReturnModels.Account GetUserAccount(string openid)
        {
            var accountModel = new Book.Models.ModelBinder.ReturnModels.Account();
            var instance = base.GetSingle(w => w.Openid == openid);
            if (instance == null) { throw new Exception("[openid:" + openid + "]没有找到用户"); }

            accountModel.MM_Currency_Current = instance.Currency;

            var list = AllServices.BookUserRechargeService.GetList(openid);
            accountModel.MM_Currency_Sum = list.Sum(s => s.Recharge_Currency);
            accountModel.MM_Currency_Buy = list.Where(w => w.Recharge_Type == (int)Cat.Enums.Book.RechargeType.微信支付充值).Sum(s => s.Recharge_Currency);
            accountModel.MM_Currency_Give = list.Where(w => w.Recharge_Type == (int)Cat.Enums.Book.RechargeType.活动赠送).Sum(s => s.Recharge_Currency);

            accountModel.MM_Currency_Used = accountModel.MM_Currency_Sum - accountModel.MM_Currency_Current;
            accountModel.MM_Currency_Ratio = Cat.Foundation.ConfigManager.BookSettings.Currency_Ratio;

            return accountModel;
        }
        #endregion

        public int Update(Book_User entity)
        {
            entity.Update_Time = entity.Update_Time ?? DateTime.Now;
            return base.Update(entity);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryPager_Book_User"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(QueryPager_Book_User queryPager_Book_User)
        {
            //查询表达式
            var exp = Cat.M.Book.Services.Helper.ExpressionHelper.GetExpressionByQueryPager<QueryPager_Book_User, Book_User>(queryPager_Book_User);
            //排序
            IList<OrderBy> listOrderBy = new List<OrderBy>();
            if (!string.IsNullOrEmpty(queryPager_Book_User.sorter))
                listOrderBy.Add(new OrderBy() { Order = queryPager_Book_User.order, Sort = queryPager_Book_User.sort });

            return base.GetByPage(queryPager_Book_User.pn, queryPager_Book_User.ps, exp, listOrderBy);
        }
    }
}
