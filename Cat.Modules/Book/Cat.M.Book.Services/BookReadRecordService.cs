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
    /// 书本阅读记录 服务类
    /// </summary>
    public class BookReadRecordService : AppBaseService<Book_Read_Record>
    {
        public int Add(Book_Read_Record entity)
        {
            if (string.IsNullOrEmpty(entity.Book_Name)) throw new Exception("Book_Name is null");
            if (string.IsNullOrEmpty(entity.Author)) throw new Exception("Author is null");
            if (string.IsNullOrEmpty(entity.Book_Link)) throw new Exception("Book_Link is null");
            if (entity.Rule == 0) throw new Exception("Rule is null");
            if (string.IsNullOrEmpty(entity.Openid)) throw new Exception("openid is null");

            //entity.Id = StringHelper.GetUUID().ToString();
            entity.Create_Time = entity.Create_Time ?? DateTime.Now;
            entity.IsHidden = entity.IsHidden ?? false;
            entity.AlreadyCollected = entity.AlreadyCollected ?? false;
            return base.Add(entity);
        }

        public int Update(Book_Read_Record entity)
        {
            entity.Update_Time = DateTime.Now;
            return base.Update(entity);
        }

        public Book_Read_Record GetSingle(string id)
        {
            var instance = base.GetSingle(w => w.Id == id);
            return instance;
        }

        public Book_Read_Record GetSingle(string openid, string book_Name, string author, int rule)
        {
            if (string.IsNullOrEmpty(book_Name)) throw new Exception("Book_Name is null");
            if (string.IsNullOrEmpty(author)) throw new Exception("Author is null");
            if (rule == 0) throw new Exception("Rule is null");

            var instance = base.GetSingle(w => w.Openid == openid && w.Book_Name == book_Name && w.Author == author && w.Rule == rule);
            return instance;
        }

        /// <summary>
        /// 更新收藏状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="alreadyCollected">是否收藏</param>
        /// <returns></returns>
        public int UpdateCollectedState(string id, bool alreadyCollected)
        {
            var instance = base.GetSingle(w => w.Id == id);
            if (instance == null) throw new Exception("没有找到要更新的数据");

            instance.AlreadyCollected = alreadyCollected;
            return base.Update(instance);
        }

        #region 获取最近的阅读
        /// <summary>
        /// 获取最近的阅读
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public List<Book.Models.ModelBinder.ReturnModels.Recent_Reading> GetRecentReading(string openid, int pn, int ps, out int count)
        {
            //最近阅读的书本
            count = base.GetCount(w => w.Openid == openid && w.Last_Reading_Record_Id != null);
            var list = base.GetList(w => w.Openid == openid && w.Last_Reading_Record_Id != null).OrderByDescending(o => o.Update_Time).ThenByDescending(o => o.Create_Time).Skip((pn - 1) * ps).Take(ps).ToList();
            //最近阅读的章节
            var chapters = list.Select(s => s.Last_Reading_Record_Id);
            //var listChapter = ServiceRepository.BookReadingRecordRepository.Get(w => chapters.Contains(w.Id) == true).ToList();
            var listChapter = AllServices.BookChapterReadRecordService.GetList(w => chapters.Contains(w.Id)).ToList();

            //构造返回数据格式
            var recentReadingList = new List<Book.Models.ModelBinder.ReturnModels.Recent_Reading>();
            foreach (var item in list)
            {
                var tempModel = listChapter.Where(w => w.Id == item.Last_Reading_Record_Id).FirstOrDefault();

                if (tempModel == null) continue;

                recentReadingList.Add(new Book.Models.ModelBinder.ReturnModels.Recent_Reading()
                {
                    Book_Info_Id = item.Id,
                    Author = item.Author,
                    Book_Name = item.Book_Name,
                    Book_Link = item.Book_Link,
                    Chapter_Name = tempModel.Chapter_Name,
                    Chapter_Link = tempModel.Chapter_Link,
                    Create_Time = item.Create_Time,
                    Update_Time = item.Update_Time
                });
            }

            return recentReadingList;
        }
        /// <summary>
        /// 获取最近的阅读
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetRecentReadingByPage(string openid, int pn, int ps)
        {
            int count;
            var list = GetRecentReading(openid, pn, ps, out count);

            //return recentReadingList;

            return new Models.Response.PageResult()
            {
                Pageindex = pn,
                Pagesize = ps,
                Count = count,
                Pages = (count / ps) + (count % ps > 0 ? 1 : 0),
                List = list
            };
        }
        #endregion

        #region 获取用户阅读过的小说书本数量
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

        #region 获取用户收藏的小说数
        /// <summary>
        /// 获取用户收藏的小说数
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="Is_Deleted"></param>
        /// <returns></returns>
        public int GetCollectionCount(string openid)
        {
            if (string.IsNullOrEmpty(openid)) throw new Exception("[openid]不能为空");

            return base.GetCount(w => w.Openid == openid && w.AlreadyCollected == true);
        }
        #endregion

        #region 获取用户收藏的小说
        /// <summary>
        /// 获取用户收藏的小说
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="Is_Deleted"></param>
        /// <returns></returns>
        public IList<Book_Read_Record> GetCollectedList(string openid)
        {
            if (string.IsNullOrEmpty(openid)) throw new Exception("[openid]不能为空");

            return base.GetList(w => w.Openid == openid && w.AlreadyCollected == true).OrderByDescending(o => o.Create_Time).ToList();
        }
        #endregion


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryPager_Book_User"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(QueryPager_Book_Read_Record queryPager_Book_User)
        {
            //查询表达式
            var exp = Cat.M.Book.Services.Helper.ExpressionHelper.GetExpressionByQueryPager<QueryPager_Book_Read_Record, Book_Read_Record>(queryPager_Book_User);
            //排序
            Dictionary<string, string> dicOrderBy = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(queryPager_Book_User.sorter))
                dicOrderBy.Add(queryPager_Book_User.sort, queryPager_Book_User.order);
            else
                dicOrderBy.Add("Create_Time", "desc");

            var q = from a in base.Entities.Where(exp)
                    join t in AllServices.BookChapterReadRecordService.Entities on a.Last_Reading_Record_Id equals t.Id into temp
                    from tt in temp.DefaultIfEmpty()
                    select new Book_Read_Record()
                    {
                        Id = a.Id,
                        Rule = a.Rule,
                        Author = a.Author,
                        Book_Name = a.Book_Name,
                        Book_Classify = a.Book_Classify,
                        Book_Link = a.Book_Link,
                        Cover_Image = a.Cover_Image,
                        Book_Intro = a.Book_Intro,
                        Openid = a.Openid,
                        Last_Reading_Record_Id = a.Last_Reading_Record_Id,
                        IsHidden = a.IsHidden,
                        AlreadyCollected = a.AlreadyCollected,
                        Create_Time = a.Create_Time,
                        Update_Time = a.Update_Time,
                        Last_Reading_ChapterName = tt.Chapter_Name,
                        Last_Reading_ChapterLink = tt.Chapter_Link
                    };

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
