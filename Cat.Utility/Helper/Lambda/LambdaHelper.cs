using Cat.Utility.Helper.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cat.Utility.Helper.Lambda
{
    public class LambdaHelper
    {
        /// <summary>
        /// 恒为真
        /// </summary>
        /// <returns>表达式</returns>
        public static Expression EqualTrue()
        {
            var type = typeof(string);
            var pRef = Expression.Constant(true);
            var constantReference = Expression.Constant(true);
            var be = Expression.Equal(pRef, constantReference);
            return be;
        }

        #region 基础表达式构建方法

        /// <summary>
        /// 等于
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression Equal<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                //var type = typeof(TSource);
                //object val = null;
                //Type dyType = TypeConvert.GetValue(type, property, value, out val);
                //if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                //var constantReference = Expression.Constant(val, dyType);
                var constantReference = Expression.Constant(value, pe.Type.GetProperty(property).PropertyType);
                BinaryExpression be = Expression.Equal(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 不等于
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression NotEqual<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                //var type = typeof(TSource);
                //object val = null;
                //Type dyType = TypeConvert.GetValue(type, property, value, out val);
                //if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                //var constantReference = Expression.Constant(val, dyType);
                var constantReference = Expression.Constant(value, pe.Type.GetProperty(property).PropertyType);
                BinaryExpression be = Expression.NotEqual(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression GreaterThan<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                //var type = typeof(TSource);
                //object val = null;
                //Type dyType = TypeConvert.GetValue(type, property, value, out val);
                //if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                //var constantReference = Expression.Constant(val, dyType);
                var constantReference = Expression.Constant(value, pe.Type.GetProperty(property).PropertyType);
                BinaryExpression be = Expression.GreaterThan(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression LessThan<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                //var type = typeof(TSource);
                //object val = null;
                //Type dyType = TypeConvert.GetValue(type, property, value, out val);
                //if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                //var constantReference = Expression.Constant(val, dyType);
                var constantReference = Expression.Constant(value, pe.Type.GetProperty(property).PropertyType);
                BinaryExpression be = Expression.LessThan(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression GreaterThanOrEqual<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                //var type = typeof(TSource);
                //object val = null;
                //Type dyType = TypeConvert.GetValue(type, property, value, out val);
                //if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                //var constantReference = Expression.Constant(val, dyType);
                var constantReference = Expression.Constant(value, pe.Type.GetProperty(property).PropertyType);
                BinaryExpression be = Expression.GreaterThanOrEqual(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression LessThanOrEqual<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                //var type = typeof(TSource);
                //object val = null;
                //Type dyType = TypeConvert.GetValue(type, property, value, out val);
                //if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                //var constantReference = Expression.Constant(val, dyType);
                var constantReference = Expression.Constant(value, pe.Type.GetProperty(property).PropertyType);
                BinaryExpression be = Expression.LessThanOrEqual(propertyReference, constantReference);
                return be;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 包含
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression Contains<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                //var type = typeof(TSource);
                //object val = null;
                //Type dyType = TypeConvert.GetValue(type, property, value, out val);
                //if (val == null) return null;
                var propertyReference = Expression.Property(pe, property);
                //var constantReference = Expression.Constant(val, dyType);
                var constantReference = Expression.Constant(value, pe.Type.GetProperty(property).PropertyType);
                var be = Expression.Call(propertyReference, typeof(string).GetMethod("Contains"), constantReference);
                return be;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 开头包含
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression StartsWith<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                var startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
                MemberExpression handledMember = Expression.Property(pe, property);
                ConstantExpression constant = Expression.Constant(value);
                return Expression.Call(handledMember, startsWithMethod, constant);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 结尾包含
        /// </summary>
        /// <typeparam name="TSource">数据类型</typeparam>
        /// <param name="pe">左侧表达式参数</param>
        /// <param name="property">属性</param>
        /// <param name="value">值</param>
        /// <returns>表达式</returns>
        public static Expression EndsWith<TSource>(ParameterExpression pe, string property, object value)
        {
            try
            {
                var startsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
                MemberExpression handledMember = Expression.Property(pe, property);
                ConstantExpression constant = Expression.Constant(value);
                return Expression.Call(handledMember, startsWithMethod, constant);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion


        /// <summary>
        /// 将高级查询转换成对应的表达式树
        /// </summary>
        /// <typeparam name="TSource">类型</typeparam>
        /// <param name="conditions">高级查询条件集合</param>
        /// <returns>对象数据类型的表达式树</returns>
        public static Expression<Func<TSource, bool>> GetExpression<TSource>(List<QueryModel> conditions)
        {
            var type = typeof(TSource);
            var pe = Expression.Parameter(type, "p");

            Expression exp = null;
            if (conditions == null)
            {
                exp = EqualTrue();
                return Expression.Lambda<Func<TSource, bool>>(exp, pe);
            }
            //并：生成交集的条件
            conditions.Where(p => p.Link == Link_Condition.And).ToList().ForEach(p =>
            {
                if (p.IgnoreNullable && p.Value == null)
                    return;
                Expression temp = GetExpressionTemp<TSource>(p, pe);
                if (exp == null)
                {
                    if (temp != null)
                    {
                        exp = temp;
                    }
                }
                else
                {
                    if (temp != null)
                    {
                        exp = Expression.AndAlso(exp, temp);
                    }
                }
            });

            //或：生成并集的条件
            conditions.Where(p => p.Link == Link_Condition.Or).ToList().ForEach(p =>
            {
                if (p.IgnoreNullable && p.Value == null)
                    return;
                Expression temp = GetExpressionTemp<TSource>(p, pe);
                if (exp == null)
                {
                    if (temp != null)
                    {
                        exp = temp;
                    }
                }
                else
                {
                    if (temp != null)
                    {
                        exp = Expression.Or(exp, temp);
                    }
                }
            });
            if (exp == null)
            {
                exp = EqualTrue();
            }

            return Expression.Lambda<Func<TSource, bool>>(exp, pe);
        }

        private static Expression GetExpressionTemp<TSource>(QueryModel p, ParameterExpression pe)
        {
            Expression temp = null;
            switch (p.Op)
            {
                case Op_Condition.Equal:
                    temp = Equal<TSource>(pe, p.PropertyName, p.Value);
                    break;
                case Op_Condition.NotEqual:
                    temp = NotEqual<TSource>(pe, p.PropertyName, p.Value);
                    break;
                case Op_Condition.GreaterThan:
                    temp = GreaterThan<TSource>(pe, p.PropertyName, p.Value);
                    break;
                case Op_Condition.LessThan:
                    temp = LessThan<TSource>(pe, p.PropertyName, p.Value);
                    break;
                case Op_Condition.GreaterThanOrEqual:
                    temp = GreaterThanOrEqual<TSource>(pe, p.PropertyName, p.Value);
                    break;
                case Op_Condition.LessThanOrEqual:
                    temp = LessThanOrEqual<TSource>(pe, p.PropertyName, p.Value);
                    break;
                case Op_Condition.Contains:
                    temp = Contains<TSource>(pe, p.PropertyName, p.Value);
                    break;
                case Op_Condition.StartsWith:
                    temp = StartsWith<TSource>(pe, p.PropertyName, p.Value);
                    break;
                case Op_Condition.EndsWith:
                    temp = EndsWith<TSource>(pe, p.PropertyName, p.Value);
                    break;
                default:
                    break;
            }
            return temp;
        }

    }
}
