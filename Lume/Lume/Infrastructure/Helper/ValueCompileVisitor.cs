using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Lume.Infrastructure.Helper
{

    public static class ValueCompileVisitor
    {
        public static Expression<Func<T, bool>> Convert<T>(Expression<Func<T, bool>> expression)
        {
            return Expression.Lambda<Func<T, bool>>(new Visitor().Modify(expression.Body), Expression.Parameter(typeof(T), "entity"));

        }

    }

    class Visitor : ExpressionVisitor
    {
        public Expression Modify(Expression expression)
        {
            return Visit(expression);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {

            var memberLeft = node.Left as MemberExpression;
            if (memberLeft != null && memberLeft.Expression is ParameterExpression)
            {
                var f = Expression.Lambda(node.Right).Compile();
                var value = f.DynamicInvoke().ToString();
                if (long.TryParse(value, out long result))
                {
                    ;
                    return base.VisitBinary(node.Update(node.Left, node.Conversion, Expression.Constant(result, node.Left.Type)));
                }
                else
                {
                    if (bool.TryParse(value, out bool result_bool))
                    {
                        return base.VisitBinary(node.Update(node.Left, node.Conversion, Expression.Constant(result_bool, typeof(bool))));
                    }
                    else
                    {
                        return base.VisitBinary(node.Update(node.Left, node.Conversion, Expression.Constant(value)));
                    }
                }
            }
            return base.VisitBinary(node);
        }
    }
}