using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Cat.Utility;
using Cat.M.Book.Models.ModelBinder.QueryModels;

namespace Cat.M.Book.Services
{
    public class AppBaseService<T> where T : Cat.M.Book.Models.BaseEntity
    {
        protected AppDbContext db;
        public AppBaseService()
        {
            db = new AppDbContext();
        }

        internal DbSet<T> Entities => db.Set<T>();

        protected int Add(T t, bool saveChanges = true)
        {
            if (string.IsNullOrEmpty(t.Id)) t.Id = StringHelper.GetUUID().ToString();
            if (t.Sort_Num == 0) t.Sort_Num = StringHelper.GetSortNum();

            db.Add(t);
            if (saveChanges)
                return db.SaveChanges();
            else
                return 0;
        }

        protected int Add(List<T> list, bool saveChanges = true)
        {
            foreach (var t in list)
            {
                if (string.IsNullOrEmpty(t.Id)) t.Id = StringHelper.GetUUID().ToString();
                if (t.Sort_Num == 0) t.Sort_Num = StringHelper.GetSortNum();
                db.Add(t);
            }
            if (saveChanges)
                return db.SaveChanges();
            else
                return 0;
        }

        protected int Update(T t, bool saveChanges = true)
        {
            db.Update(t);
            if (saveChanges)
                return db.SaveChanges();
            else
                return 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(T entity, bool saveChanges = true)
        {
            db.Entry(entity).State = EntityState.Deleted;
            if (saveChanges)
            {
                return db.SaveChanges();
            }
            else
                return 0;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected int Delete(string id, bool saveChanges = true)
        {
            var entity = this.GetByKey(id);
            if (entity == null)
            {
                throw new Exception("找不到对应的实体数据");
            }
            else
            {
                return Delete(entity);
            }
        }
        /// <summary>
        /// 删除多个
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        protected int Delete(List<string> ids, bool saveChanges = true)
        {
            int i = 0;
            foreach (var id in ids)
            {
                i += Delete(id, saveChanges);
            }
            return i;
        }

        /// <summary>
        /// 根据主键列的值查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected T GetByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception("主键列的值不能为空");

            return Entities.Find(key);
        }

        protected int GetCount(Expression<Func<T, bool>> predicate)
        {
            return Entities.Count(predicate);
        }

        protected T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return Entities.Where(predicate).FirstOrDefault();
        }

        protected List<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return Entities.Where(predicate).ToList();
        }

        /// <summary>
        /// 分页搜索
        /// 默认为“创建时间”倒序
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        protected List<T> GetByList(int pn, int ps, Expression<Func<T, bool>> expression = null, IList<OrderBy> listOrderBy = null)
        {
            Dictionary<string, string> dicOrderBy = new Dictionary<string, string>();
            if (listOrderBy == null || listOrderBy.Count == 0)
            {
                //dicOrderBy.Add("Sort_Num", "desc");
                dicOrderBy.Add("Create_Time", "desc");
            }
            else
            {
                foreach (var item in listOrderBy)
                {
                    dicOrderBy.Add(item.Sort, item.Order);
                }
            }

            return Entities.Where(expression).OrderBy(dicOrderBy).Skip((pn - 1) * ps).Take(ps).ToList();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <param name="expression"></param>
        /// <param name="listOrderBy"></param>
        /// <returns></returns>
        protected Models.Response.PageResult GetByPage(int pn, int ps, Expression<Func<T, bool>> expression = null, IList<OrderBy> listOrderBy = null)
        {
            if (ps > 1000) ps = 1000;

            Dictionary<string, string> dicOrderBy = new Dictionary<string, string>();
            if (listOrderBy == null || listOrderBy.Count == 0)
            {
                //dicOrderBy.Add("Sort_Num", "desc");
                dicOrderBy.Add("Create_Time", "desc");
            }
            else
            {
                foreach (var item in listOrderBy)
                {
                    dicOrderBy.Add(item.Sort, item.Order);
                }
            }

            var list = Entities.Where(expression).OrderBy(dicOrderBy).Skip((pn - 1) * ps).Take(ps).ToList();
            int count = Entities.Count(expression);

            int pages = (count / ps) + (count % ps > 0 ? 1 : 0);

            return new Models.Response.PageResult()
            {
                Pageindex = pn,
                Pagesize = ps,
                Count = count,
                Pages = pages,
                List = list
            };
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="requestParams"></param>
        /// <param name="expression"></param>
        /// <param name="listOrderBy"></param>
        /// <returns></returns>
        protected Models.Response.PageResult GetByPage(QueryPager requestParams, Expression<Func<T, bool>> expression = null, IList<OrderBy> listOrderBy = null)
        {
            return this.GetByPage(requestParams.pn, requestParams.ps, expression, listOrderBy);
        }

        protected List<T> GetAll(Expression<Func<T, bool>> expression = null)
        {
            return Entities.Where(expression).ToList();
        }


    }
}
