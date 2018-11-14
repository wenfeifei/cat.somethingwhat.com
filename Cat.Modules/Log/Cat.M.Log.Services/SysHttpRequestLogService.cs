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

namespace Cat.M.Log.Services
{
    /// <summary>
    /// http请求日志 服务类
    /// </summary>
    public class SysHttpRequestLogService : CatLogBaseService<Sys_HttpRequest_Log>
    {
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="remark"></param>
        public void AddLog(string remark = "")
        {
            try
            {
                var httpContext = CatContext.HttpContext;

                Sys_HttpRequest_Log entity = new Sys_HttpRequest_Log()
                {
                    TraceIdentifier = httpContext.TraceIdentifier,
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
                FileHelper.CreateFiles($"~/log/exception/Sys_HttpRequest_Log.{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", ex);
            }
        }

        /// <summary>
        /// 分页搜索
        /// 默认为“创建时间”倒序
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public new List<Sys_HttpRequest_Log> GetByPage(int pn, int ps, Expression<Func<Sys_HttpRequest_Log, bool>> expression = null)
        {
            return base.GetByPage(pn, ps, expression).List as List<Sys_HttpRequest_Log>;
        }

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_HttpRequest_Log GetSingle(string id)
        {
            return base.GetSingle(w => w.Id == id);
        }
    }
}
