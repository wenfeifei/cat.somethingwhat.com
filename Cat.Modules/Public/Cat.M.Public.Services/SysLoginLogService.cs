using Cat.M.Public.Models.ModelBinder.QueryModels;
using Cat.M.Public.Models.Table;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace Cat.M.Public.Services
{
    /// <summary>
    /// 登录日志 服务类
    /// </summary>
    public class SysLoginLogService : AppBaseService<Sys_Login_Log>
    {

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="User_Id">登录名</param>
        /// <param name="IsSuccessed">是否登录成功</param>
        /// <returns></returns>
        public int Add(string User_Id, bool IsSuccessed)
        {
            Sys_Login_Log entity = new Sys_Login_Log();
            var ipDetail = Cat.Utility.IPHelper.GetIpDetail();

            if (ipDetail == null) throw new Exception("ipDetail is null");

            entity.User_Id = User_Id;
            entity.Client_IP = Cat.Utility.IPHelper.GetClientIP();
            entity.Country = ipDetail.country;
            entity.Province = ipDetail.province;
            entity.City = ipDetail.city;
            entity.District = ipDetail.district;
            entity.TraceIdentifier = Cat.Foundation.CatContext.HttpContext.TraceIdentifier;
            entity.Create_Time = DateTime.Now;
            entity.IsSuccessed = IsSuccessed;

            return base.Add(entity);
        }

        /// <summary>
        /// 获取指定用户今天内的登录失败次数
        /// </summary>
        /// <param name="User_Id"></param>
        /// <returns></returns>
        public int GetLoginFailTimesByToday(string User_Id)
        {
            var dateTimeStart = DateTime.Today;
            var times = base.GetCount(w => w.User_Id == User_Id && w.Create_Time >= dateTimeStart);
            return times;
        }

    }
}
