using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cat.Web.Areas.Api.Models.Response
{
    public class LoginResult
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="type"></param>
        /// <param name="currentAuthority"></param>
        /// <param name="NickName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static LoginResult Success(string type, string currentAuthority, string User_Id, string NickName, string token)
        {
            return new LoginResult()
            {
                status = "ok",
                type = type,
                currentAuthority = currentAuthority,
                token = token,
                User_Id = User_Id,
                NickName = NickName
            };
        }
        /// <summary>
        /// 登录失败
        /// </summary>
        /// <param name="type"></param>
        /// <param name="currentAuthority"></param>
        /// <param name="NickName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static LoginResult Fail(string type, string msg)
        {
            return new LoginResult()
            {
                status = "error",
                type = type,
                msg = msg
            };
        }

        /// <summary>
        /// ok / error
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// account / mobile
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string currentAuthority { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string User_Id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// msg
        /// </summary>
        public string msg { get; set; }        
    }
}
