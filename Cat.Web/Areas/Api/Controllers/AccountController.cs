using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.M.Public.Models.ModelBinder.ActionModels;
using Cat.M.Public.Models.ModelBinder.QueryModels;
using Cat.M.Public.Models.Table;
using Cat.M.Public.Services;
using Cat.Utility;
using Cat.Web.Areas.Api.Models.Param;
using Cat.Web.Support.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Api.Controllers
{
    public class AccountController : BaseController
    {
        public class LoginRequestModel
        {
            public string userName { get; set; }
            public string password { get; set; }
            public string type { get; set; }
        }
        public class CurUserRequestModel
        {
            public string name { get; set; }
            public string avatar { get; set; }
            public string userid { get; set; }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Anonymous)]
        public IActionResult Login([FromBody]LoginRequestModel model)
        {

            Models.Response.LoginResult resObj;
            try
            {
                var instance = AllServices.SysAccountService.Login(model.userName, model.password);

                var auth = new Cat.M.Book.Models.ModelBinder.ReturnModels.ApiAuth()
                {
                    Authority = instance.Authority,
                    User_Id = instance.User_Id,
                    Pwd_Incomplete = instance.Password.Substring(0, 5) + instance.Password.Substring(instance.Password.Length - 5, 5),
                    LoginTime = DateTime.Now
                };
                string token = AesHelper.AesEncrypt(auth.ToJson());

                resObj = Models.Response.LoginResult.Success(model.type, instance.Authority, instance.User_Id, instance.NickName, token);
            }
            catch (Exception ex)
            {
                resObj = Models.Response.LoginResult.Fail(model.type, ex.Message);
            }

            JsonResult jsonResult = new JsonResult(resObj);

            return jsonResult;
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public CurUserRequestModel GetCurrentUser()
        {
            var user = AllServices.SysAccountService.GetSingle(w => w.User_Id == CurUserId);
            return new CurUserRequestModel()
            {
                name = user.NickName,
                avatar = user.Avatar ?? string.Empty,
                userid = user.Id
            };
        }

        /// <summary>
        /// 分页查询用户数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        public ActionRes GetListByPage(QueryPager_Sys_Account queryModel)
        {
            var pageResult = AllServices.SysAccountService.GetByPage(queryModel);

            foreach (var item in pageResult.List as List<Sys_Account>)
            {
                item.Password_Salt = "";
            }

            return ActionRes.Success(pageResult);
        }

        /// <summary>
        /// 检查新增账户时的用户id是否已存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user_Id"></param>
        /// <returns></returns>
        public ActionRes CheckUserIdRepeat(string id, string user_Id)
        {
            if (string.IsNullOrEmpty(user_Id)) throw new Exception("[用户id]不能为空");

            var instance = AllServices.SysAccountService.GetSingle(w => w.User_Id == user_Id);
            if (instance != null && instance.Id != id) throw new Exception("已存在的用户id，不能重复添加");

            return ActionRes.Success();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionRes Add([FromBody]Add_Sys_Account_Input model)
        {
            //检查权限分组（防止别人乱来）
            var authorities = Cat.M.Public.Services.Constants.Authority.AllAuthority;
            if (model.Authority.Count > 0)
            {
                foreach (var item in model.Authority)
                {
                    if (!authorities.Contains(item))
                    {
                        throw new Exception($"不合法的权限分组[{item}]");
                    }
                }
            }
            //
            var res = AllServices.SysAccountService.Add(model);
            return ActionRes.Success(res);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="addModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionRes Update([FromBody]Update_Sys_Account_Input model)
        {
            //检查权限分组（防止别人乱来）
            var authorities = Cat.M.Public.Services.Constants.Authority.AllAuthority;
            if (model.Authority.Count > 0)
            {
                foreach (var item in model.Authority)
                {
                    if (!authorities.Contains(item))
                    {
                        throw new Exception($"不合法的权限分组[{item}]");
                    }
                }
            }
            //
            var instance = AllServices.SysAccountService.GetByKey(model.Id);
            if (instance == null) throw new Exception($"没有找到指定的用户[{model.Id}]");
            CheckPression(instance);

            var res = AllServices.SysAccountService.Update(model);
            return ActionRes.Success(res);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionRes Delete([FromBody]Ids_Input model)
        {
            if (model.ids.Count == 0) throw new Exception("参数错误，ids is null");
            foreach (var id in model.ids)
            {
                var instance = AllServices.SysAccountService.GetByKey(id);
                if (instance == null) throw new Exception($"没有找到指定的用户[{id}]");
                if (instance.User_Id == base.CurUserId) throw new Exception("你傻啦，怎么可以自己删自己的账号");
                CheckPression(instance);
            }

            var res = AllServices.SysAccountService.Delete(model.ids);
            return ActionRes.Success($"已成功删除{res}条数据");
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionRes ResetPwd([FromBody]ResetPwd_Input model)
        {
            if (model.id.IsNullOrEmpty() || model.password.IsNullOrEmpty()) { throw new Exception("不能为空"); }

            var instance = AllServices.SysAccountService.GetByKey(model.id);
            if (instance == null) throw new Exception("没有找到指定的用户");
            CheckPression(instance);

            var res = AllServices.SysAccountService.ResetPwd(model.id, model.password);
            return ActionRes.Success(res);
        }



        /// <summary>
        /// 检查当前登录用户是否有权限操作
        /// </summary>
        /// <param name="instance"></param>
        private void CheckPression(Sys_Account instance)
        {
            if (base.CurUserId != "administrator")
            {
                if (instance.User_Id == "administrator" || instance.User_Id == "test") throw new Exception($"{instance.User_Id}是保留账户，你没有权限对其操作");
            }
        }

    }
}