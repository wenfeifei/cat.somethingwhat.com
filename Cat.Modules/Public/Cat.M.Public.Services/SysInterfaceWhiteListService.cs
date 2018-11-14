using Cat.M.Public.Models.ModelBinder.ActionModels;
using Cat.M.Public.Models.ModelBinder.QueryModels;
using Cat.M.Public.Models.Table;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Services
{
    /// <summary>
    /// 接口白名单表 服务类
    /// </summary>
    public class SysInterfaceWhiteListService : AppBaseService<Sys_Interface_WhiteList>
    {
        public new Sys_Interface_WhiteList GetByKey(string id)
        {
            var instance = base.GetByKey(id);
            return instance;
        }
        public Sys_Interface_WhiteList GetByAppid(string appid)
        {
            return base.GetSingle(w => w.Appid == appid);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryPager_Book_User"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(QueryPager_Sys_Interface_WhiteList queryPager_Book_User)
        {
            //查询表达式
            var exp = Cat.M.Public.Services.Helper.ExpressionHelper.GetExpressionByQueryPager<QueryPager_Sys_Interface_WhiteList, Sys_Interface_WhiteList>(queryPager_Book_User);
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
        public Sys_Interface_WhiteList Add(Add_Sys_Interface_WhiteList_Input inputModel)
        {
            if (inputModel.Appid.IsNullOrEmpty()) { throw new Exception("不能为空"); }

            //检查User_Id重复
            if (base.GetCount(w => w.Appid == inputModel.Appid) > 0) throw new Exception("Appid已存在，请填写其他");

            Sys_Interface_WhiteList model = new Sys_Interface_WhiteList();
            //model.Id = StringHelper.GuidTo16String();
            model.Create_Time = DateTime.Now;

            model.Appid = inputModel.Appid;
            model.Validity_Time = inputModel.Validity_Time.ToDateTime(Convert.ToDateTime("5555-05-05 05:05:05"));
            model.Remark = inputModel.Remark;

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
        public Sys_Interface_WhiteList Update(Update_Sys_Interface_WhiteList_Input model)
        {
            if (model.Id.IsNullOrEmpty() || model.Appid.IsNullOrEmpty()) { throw new Exception("不能为空"); }

            Sys_Interface_WhiteList instance = base.GetByKey(model.Id);
            instance.Update_Time = DateTime.Now;

            instance.Validity_Time = model.Validity_Time;
            instance.Remark = model.Remark;

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
