using Cat.M.Book.Models.ModelBinder.QueryModels;
using Cat.M.Book.Models.Table;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services
{
    /// <summary>
    /// 用户消费记录表 服务类
    /// </summary>
    public class BookUserConsumeService : AppBaseService<Book_User_Consume>
    {
        public int Add(Book_User_Consume entity)
        {
            //entity.Id = StringHelper.GetUUID().ToString();
            entity.Create_Time = DateTime.Now;
            return base.Add(entity);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryPager_Book_User"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(QueryPager_Book_User_Consume queryPager_Book_User)
        {
            //查询表达式
            var exp = Cat.M.Book.Services.Helper.ExpressionHelper.GetExpressionByQueryPager<QueryPager_Book_User_Consume, Book_User_Consume>(queryPager_Book_User);
            //排序
            IList<OrderBy> listOrderBy = new List<OrderBy>();
            if (!string.IsNullOrEmpty(queryPager_Book_User.sorter))
                listOrderBy.Add(new OrderBy() { Order = queryPager_Book_User.order, Sort = queryPager_Book_User.sort });

            return base.GetByPage(queryPager_Book_User.pn, queryPager_Book_User.ps, exp, listOrderBy);
        }
    }
}
