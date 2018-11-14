using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Utility.Helper.Lambda
{
    /// <summary>
    /// 用于实体查询的特性
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class QueryAttribute : Attribute
    {
        /// <summary>
        /// 搜索条件
        /// </summary>
        public Op_Condition Op_Condition { get; }

        /// <summary>
        /// 当前条件所属类型
        /// </summary>
        public Link_Condition Link_Condition { get; }

        /// <summary>
        /// 是否忽略空值，true：将不会构建查询表达式
        /// </summary>
        public bool IgnoreNullable { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="op_Condition">搜索条件</param>
        /// <param name="link_Condition">当前条件所属类型</param>
        /// <param name="ignoreNullable">是否忽略空值，true：将不会构建查询表达式</param>
        public QueryAttribute(Op_Condition op_Condition, Link_Condition link_Condition, bool ignoreNullable)
        {
            this.Op_Condition = op_Condition;
            this.Link_Condition = link_Condition;
            this.IgnoreNullable = ignoreNullable;
        }
    }
}
