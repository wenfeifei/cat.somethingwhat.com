using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Book.Models.ModelBinder.ActionModels;
using Cat.M.Book.Models.ModelBinder.QueryModels;
using Cat.M.Book.Services;
using Cat.M.Public.Models.ModelBinder.ActionModels;
using Cat.M.Public.Models.ModelBinder.QueryModels;
using Cat.M.Public.Models.Table;
using Cat.Utility;
using Cat.Web.Areas.Api.Models.Param;
using Cat.Web.Support.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Api.Controllers
{
    /// <summary>
    /// 系统消息
    /// </summary>
    public class BookUserMessageController : BaseController
    {
        /// <summary>
        /// 分页查询用户数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        public ActionRes GetListByPage(QueryPager_Book_User_Message queryModel)
        {
            var pageResult = AllServices.BookUserMessageService.GetByPage(queryModel);
            return ActionRes.Success(pageResult);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Administrator)]
        public ActionRes Add([FromBody]Add_Book_User_Message_Input model)
        {
            var res = AllServices.BookUserMessageService.Add(model);
            return ActionRes.Success(res);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="addModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Administrator)]
        public ActionRes Update([FromBody]Update_Book_User_Message_Input model)
        {
            var instance = AllServices.BookUserMessageService.GetByKey(model.Id);
            if (instance == null) throw new Exception($"没有找到指定的数据[{model.Id}]");

            var res = AllServices.BookUserMessageService.Update(model);
            return ActionRes.Success(res);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Administrator)]
        public ActionRes Delete([FromBody]Ids_Input model)
        {
            if (model.ids.Count == 0) throw new Exception("参数错误，ids is null");
            foreach (var id in model.ids)
            {
                var instance = AllServices.BookUserMessageService.GetByKey(id);
                if (instance == null) throw new Exception($"没有找到指定的数据[{id}]");
            }

            var res = AllServices.BookUserMessageService.Delete(model.ids);
            return ActionRes.Success($"已成功删除{res}条数据");
        }

    }
}