using Cat.Utility.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Models.ModelBinder.QueryModels
{
    public class QueryPager_Book_Read_Record : QueryPager
    {
        /// <summary>
        /// Openid
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public string Openid { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        [QueryAttribute(Op_Condition.Contains, Link_Condition.And, true)]
        public string Author { get; set; }
        /// <summary>
        /// 书名
        /// </summary>
        [QueryAttribute(Op_Condition.Contains, Link_Condition.And, true)]
        public string Book_Name { get; set; }
        /// <summary>
        /// 是否已收藏
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public bool? AlreadyCollected { get; set; }
    }
}
