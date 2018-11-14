using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Services.Enum
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 指出细粒度信息事件对调试应用程序是非常有帮助的，主要用于开发过程中打印一些运行信息
        /// </summary>
        DEBUG,
        /// <summary>
        /// 消息在粗粒度级别上突出强调应用程序的运行过程。打印一些你感兴趣的或者重要的信息，这个可以用于生产环境中输出程序运行的一些重要信息，但是不能滥用，避免打印过多的日志
        /// </summary>
        INFO,
        /// <summary>
        /// 表明系统出现轻微的不合理但不影响运行和使用
        /// </summary>
        WARN,
        /// <summary>
        /// 表明出现了系统错误和异常，无法正常完成目标操作
        /// </summary>
        ERROR,
        /// <summary>
        /// 指出每个严重的错误事件将会导致应用程序的退出
        /// </summary>
        FATAL
    }
}
