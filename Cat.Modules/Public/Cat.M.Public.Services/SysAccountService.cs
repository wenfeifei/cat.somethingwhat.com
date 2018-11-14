using Cat.M.Public.Models.ModelBinder.ActionModels;
using Cat.M.Public.Models.ModelBinder.QueryModels;
using Cat.M.Public.Models.Table;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using AllCacheServices = Cat.M.Cache.Services.AllServices;

namespace Cat.M.Public.Services
{
    /// <summary>
    /// 后台账户 服务类
    /// </summary>
    public class SysAccountService : AppBaseService<Sys_Account>
    {
        private const string CACHE_KEY = "cat.m.public.services.sysaccountservice";

        public new Sys_Account GetByKey(string id)
        {
            //var instance = base.GetByKey(id);
            var instance = GetAllByCache().FirstOrDefault(w => w.Id == id);
            return instance;
        }
        public new Sys_Account GetSingle(Expression<Func<Sys_Account, bool>> exp)
        {
            //var instance = base.GetSingle(exp);
            var instance = GetAllByCache().AsQueryable().FirstOrDefault(exp);
            return instance;
        }

        public new List<Sys_Account> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// 从缓存中获取所有数据
        /// 将要缓存的数据换位json再缓存起来，这个实属无奈之举，因为使用MS自带的缓存类缓存数据，当数据被修改后，缓存的数据会跟着改变。这个问题暂时未有头绪去解决
        /// </summary>
        /// <returns></returns>
        public List<Sys_Account> GetAllByCache()
        {
            string listJson;
            if (AllCacheServices.CacheService.Exists(CACHE_KEY))
            {
                listJson = AllCacheServices.CacheService.GetCache(CACHE_KEY).ToString();
            }
            else
            {
                listJson = Cat.M.Public.Services.AllServices.SysAccountService.GetAll().ToJson();
                AllCacheServices.CacheService.SetCache(CACHE_KEY, listJson);
            }

            var list = Serializer.JsonDeserialize<List<Sys_Account>>(listJson);
            return list;
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        public void RemoveCache()
        {
            AllCacheServices.CacheService.Remove(CACHE_KEY);
        }

        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="User_Id"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Sys_Account Login(string User_Id, string Password)
        {
            //step1: 检查输入
            if (string.IsNullOrEmpty(User_Id) || string.IsNullOrEmpty(Password))
                throw new Exception("不能为空");

            var userInstance = GetSingle(w => w.User_Id == User_Id);

            //step2: 检查是否存在用户
            if (userInstance == null)
                throw new Exception("找不到用户");

            //step3: 检查用户登录失败次数是否超过设定的上限
            if (AllServices.SysLoginLogService.GetLoginFailTimesByToday(userInstance.User_Id) >= Cat.Foundation.ConfigManager.CatSettings.AllowLoginFailTimes)
                throw new Exception("登录失败次数过多,今天内不能登录");

            //step4: 检查密码是否正确
            if (userInstance.Password != GetEncryptPwd(Password, userInstance.Password_Salt))
            {
                AllServices.SysLoginLogService.Add(userInstance.User_Id, false);
                throw new Exception("密码错误");
            }

            //step5: 检查用户状态
            if (userInstance.Disable == true)
                throw new Exception("账户被禁用，无法使用");

            AllServices.SysLoginLogService.Add(userInstance.User_Id, true);
            return userInstance;
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Sys_Account ModifyPwd(string id, string pwd)
        {
            var instance = GetByKey(id);
            if (instance == null)
                throw new Exception("指定的用户未能找到，可能已被删除");
            if (pwd.IsNullOrEmpty())
            {
                throw new Exception("不能为空");
            }
            if (pwd.Length < 6)
            {
                throw new Exception("密码不能少于6位");
            }

            instance.Update_Time = DateTime.Now;

            instance.Password = GetEncryptPwd(pwd, instance.Password_Salt);

            base.Update(instance);

            RemoveCache();

            return instance;
        }
        #endregion

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="queryPager_Book_User"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(QueryPager_Sys_Account queryPager_Book_User)
        {
            //查询表达式
            var exp = Cat.M.Public.Services.Helper.ExpressionHelper.GetExpressionByQueryPager<QueryPager_Sys_Account, Sys_Account>(queryPager_Book_User);
            //排序
            IList<OrderBy> listOrderBy = new List<OrderBy>();
            if (!string.IsNullOrEmpty(queryPager_Book_User.sorter))
                listOrderBy.Add(new OrderBy() { Order = queryPager_Book_User.order, Sort = queryPager_Book_User.sort });

            var list = GetAllByCache().AsQueryable().Where(exp).ToList();

            return base.GetByPage(queryPager_Book_User.pn, queryPager_Book_User.ps, list, listOrderBy);
        }

        /// <summary>
        /// 获取加密后的密码
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string GetEncryptPwd(string pwd, string salt)
        {
            return MD5Helper.MD5(MD5Helper.MD5(pwd) + salt + "cat.book.1990");
        }

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Sys_Account Add(Add_Sys_Account_Input inputModel)
        {
            if (inputModel.Authority.Count == 0 || inputModel.User_Id.IsNullOrEmpty() || inputModel.NickName.IsNullOrEmpty() || inputModel.Password.IsNullOrEmpty()) { throw new Exception("不能为空"); }

            //检查User_Id重复
            //if (base.GetCount(w => w.User_Id == inputModel.User_Id) > 0) throw new Exception("用户id已存在，请填写其他");
            if (GetAllByCache().Count(w => w.User_Id == inputModel.User_Id) > 0) throw new Exception("用户id已存在，请填写其他");

            Sys_Account model = new Sys_Account();
            //model.Id = StringHelper.GuidTo16String();
            model.Create_Time = DateTime.Now;

            model.Authority = string.Join(',', inputModel.Authority);
            model.User_Id = inputModel.User_Id;
            model.NickName = inputModel.NickName;
            model.Password_Salt = Guid.NewGuid().ToString();
            model.Password = GetEncryptPwd(inputModel.Password, model.Password_Salt);
            model.Avatar = inputModel.Avatar;
            model.Disable = inputModel.Disable ?? false;

            base.Add(model);

            RemoveCache();

            return model;
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Sys_Account Update(Update_Sys_Account_Input model)
        {
            if (model.Id.IsNullOrEmpty() || model.Authority.Count == 0 || model.NickName.IsNullOrEmpty()) { throw new Exception("不能为空"); }

            Sys_Account instance = GetByKey(model.Id);
            instance.Update_Time = DateTime.Now;

            instance.Authority = string.Join(',', model.Authority);
            instance.NickName = model.NickName;
            //instance.Password = GetEncryptPwd(model.Password, instance.Password_Salt);
            instance.Avatar = model.Avatar;
            instance.Disable = model.Disable;

            base.Update(instance);

            RemoveCache();

            return instance;
        }
        #endregion

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int Delete(List<string> ids)
        {
            if (ids.Count == 0) throw new Exception("请选择要删除的数据");
            var res = base.Delete(ids);

            RemoveCache();

            return res;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        public Sys_Account ResetPwd(string id, string password)
        {
            var instance = GetByKey(id);
            if (instance != null)
            {
                instance.Password_Salt = Guid.NewGuid().ToString();
                instance.Password = GetEncryptPwd(password, instance.Password_Salt);
                instance.Update_Time = DateTime.Now;
                base.Update(instance);

                RemoveCache();
            }
            return instance;
        }

    }
}
