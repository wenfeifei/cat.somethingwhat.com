using Cat.M.Public.Models.ModelBinder.QueryModels;
using Cat.Utility.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.QueryModels
{
    public class QueryPager_Sys_Account : QueryPager
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public string User_Id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [QueryAttribute(Op_Condition.Contains, Link_Condition.And, true)]
        public string NickName { get; set; }
        /// <summary>
        /// 账户是否被禁用
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public bool? Disable { get; set; }
    }
}
