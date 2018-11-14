using Cat.Foundation;
using Cat.M.Log.Models.Table;
using Cat.M.Log.Services.Enum;
using Cat.M.Log.Services.Models.Response;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cat.M.Log.Services
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogService : ILogService
    {
        ILogService logService;

        public LogService()
        {
            if (Foundation.ConfigManager.CatSettings.LogService == "MongoDbService")
                logService = new MongoDbService();
            else if (Foundation.ConfigManager.CatSettings.LogService == "Log4NetService")
                logService = new Log4NetService();
            else
                throw new Exception("配置文件未配置LogService或找不到实现的方法");
        }


        #region debug
        /// <summary>
        /// DEBUG （调试信息）：记录系统用于调试的一切信息，内容或者是一些关键数据内容的输出。 下面演示根据每个日志等级生成对应的一个文件。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        public void Debug(string title, Exception ex = null)
        {
            Debug(new Sys_Log()
            {
                Title = title,
            }, ex);
        }
        /// <summary>
        /// DEBUG （调试信息）：记录系统用于调试的一切信息，内容或者是一些关键数据内容的输出。 下面演示根据每个日志等级生成对应的一个文件。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Debug(string title, string message = "", Exception ex = null)
        {
            Debug(new Sys_Log()
            {
                Title = title,
                Message = message,
            }, ex);
        }
        /// <summary>
        /// DEBUG （调试信息）：记录系统用于调试的一切信息，内容或者是一些关键数据内容的输出。 下面演示根据每个日志等级生成对应的一个文件。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public void Debug(string title, string message = "", string info = "", Exception ex = null)
        {
            Debug(new Sys_Log()
            {
                Title = title,
                Message = message,
                Info = info,
            }, ex);
        }
        /// <summary>
        /// DEBUG （调试信息）：记录系统用于调试的一切信息，内容或者是一些关键数据内容的输出。 下面演示根据每个日志等级生成对应的一个文件。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ex"></param>
        public void Debug(Sys_Log entity, Exception ex = null)
        {
            entity.Id = StringHelper.GetUUID().ToString();
            entity.Create_Time = DateTime.Now;
            entity.TraceIdentifier = CatContext.HttpContext.TraceIdentifier;
            entity.Level = LogLevel.DEBUG.ToString().ToLower();

            logService.Debug(entity, ex);
        }
        #endregion


        #region info
        /// <summary>
        /// INFO（一般信息）：记录系统运行中应该让用户知道的基本信息。例如，服务开始运行，功能已经开户等。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        public void Info(string title, Exception ex = null)
        {
            Info(new Sys_Log()
            {
                Title = title,
            }, ex);
        }
        /// <summary>
        /// INFO（一般信息）：记录系统运行中应该让用户知道的基本信息。例如，服务开始运行，功能已经开户等。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Info(string title, string message = "", Exception ex = null)
        {
            Info(new Sys_Log()
            {
                Title = title,
                Message = message,
            }, ex);
        }
        /// <summary>
        /// INFO（一般信息）：记录系统运行中应该让用户知道的基本信息。例如，服务开始运行，功能已经开户等。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public void Info(string title, string message = "", string info = "", Exception ex = null)
        {
            Info(new Sys_Log()
            {
                Title = title,
                Message = message,
                Info = info,
            }, ex);
        }
        /// <summary>
        /// INFO（一般信息）：记录系统运行中应该让用户知道的基本信息。例如，服务开始运行，功能已经开户等。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ex"></param>
        public void Info(Sys_Log entity, Exception ex = null)
        {
            entity.Id = StringHelper.GetUUID().ToString();
            entity.Create_Time = DateTime.Now;
            entity.TraceIdentifier = CatContext.HttpContext.TraceIdentifier;
            entity.Level = LogLevel.INFO.ToString().ToLower();

            logService.Info(entity, ex);
        }
        #endregion


        #region warn
        /// <summary>
        /// WARN（警告）：记录系统中不影响系统继续运行，但不符合系统运行正常条件，有可能引起系统错误的信息。例如，记录内容为空，数据内容不正确等。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        public void Warn(string title, Exception ex = null)
        {
            Warn(new Sys_Log()
            {
                Title = title,
            }, ex);
        }
        /// <summary>
        /// WARN（警告）：记录系统中不影响系统继续运行，但不符合系统运行正常条件，有可能引起系统错误的信息。例如，记录内容为空，数据内容不正确等。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Warn(string title, string message = "", Exception ex = null)
        {
            Warn(new Sys_Log()
            {
                Title = title,
                Message = message,
            }, ex);
        }
        /// <summary>
        /// WARN（警告）：记录系统中不影响系统继续运行，但不符合系统运行正常条件，有可能引起系统错误的信息。例如，记录内容为空，数据内容不正确等。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public void Warn(string title, string message = "", string info = "", Exception ex = null)
        {
            Warn(new Sys_Log()
            {
                Title = title,
                Message = message,
                Info = info,
            }, ex);
        }
        /// <summary>
        /// WARN（警告）：记录系统中不影响系统继续运行，但不符合系统运行正常条件，有可能引起系统错误的信息。例如，记录内容为空，数据内容不正确等。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ex"></param>
        public void Warn(Sys_Log entity, Exception ex = null)
        {
            entity.Id = StringHelper.GetUUID().ToString();
            entity.Create_Time = DateTime.Now;
            entity.TraceIdentifier = CatContext.HttpContext.TraceIdentifier;
            entity.Level = LogLevel.WARN.ToString().ToLower();

            logService.Warn(entity, ex);
        }
        #endregion


        #region error
        /// <summary>
        /// ERROR（一般错误）：记录系统中出现的导致系统不稳定，部分功能出现混乱或部分功能失效一类的错误。例如，数据字段为空，数据操作不可完成，操作出现异常等。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        public void Error(string title, Exception ex = null)
        {
            Error(new Sys_Log()
            {
                Title = title,
            }, ex);
        }
        /// <summary>
        /// ERROR（一般错误）：记录系统中出现的导致系统不稳定，部分功能出现混乱或部分功能失效一类的错误。例如，数据字段为空，数据操作不可完成，操作出现异常等。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Error(string title, string message = "", Exception ex = null)
        {
            Error(new Sys_Log()
            {
                Title = title,
                Message = message,
            }, ex);
        }
        /// <summary>
        /// ERROR（一般错误）：记录系统中出现的导致系统不稳定，部分功能出现混乱或部分功能失效一类的错误。例如，数据字段为空，数据操作不可完成，操作出现异常等。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public void Error(string title, string message = "", string info = "", Exception ex = null)
        {
            Error(new Sys_Log()
            {
                Title = title,
                Message = message,
                Info = info,
            }, ex);
        }
        /// <summary>
        /// ERROR（一般错误）：记录系统中出现的导致系统不稳定，部分功能出现混乱或部分功能失效一类的错误。例如，数据字段为空，数据操作不可完成，操作出现异常等。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ex"></param>
        public void Error(Sys_Log entity, Exception ex = null)
        {
            entity.Id = StringHelper.GetUUID().ToString();
            entity.Create_Time = DateTime.Now;
            entity.TraceIdentifier = CatContext.HttpContext.TraceIdentifier;
            entity.Level = LogLevel.ERROR.ToString().ToLower();

            logService.Error(entity, ex);
        }
        #endregion


        #region fatal
        /// <summary>
        /// FATAL（致命错误）：记录系统中出现的能使用系统完全失去功能，服务停止，系统崩溃等使系统无法继续运行下去的错误。例如，数据库无法连接，系统出现死循环。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        public void Fatal(string title, Exception ex = null)
        {
            Fatal(new Sys_Log()
            {
                Title = title,
            }, ex);
        }
        /// <summary>
        /// FATAL（致命错误）：记录系统中出现的能使用系统完全失去功能，服务停止，系统崩溃等使系统无法继续运行下去的错误。例如，数据库无法连接，系统出现死循环。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public void Fatal(string title, string message = "", Exception ex = null)
        {
            Fatal(new Sys_Log()
            {
                Title = title,
                Message = message,
            }, ex);
        }
        /// <summary>
        /// FATAL（致命错误）：记录系统中出现的能使用系统完全失去功能，服务停止，系统崩溃等使系统无法继续运行下去的错误。例如，数据库无法连接，系统出现死循环。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="info"></param>
        /// <param name="ex"></param>
        public void Fatal(string title, string message = "", string info = "", Exception ex = null)
        {
            Fatal(new Sys_Log()
            {
                Title = title,
                Message = message,
                Info = info,
            }, ex);
        }
        /// <summary>
        /// FATAL（致命错误）：记录系统中出现的能使用系统完全失去功能，服务停止，系统崩溃等使系统无法继续运行下去的错误。例如，数据库无法连接，系统出现死循环。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ex"></param>
        public void Fatal(Sys_Log entity, Exception ex = null)
        {
            entity.Id = StringHelper.GetUUID().ToString();
            entity.Create_Time = DateTime.Now;
            entity.TraceIdentifier = CatContext.HttpContext.TraceIdentifier;
            entity.Level = LogLevel.FATAL.ToString().ToLower();

            logService.Fatal(entity, ex);
        }
        #endregion



        //public PageResult GetByPage(int pn, int ps, Expression<Func<T, bool>> expression)
        //{
        //    if (logService.GetType().FullName.IndexOf("MongoDbService") > -1)
        //    {
        //        return new AppBaseService<T>(Cat.Foundation.ConfigManager.ConnectionStrings.MongoDB.DBName).GetByPage(pn, ps, expression);
        //    }
        //    else
        //    {
        //        throw new Exception("log4net不支持查询数据功能，若要使用，请配置为Mongodb作为日志数据库");
        //    }
        //}

        //public T GetSingle(Expression<Func<T, bool>> expression)
        //{
        //    if (logService.GetType().FullName.IndexOf("MongoDbService") > -1)
        //    {
        //        return new AppBaseService<T>(Cat.Foundation.ConfigManager.ConnectionStrings.MongoDB.DBName).GetSingle(expression);
        //    }
        //    else
        //    {
        //        throw new Exception("log4net不支持查询数据功能，若要使用，请配置为Mongodb作为日志数据库");
        //    }
        //}
    }
}
