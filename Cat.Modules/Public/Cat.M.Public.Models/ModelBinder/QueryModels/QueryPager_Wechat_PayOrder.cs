using Cat.M.Public.Models.ModelBinder.QueryModels;
using Cat.Utility.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.QueryModels
{
    public class QueryPager_Wechat_PayOrder : QueryPager
    {
        /// <summary>
        /// Openid
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public string Openid { get; set; }
    }
}
