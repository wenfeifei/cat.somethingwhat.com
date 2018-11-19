using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.M.Book.Models.ModelBinder.ReturnModels;
using Cat.M.Public.Services.Constants;
using Cat.M.Public.Services.Helper;
using Cat.Utility;
using Cat.Web.Support.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Api.Controllers
{
    [Area("Api")]
    [ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Guest)]
    [CatErrorFilterAttribute]
    public class BaseController : Controller
    {
        /// <summary>
        /// token
        /// </summary>
        public string token => ApiHelper.AuthToken;

        public ApiAuth ApiAuth
        {
            get
            {
                var _token = AesHelper.AesDecrypt(token);

                ApiAuth auth;
                if (string.IsNullOrEmpty(_token))
                {
                    //访客登录
                    auth = new ApiAuth()
                    {
                        Authority = string.Empty,
                        LoginTime = DateTime.Now
                    };
                }
                else
                {
                    auth = Serializer.JsonDeserialize<ApiAuth>(_token);
                }
                return auth;
            }
        }

        /// <summary>
        /// 当前后台登录用户的 User_Id
        /// </summary>
        public string CurUserId => ApiAuth.User_Id;

        /// <summary>
        /// 当前登录用户是否为管理员
        /// </summary>
        public bool IsAdministrator => ApiAuth.Authority.Split(",", StringSplitOptions.RemoveEmptyEntries).Contains(AuthorityIdentityEnum.Administrator.ToString().ToLower());
    }
}