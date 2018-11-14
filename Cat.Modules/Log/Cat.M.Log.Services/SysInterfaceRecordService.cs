using Cat.Foundation;
using Cat.M.Log.Models.Table;
using Cat.M.Log.Services.Enum;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using Cat.Utility;
using System.Linq.Expressions;
using Cat.M.Log.Models.ModelBinder.QueryModels;

namespace Cat.M.Log.Services
{
    /// <summary>
    /// 接口访问记录 服务类
    /// </summary>
    public class SysInterfaceRecordService : CatLogBaseService<Sys_Interface_Record>
    {
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="remark"></param>
        public void AddLog(Cat.Enums.RecordType recordType, long millisecond, string remark = "")
        {
            try
            {
                var httpContext = CatContext.HttpContext;

                Sys_Interface_Record entity = new Sys_Interface_Record()
                {
                    TraceIdentifier = httpContext.TraceIdentifier,
                    Millisecond = millisecond,
                    RecordType = recordType.ToString(),
                    Host = httpContext.Request.Host.ToString(),
                    Path = httpContext.Request.Path,
                    Method = httpContext.Request.Method,
                    Url = httpContext.Request.Scheme + "://" + httpContext.Request.Host + httpContext.Request.Path + httpContext.Request.QueryString,
                    Referer = httpContext.Request.Headers["Referer"].ToStr(),
                    ContentType = httpContext.Request.ContentType,
                    RequestParams = RequestHelper.GetParms(httpContext.Request),
                    User_Agent = httpContext.Request.Headers["User-Agent"].ToStr(),
                    RemoteIpAddress = IPHelper.GetClientIP(),
                    Remark = remark,
                };

                entity.Create_Time = entity.Create_Time ?? DateTime.Now;
                base.InsertOne(entity);
            }
            catch (Exception ex)
            {
                FileHelper.CreateFiles($"~/log/exception/Sys_Interface_Record.{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", ex);
            }
        }

        ///// <summary>
        ///// 分页搜索
        ///// 默认为“创建时间”倒序
        ///// </summary>
        ///// <param name="pn"></param>
        ///// <param name="ps"></param>
        ///// <returns></returns>
        //public new List<Sys_Interface_Record> GetByPage(int pn, int ps, Expression<Func<Sys_Interface_Record, bool>> expression = null)
        //{
        //    return base.GetByPage(pn, ps, expression);
        //}

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(QueryPager_Sys_Interface_Record requestParams)
        {
            //查询表达式
            var exp = Cat.M.Log.Services.Helper.ExpressionHelper.GetExpressionByQueryPager<QueryPager_Sys_Interface_Record, Sys_Interface_Record>(requestParams);

            var pageObj = base.GetByPage(requestParams.pn, requestParams.ps, exp);

            //补时差
            foreach(var item in pageObj.List as List<Sys_Interface_Record>)
            {
                item.Create_Time = item.Create_Time.ToDateTime().AddHours(8);
            }

            return pageObj;
        }

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_Interface_Record GetSingle(string id)
        {
            return base.GetSingle(w => w.Id == id);
        }
    }
}
