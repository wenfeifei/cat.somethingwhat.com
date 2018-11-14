using Cat.Utility.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Models.ModelBinder.QueryModels
{
    public class QM_Sys_Exception_Log : QueryModelBase
    {
        /// <summary>
        /// ExceptionType
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public string ExceptionType { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        [QueryAttribute(Op_Condition.Contains, Link_Condition.And, true)]
        public string Message { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        [QueryAttribute(Op_Condition.Contains, Link_Condition.And, true)]
        public string Remark { get; set; }
    }
}
