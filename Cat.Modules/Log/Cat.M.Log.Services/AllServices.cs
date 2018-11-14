using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Services
{
    public class AllServices
    {
        /// <summary>
        /// 通用日志 服务类
        /// </summary>
        public static LogService LogService => new LogService();
        /// <summary>
        /// 操作日志 服务类
        /// </summary>
        public static SysActionLogService SysActionLogService => new SysActionLogService();
        /// <summary>
        /// 异常日志 服务类
        /// </summary>
        public static SysExceptionLogService SysExceptionLogService => new SysExceptionLogService();
        /// <summary>
        /// http请求日志 服务类
        /// </summary>
        public static SysHttpRequestLogService SysHttpRequestLogService => new SysHttpRequestLogService();
        /// <summary>
        /// 接口访问记录 服务类
        /// </summary>
        public static SysInterfaceRecordService SysInterfaceRecordService => new SysInterfaceRecordService();
    }
}
