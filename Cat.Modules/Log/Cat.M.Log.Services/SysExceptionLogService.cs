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
    /// 操作日志 服务类
    /// </summary>
    public class SysExceptionLogService : CatLogBaseService<Sys_Exception_Log>
    {
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="exceptionType"></param>
        /// <param name="message"></param>
        /// <param name="remark"></param>
        public void AddLog(ExceptionType exceptionType, Exception ex, string remark = "")
        {
            try
            {
                Sys_Exception_Log entity = new Sys_Exception_Log()
                {
                    TraceIdentifier = CatContext.HttpContext.TraceIdentifier,
                    ExceptionType = exceptionType.ToString(),
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException.ToStr(),
                    Source = ex.Source,
                    TargetSite = ex.TargetSite.ToStr(),
                    Remark = remark,
                };

                entity.Create_Time = entity.Create_Time ?? DateTime.Now;
                base.InsertOne(entity);
            }
            catch (Exception ex2)
            {
                FileHelper.CreateFiles($"~/log/exception/Sys_Exception_Log.{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", ex2);
            }
        }

        /// <summary>
        /// 分页搜索
        /// 默认为“创建时间”倒序
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public new List<Sys_Exception_Log> GetByPage(int pn, int ps, Expression<Func<Sys_Exception_Log, bool>> expression = null)
        {
            return base.GetByPage(pn, ps, expression).List as List<Sys_Exception_Log>;
        }

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_Exception_Log GetSingle(string id)
        {
            return base.GetSingle(w => w.Id == id);
        }
    }
}
