using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cat.Utility;
using Microsoft.AspNetCore.Mvc.Filters;

using AllLogServices = Cat.M.Log.Services.AllServices;

namespace Cat.Web.Support.Filter
{
    /// <summary>
    /// 接口访问记录 筛选器
    /// </summary>
    sealed class AccessInterfaceRecordAttribute : ActionFilterAttribute
    {
        public Cat.Enums.RecordType RecordType = Enums.RecordType.default_val;

        Stopwatch Stopwatch;

        /// <summary>
        /// 在Action执行之前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
        }
        /// <summary>
        /// 在执行Result后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Stopwatch.Stop();

            long millSecond = Stopwatch.ElapsedMilliseconds;

            AllLogServices.SysInterfaceRecordService.AddLog(RecordType, millSecond, IPHelper.GetClientIP());
        }
    }
}
