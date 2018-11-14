using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Models.ModelBinder.ReturnModels
{
    public class ApiAuth
    {
        public string Authority { get; set; }
        public string User_Id { get; set; }
        /// <summary>
        /// 不完整的用户密码，取密码前五位和后五位组成的字符串
        /// </summary>
        public string Pwd_Incomplete { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
