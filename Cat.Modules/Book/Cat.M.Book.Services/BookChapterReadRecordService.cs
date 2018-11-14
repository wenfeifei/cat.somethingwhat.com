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
    /// 书本章节阅读记录 服务类
    /// </summary>
    public class BookChapterReadRecordService : AppBaseService<Book_Chapter_Read_Record>
    {

        public Book_Chapter_Read_Record GetSingle(string id)
        {
            var instance = base.GetSingle(w => w.Id == id);
            return instance;
        }

        public new List<Book_Chapter_Read_Record> GetList(Expression<Func<Book_Chapter_Read_Record, bool>> predicate)
        {
            var list = base.GetList(predicate);
            return list;
        }

        #region 更新阅读时长
        /// <summary>
        /// 更新阅读时长
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Duration"></param>
        /// <returns></returns>
        public int UpdateDuration(string Id, int Duration)
        {
            var instance = base.GetSingle(w => w.Id == Id);
            if (instance == null) throw new Exception($"更新阅读时长失败，Id：{Id}");

            instance.Duration = instance.Duration.ToInt32(0) + Duration;
            return base.Update(instance);
        }
        #endregion

        #region 新增或更新章节阅读记录
        /// <summary>
        /// 新增或更新章节阅读记录
        /// </summary>
        /// <param name="entity"></param>
        public Book_Chapter_Read_Record AddOrUpdate(Book_Chapter_Read_Record entity)
        {
            if (entity.Chapter_Link.IsNullOrEmpty()) throw new Exception("[Chapter_Link]没有正确获取到");
            //if (entity.Openid.IsNullOrEmpty()) throw new Exception("[Openid]没有正确获取到");

            Book_Chapter_Read_Record returnEntity = new Book_Chapter_Read_Record();
            var instance = base.GetSingle(w => w.Chapter_Link == entity.Chapter_Link && w.Openid == entity.Openid);

            if (instance == null)
            {
                //新增
                var tempModel = new Book_Chapter_Read_Record();
                //tempModel.Id = StringHelper.GetUUID().ToString();
                tempModel.Book_Read_Record_Id = entity.Book_Read_Record_Id;
                tempModel.Chapter_Name = entity.Chapter_Name;
                tempModel.Chapter_Link = entity.Chapter_Link;
                tempModel.Number_Of_Words = entity.Number_Of_Words;
                tempModel.Create_Time = DateTime.Now;
                tempModel.Openid = entity.Openid;
                tempModel.Duration = 0;
                base.Add(tempModel);
                tempModel.Flag = "add";
                returnEntity = tempModel;
            }
            else
            {
                //修改
                instance.Number_Of_Words = entity.Number_Of_Words;
                instance.Update_Time = DateTime.Now;
                instance.Duration = instance.Duration.ToInt(0);
                base.Update(instance);
                instance.Flag = "update";
                returnEntity = instance;
            }

            //要修改Book_Info的信息
            var tempInstance = AllServices.BookReadRecordService.GetSingle(returnEntity.Book_Read_Record_Id);
            tempInstance.Last_Reading_Record_Id = returnEntity.Id;
            AllServices.BookReadRecordService.Update(tempInstance);

            return returnEntity;
        }
        #endregion

        #region 获取用户阅读过的小说章节数
        /// <summary>
        /// 获取用户阅读过的小说书本数量
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public int GetCountByOpenid(string openid)
        {
            if (string.IsNullOrEmpty(openid)) throw new Exception("[openid]不能为空");

            return base.GetCount(w => w.Openid == openid);
        }
        #endregion

        #region 获取指定用户的总的阅读时长（秒）
        /// <summary>
        /// 获取指定用户的总的阅读时长（秒）
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public int GetDuration(string openid)
        {
            return base.Entities.Where(w => w.Openid == openid).Sum(s => s.Duration);
        }
        #endregion

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryPager_Book_User"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(QueryPager_Book_Chapter_Read_Record queryPager_Book_User, string Book_Name, string Author)
        {
            //查询表达式
            var exp = Cat.M.Book.Services.Helper.ExpressionHelper.GetExpressionByQueryPager<QueryPager_Book_Chapter_Read_Record, Book_Chapter_Read_Record>(queryPager_Book_User);
            //排序
            Dictionary<string, string> dicOrderBy = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(queryPager_Book_User.sorter))
                dicOrderBy.Add(queryPager_Book_User.sort, queryPager_Book_User.order);
            else
                dicOrderBy.Add("Create_Time", "desc");

            var q = from a in base.Entities.Where(exp)
                    join t in AllServices.BookReadRecordService.Entities on a.Book_Read_Record_Id equals t.Id into temp
                    from tt in temp.DefaultIfEmpty()
                    select new Book_Chapter_Read_Record()
                    {
                        Id = a.Id,
                        Book_Read_Record_Id = a.Book_Read_Record_Id,
                        Openid = a.Openid,
                        Chapter_Name = a.Chapter_Name,
                        Chapter_Link = a.Chapter_Link,
                        Number_Of_Words = a.Number_Of_Words,
                        Duration = a.Duration,
                        Book_Name = tt.Book_Name,
                        Book_Link = tt.Book_Link,
                        Author = tt.Author,
                        Create_Time = a.Create_Time,
                        Update_Time = a.Update_Time
                    };

            if (!string.IsNullOrEmpty(Book_Name)) q = q.Where(w => w.Book_Name.Contains(Book_Name));
            if (!string.IsNullOrEmpty(Author)) q = q.Where(w => w.Author.Contains(Author));

            int count = q.Count();
            var list = q.OrderBy(dicOrderBy).Skip((queryPager_Book_User.pn - 1) * queryPager_Book_User.ps).Take(queryPager_Book_User.ps).ToList();

            return new Models.Response.PageResult()
            {
                Count = count,
                List = list,
                Pageindex = queryPager_Book_User.pn,
                Pagesize = queryPager_Book_User.ps,
                Pages = (count / queryPager_Book_User.ps) + (count % queryPager_Book_User.ps > 0 ? 1 : 0)
            };

            //return base.GetByPage(queryPager_Book_User.pn, queryPager_Book_User.ps, exp, listOrderBy);
        }
    }
}
