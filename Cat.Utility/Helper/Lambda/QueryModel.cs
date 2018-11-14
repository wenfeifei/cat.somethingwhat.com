using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Utility.Helper.Lambda
{
    /// <summary>
    /// 高级查询
    /// </summary>
    public class QueryModel
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public Op_Condition Op { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 这组条件与其它条件的关系
        /// </summary>
        public Link_Condition Link { get; set; }

        /// <summary>
        /// 是否忽略空值，true：将不会构建查询表达式
        /// </summary>
        public bool IgnoreNullable { get; set; }
    }
}
