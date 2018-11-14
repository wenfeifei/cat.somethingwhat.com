using Cat.Foundation;
using Cat.M.Log.Models.Table;
using Cat.M.Log.Services.Enum;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Linq.Expressions;
using Cat.Utility;

namespace Cat.M.Log.Services
{
    /// <summary>
    /// 操作日志 服务类
    /// </summary>
    public class SysActionLogService : CatLogBaseService<Sys_Action_Log>
    {
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="remark"></param>
        public void AddLog(LogLevel logLevel, string message, string remark = "")
        {
            try
            {
                Sys_Action_Log entity = new Sys_Action_Log()
                {
                    TraceIdentifier = CatContext.HttpContext.TraceIdentifier,
                    Level = logLevel.ToString(),
                    Message = message,
                    Remark = remark,
                    //Client_IP = CatContext.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString()
                };

                entity.Create_Time = entity.Create_Time ?? DateTime.Now;
                base.InsertOne(entity);
            }
            catch (Exception ex)
            {
                FileHelper.CreateFiles($"~/log/exception/Sys_Action_Log.{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.log", ex);
            }
        }

        /// <summary>
        /// 分页搜索
        /// 默认为“创建时间”倒序
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public new List<Sys_Action_Log> GetByPage(int pn, int ps, Expression<Func<Sys_Action_Log, bool>> expression = null)
        {
            return base.GetByPage(pn, ps, expression).List as List<Sys_Action_Log>;
        }

        /// <summary>
        /// 获取日志信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sys_Action_Log GetSingle(string id)
        {
            return base.GetSingle(w => w.Id == id);
        }
    }
}
