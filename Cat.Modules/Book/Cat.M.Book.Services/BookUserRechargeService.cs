using Cat.M.Book.Models.ModelBinder.QueryModels;
using Cat.M.Book.Models.Table;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cat.M.Book.Services
{
    /// <summary>
    /// 喵币充值记录 服务类
    /// </summary>
    public class BookUserRechargeService : AppBaseService<Book_User_Recharge>
    {
        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Book_User_Recharge Add(string Openid, int Recharge_Type, int Recharge_Currency, string Remark)
        {
            if (Openid.IsNullOrEmpty()) { throw new Exception("[Openid]不能为空"); }
            //if (Recharge_Currency <= 0) { throw new Exception("要充值的喵币应大于0"); }

            Book_User_Recharge model = new Book_User_Recharge();
            //model.Id = StringHelper.GuidTo16String();
            model.Create_Time = DateTime.Now;

            model.Recharge_Type = Recharge_Type;
            model.Recharge_Currency = Recharge_Currency;
            model.Remark = Remark;
            model.Openid = Openid;

            base.Add(model);

            return model;
        }
        #endregion

        #region 获取购买/赠送记录
        /// <summary>
        /// 获取购买/赠送记录
        /// </summary>
        /// <param name="RechargeType"></param>
        /// <param name="openid"></param>
        /// <param name="top">获取前几条记录</param>
        /// <returns></returns>
        public IList<Book_User_Recharge> GetRecoreds(Cat.Enums.Book.RechargeType RechargeType, string openid, int top)
        {
            int rechargeType = (int)RechargeType;
            var list = base.GetList(w => w.Openid == openid && w.Recharge_Type == rechargeType).OrderByDescending(o => o.Create_Time).Take(top).ToList();
            return list;
        }
        #endregion

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <param name="RechargeType"></param>
        /// <param name="openid"></param>
        /// <param name="top">获取前几条记录</param>
        /// <returns></returns>
        public IList<Book_User_Recharge> GetList(string openid)
        {
            var list = base.GetList(w => w.Openid == openid);
            return list;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryPager_Book_User"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(QueryPager_Book_User_Recharge queryPager_Book_User)
        {
            //查询表达式
            var exp = Cat.M.Book.Services.Helper.ExpressionHelper.GetExpressionByQueryPager<QueryPager_Book_User_Recharge, Book_User_Recharge>(queryPager_Book_User);
            //排序
            IList<OrderBy> listOrderBy = new List<OrderBy>();
            if (!string.IsNullOrEmpty(queryPager_Book_User.sorter))
                listOrderBy.Add(new OrderBy() { Order = queryPager_Book_User.order, Sort = queryPager_Book_User.sort });

            return base.GetByPage(queryPager_Book_User.pn, queryPager_Book_User.ps, exp, listOrderBy);
        }
    }
}
