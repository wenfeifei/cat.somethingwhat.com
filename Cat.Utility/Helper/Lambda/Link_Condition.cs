using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cat.Utility.Helper.Lambda
{
    /// <summary>
    /// 当前条件所属类型
    /// </summary>
    public enum Link_Condition
    {
        /// <summary>
        /// 并
        /// </summary>
        [Description("并")]
        And = 0,

        /// <summary>
        /// 或
        /// </summary>
        [Description("或")]
        Or = 1
    }
}
