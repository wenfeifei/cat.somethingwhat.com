using Cat.Utility.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Models.ModelBinder.QueryModels
{
    public class QueryPager_Book_User_Consume : QueryPager
    {
        /// <summary>
        /// Openid
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public string Openid { get; set; }
    }
}
