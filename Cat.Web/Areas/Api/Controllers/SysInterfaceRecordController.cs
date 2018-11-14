using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Book.Models.ModelBinder.QueryModels;
using Cat.M.Book.Services;
using Cat.M.Log.Models.ModelBinder.QueryModels;
using Cat.M.Log.Models.Table;
using Microsoft.AspNetCore.Mvc;

using AllServices = Cat.M.Log.Services.AllServices;

namespace Cat.Web.Areas.Api.Controllers
{
    /// <summary>
    /// 接口访问记录
    /// </summary>
    public class SysInterfaceRecordController : BaseController
    {
        /// <summary>
        /// 获取接口访问记录
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public ActionRes GetListByPage(QueryPager_Sys_Interface_Record requestParams)
        {
            var pageResult = AllServices.SysInterfaceRecordService.GetByPage(requestParams);

            return ActionRes.Success(pageResult);
        }
    }
}