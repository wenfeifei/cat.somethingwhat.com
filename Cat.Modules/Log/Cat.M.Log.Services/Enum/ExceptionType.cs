using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Services.Enum
{
    /// <summary>
    /// 异常分类
    /// </summary>
    public enum ExceptionType
    {
        /// <summary>
        /// 一般的异常
        /// </summary>
        Exception,
        /// <summary>
        /// 业务异常
        /// </summary>
        BussinessException,
        /// <summary>
        /// 服务异常
        /// </summary>
        ServiceException,
        /// <summary>
        /// 数据访问相关异常
        /// </summary>
        DAOException,
        /// <summary>
        /// http异常
        /// </summary>
        HttpException
    }
}
