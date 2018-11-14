using Cat.M.Log.Models.ModelBinder.QueryModels;
using Cat.Utility.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Models
{
    public class QueryModelBase
    {
        /// <summary>
        /// Id
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public string Id { get; set; }
        /// <summary>
        /// TraceIdentifier
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public string TraceIdentifier { get; set; }
    }
}
