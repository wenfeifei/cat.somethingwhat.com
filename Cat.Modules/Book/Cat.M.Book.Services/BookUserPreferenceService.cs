using Cat.M.Book.Models.ModelBinder.QueryModels;
using Cat.M.Book.Models.Table;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services
{
    /// <summary>
    /// 用户偏好设置 服务类
    /// </summary>
    public class BookUserPreferenceService : AppBaseService<Book_User_Preference>
    {
        /// <summary>
        /// 获取用户偏好
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public Book_User_Preference GetSingleByOpenid(string openid)
        {
            var instance = base.GetSingle(w => w.Openid == openid);
            return instance;
        }

        #region 新增或更新
        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Book_User_Preference AddOrUpdateUserPreference(Book_User_Preference entity)
        {
            if (entity.Openid.IsNullOrEmpty()) { throw new Exception("[Openid]不能为空"); }

            Book_User_Preference instance = this.GetSingleByOpenid(entity.Openid);
            bool isAdd = true;

            if (instance == null)
            {
                isAdd = true;
                instance = new Book_User_Preference();
                //instance.Id = StringHelper.GuidTo16String();
                instance.Create_Time = DateTime.Now;
            }
            else
            {
                isAdd = false;
                instance.Update_Time = DateTime.Now;
            }

            instance.Openid = entity.Openid;
            instance.FontSize = entity.FontSize;
            instance.FontColor = entity.FontColor;
            instance.FontFamily = entity.FontFamily;
            instance.ScreenBrightness = entity.ScreenBrightness;
            instance.KeepScreenOn = entity.KeepScreenOn;
            instance.BackgroundColor = entity.BackgroundColor;

            if (isAdd)
                base.Add(instance);
            else
                base.Update(instance);

            return instance;
        }
        #endregion

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(QueryPager_Book_User_Preference requestParams)
        {
            //查询表达式
            var exp = Cat.M.Book.Services.Helper.ExpressionHelper.GetExpressionByQueryPager<QueryPager_Book_User_Preference, Book_User_Preference>(requestParams);
            //排序
            IList<OrderBy> listOrderBy = new List<OrderBy>();
            if (!string.IsNullOrEmpty(requestParams.sorter))
                listOrderBy.Add(new OrderBy() { Order = requestParams.order, Sort = requestParams.sort });

            return base.GetByPage(requestParams.pn, requestParams.ps, exp, listOrderBy);
        }

    }
}
