using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MongoDB.Driver;
using Cat.Utility;

namespace Cat.M.Log.Services
{
    public class AppBaseService<T> where T : Cat.M.Log.Models.BaseEntity
    {
        protected AppDbContext db;
        private string dbName;
        public AppBaseService(string dbName)
        {
            db = new AppDbContext();
            this.dbName = dbName;
        }

        /// <summary>
        /// 得到一个数据库
        /// 若数据库不存在，会在首次使用数据库的时候进行自动创建
        /// </summary>
        private IMongoDatabase GetDatabase()
        {
            return db.client.GetDatabase(dbName);
        }

        /// <summary>
        /// 获取数据集
        /// 若数据集不存在，会在首次使用的时候进行自动创建
        /// </summary>
        protected IMongoCollection<T> GetCollection()
        {
            string collectionName = typeof(T).Name;
            return GetDatabase().GetCollection<T>(collectionName);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="t"></param>
        protected void InsertOne(T t)
        {
            if (string.IsNullOrEmpty(t.Id)) t.Id = StringHelper.GetUUID().ToString();
            GetCollection().InsertOne(t);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entities"></param>
        protected void InsertMany(List<T> entities)
        {
            foreach (var t in entities)
            {
                if (string.IsNullOrEmpty(t.Id)) t.Id = StringHelper.GetUUID().ToString();
            }
            GetCollection().InsertMany(entities);
        }


        public T GetSingle(Expression<Func<T, bool>> expression)
        {
            var collections = GetCollection();
            return collections.Find(expression).FirstOrDefault();
        }

        public List<T> GetList(Expression<Func<T, bool>> expression)
        {
            var collections = GetCollection();
            return collections.Find(expression).ToList();
        }

        ///// <summary>
        ///// 分页搜索
        ///// 默认为“创建时间”倒序
        ///// </summary>
        ///// <param name="pn"></param>
        ///// <param name="ps"></param>
        ///// <returns></returns>
        //protected List<T> GetByPage(int pn, int ps, Expression<Func<T, bool>> expression = null)
        //{
        //    var collections = GetCollection();
        //    return collections.Find(expression).SortByDescending(o => o.Create_Time).Skip((pn - 1) * ps).Limit(ps).ToList();
        //}

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(int pn, int ps, Expression<Func<T, bool>> expression = null)
        {
            if (pn == 0) pn = 1;
            if (ps == 0) ps = 10;

            var collections = GetCollection();

            if (ps > 1000) ps = 1000;

            var list = collections.Find(expression).SortByDescending(o => o.Create_Time).Skip((pn - 1) * ps).Limit(ps).ToList();
            int count = collections.CountDocuments(expression).ToInt();

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

    }
}
