using Cat.M.Log.Models.Table;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cat.M.Log.Services
{
    public interface ILogService
    {
        /// <summary>
        /// DEBUG （调试信息）：记录系统用于调试的一切信息，内容或者是一些关键数据内容的输出。 下面演示根据每个日志等级生成对应的一个文件。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ex"></param>
        void Debug(Sys_Log entity, Exception ex = null);
        /// <summary>
        /// INFO（一般信息）：记录系统运行中应该让用户知道的基本信息。例如，服务开始运行，功能已经开户等。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ex"></param>
        void Info(Sys_Log entity, Exception ex = null);
        /// <summary>
        /// WARN（警告）：记录系统中不影响系统继续运行，但不符合系统运行正常条件，有可能引起系统错误的信息。例如，记录内容为空，数据内容不正确等。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ex"></param>
        void Warn(Sys_Log entity, Exception ex = null);
        /// <summary>
        /// ERROR（一般错误）：记录系统中出现的导致系统不稳定，部分功能出现混乱或部分功能失效一类的错误。例如，数据字段为空，数据操作不可完成，操作出现异常等。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ex"></param>
        void Error(Sys_Log entity, Exception ex = null);
        /// <summary>
        /// FATAL（致命错误）：记录系统中出现的能使用系统完全失去功能，服务停止，系统崩溃等使系统无法继续运行下去的错误。例如，数据库无法连接，系统出现死循环。
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ex"></param>
        void Fatal(Sys_Log entity, Exception ex = null);
    }
}
