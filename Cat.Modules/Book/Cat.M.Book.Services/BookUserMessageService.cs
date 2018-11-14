using Cat.M.Book.Models.Table;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Cat.M.Book.Models.ModelBinder.ActionModels;
using Cat.M.Book.Models.ModelBinder.QueryModels;

namespace Cat.M.Book.Services
{
    /// <summary>
    /// 系统信息表 服务类
    /// </summary>
    public class BookUserMessageService : AppBaseService<Book_User_Message>
    {
        public new Book_User_Message GetByKey(string id)
        {
            var instance = base.GetByKey(id);
            return instance;
        }

        public int Add(Book_User_Message entity)
        {
            if (string.IsNullOrEmpty(entity.Content)) throw new Exception("请填写内容");

            entity.Openid = entity.Openid ?? "";
            entity.Create_Time = DateTime.Now;
            return base.Add(entity);
        }

        /// <summary>
        /// 列表数据
        /// </summary>
        /// <returns></returns>
        public IList<Book_User_Message> GetListByOpenid(string openid, int top)
        {
            var list = base.GetByList(1, top, w => w.Openid == openid || w.Openid == "" || w.Openid == null);
            return list;
        }

        /// <summary>
        /// 列表数据（用户未阅读过的）
        /// </summary>
        /// <returns></returns>
        public IList<Book_User_Message> GetListByNoRead(string openid, int top)
        {
            var record = AllServices.BookUserMessageReadRecordService.Entities.Where(w => w.Openid == openid);
            var q = from a in base.Entities
                    join b in record on a.Id equals b.Book_User_Message_Id into temp
                    from tt in temp.DefaultIfEmpty()
                    where a.Openid == "" || a.Openid == null || a.Openid == openid
                    where tt == null
                    select new Book_User_Message()
                    {
                        Id = a.Id,
                        Openid = a.Openid,
                        Content = a.Content,
                        Create_Time = a.Create_Time,
                    };
            var list = q.OrderByDescending(o => o.Create_Time).Take(top).ToList();
            //var list = base.GetByPage(1, top, w => w.Openid == openid || w.Openid == "");
            return list;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryPager_Book_User"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(QueryPager_Book_User_Message queryPager_Book_User)
        {
            //查询表达式
            var exp = Cat.M.Book.Services.Helper.ExpressionHelper.GetExpressionByQueryPager<QueryPager_Book_User_Message, Book_User_Message>(queryPager_Book_User);
            //排序
            IList<OrderBy> listOrderBy = new List<OrderBy>();
            if (!string.IsNullOrEmpty(queryPager_Book_User.sorter))
                listOrderBy.Add(new OrderBy() { Order = queryPager_Book_User.order, Sort = queryPager_Book_User.sort });

            return base.GetByPage(queryPager_Book_User.pn, queryPager_Book_User.ps, exp, listOrderBy);
        }


        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Book_User_Message Add(Add_Book_User_Message_Input inputModel)
        {
            if (inputModel.Content.IsNullOrEmpty()) { throw new Exception("不能为空"); }
            
            Book_User_Message model = new Book_User_Message();
            //model.Id = StringHelper.GuidTo16String();
            model.Create_Time = DateTime.Now;

            model.Openid = inputModel.Openid;
            model.Content = inputModel.Content;

            base.Add(model);

            return model;
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Book_User_Message Update(Update_Book_User_Message_Input model)
        {
            if (model.Id.IsNullOrEmpty() || model.Content.IsNullOrEmpty()) { throw new Exception("不能为空"); }

            Book_User_Message instance = base.GetByKey(model.Id);
            instance.Update_Time = DateTime.Now;

            instance.Openid = model.Openid;
            instance.Content = model.Content;

            base.Update(instance);

            return instance;
        }
        #endregion

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int Delete(List<string> ids)
        {
            if (ids.Count == 0) throw new Exception("请选择要删除的数据");
            return base.Delete(ids);
        }

    }
}
