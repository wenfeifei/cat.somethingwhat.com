using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cat.Utility.Helper.Lambda
{
    /// <summary>
    /// 高级搜索条件
    /// </summary>
    public enum Op_Condition
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("=")]
        Equal,
        /// <summary>
        /// 不等于
        /// </summary>
        [Description("!=")]
        NotEqual,
        /// <summary>
        /// 大于
        /// </summary>
        [Description(">")]
        GreaterThan,
        /// <summary>
        /// 小于
        /// </summary>
        [Description("<")]
        LessThan,
        /// <summary>
        /// 大于或等于
        /// </summary>
        [Description(">=")]
        GreaterThanOrEqual,
        /// <summary>
        /// 小于或等于
        /// </summary>
        [Description("<=")]
        LessThanOrEqual,
        /// <summary>
        /// 包含
        /// </summary>
        [Description("包含")]
        Contains,
        /// <summary>
        /// 开头包含
        /// </summary>
        [Description("开头包含")]
        StartsWith,
        /// <summary>
        /// 结尾包含
        /// </summary>
        [Description("结尾包含")]
        EndsWith
    }
}
