using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ReturnModels.Wechat
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}
