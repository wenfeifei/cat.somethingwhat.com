using Cat.Utility.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Models.ModelBinder.QueryModels
{
    public class QueryPager_Sys_Interface_Record : QueryPager
    {
        /// <summary>
        /// Path
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public string Path { get; set; }
        /// <summary>
        /// Referer
        /// </summary>
        [QueryAttribute(Op_Condition.Contains, Link_Condition.And, true)]
        public string Referer { get; set; }
        /// <summary>
        /// RecordType
        /// </summary>
        [QueryAttribute(Op_Condition.Contains, Link_Condition.And, true)]
        public string User_Agent { get; set; }
        /// <summary>
        /// RecordType
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public string RecordType { get; set; }
    }
}
