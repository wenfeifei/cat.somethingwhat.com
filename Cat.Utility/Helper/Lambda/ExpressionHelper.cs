using Cat.Utility.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Cat.Utility
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// 获取(构造)查询表达式，默认忽略null值
        /// </summary>
        /// <typeparam name="TEntity">ExpQueryModel类型</typeparam>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="queryModel">ExpQueryModel实例</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GetExpressionByQueryModel<T>(object queryModel)
        //where T : Models.BaseEntity
        {
            var properties = queryModel.GetType().GetProperties();
            var entity_properties = typeof(T).GetProperties();
            List<QueryModel> conditions = new List<QueryModel>();
            foreach (var p in properties)
            {
                if (entity_properties.Count(w => w.Name.ToLower() == p.Name.ToLower()) == 0) continue; //
                //获取特性类的属性信息
                var obj = p.GetCustomAttribute(typeof(QueryAttribute), true);
                var _Op_Condition = obj.GetType().GetProperty("Op_Condition").GetValue(obj);
                var _Link_Condition = obj.GetType().GetProperty("Link_Condition").GetValue(obj);
                var _IgnoreNullable = obj.GetType().GetProperty("IgnoreNullable").GetValue(obj);
                //
                conditions.Add(new QueryModel() { PropertyName = p.Name, Value = p.GetValue(queryModel), Op = (Op_Condition)_Op_Condition, Link = (Link_Condition)_Link_Condition, IgnoreNullable = (bool)_IgnoreNullable });
            }

            var exp = LambdaHelper.GetExpression<T>(conditions);
            return exp;
        }
    }
}
