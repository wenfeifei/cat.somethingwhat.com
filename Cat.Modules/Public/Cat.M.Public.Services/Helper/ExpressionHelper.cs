using Cat.Utility.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Cat.M.Public.Services.Helper
{
    /// <summary>
    /// 动态表达式帮助类
    /// </summary>
    public class ExpressionHelper
    {
        /// <summary>
        /// 获取(构造)查询表达式，默认忽略null值（用于分页查询）
        /// </summary>
        /// <typeparam name="TEntity">QueryPager类型</typeparam>
        /// <typeparam name="BaseEntity">BaseEntity类型</typeparam>
        /// <param name="queryPager">QueryPager实例</param>
        /// <returns></returns>
        public static Expression<Func<BaseEntity, bool>> GetExpressionByQueryPager<TEntity, BaseEntity>(TEntity queryPager)
            where TEntity : Public.Models.ModelBinder.QueryModels.QueryPager
            where BaseEntity : Cat.M.Public.Models.BaseEntity
        {
            var properties = queryPager.GetType().GetProperties();
            List<QueryModel> conditions = new List<QueryModel>();
            foreach (var p in properties)
            {
                if (p.DeclaringType.Namespace == "Cat.Models.ModelBinder.QueryModels")
                {
                    //排除基类属性
                    continue;
                }
                //获取特性类的属性信息
                var obj = p.GetCustomAttribute(typeof(Cat.Utility.Helper.Lambda.QueryAttribute), true);

                if (obj == null) continue;

                var _Op_Condition = obj.GetType().GetProperty("Op_Condition").GetValue(obj);
                var _Link_Condition = obj.GetType().GetProperty("Link_Condition").GetValue(obj);
                var _IgnoreNullable = obj.GetType().GetProperty("IgnoreNullable").GetValue(obj);
                //
                conditions.Add(new QueryModel() { PropertyName = p.Name, Value = p.GetValue(queryPager), Op = (Op_Condition)_Op_Condition, Link = (Link_Condition)_Link_Condition, IgnoreNullable = (bool)_IgnoreNullable });
            }

            var exp = LambdaHelper.GetExpression<BaseEntity>(conditions);
            return exp;
        }
    }
}
