// --------------------------------------------------------------------------------------------------------------------
// <copyright http://www.opensource.org file="ReflectOn.cs">
//    (c) 2022. See license.txt in binary folder.
// </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NPlant.Core
{
    public static class ReflectOn<T>
    {
        #region Public Methods and Operators

        public static MemberInfo ForMember<TMember>(Expression<Func<T, TMember>> expression)
        {
            var memberExpression = FindMember<MemberExpression>(expression);

            MemberInfo member = memberExpression.Member;

            if (member == null)
                throw new ArgumentException("Failed to find the member expression");

            return member;
        }

        public static MethodInfo ForMethod(Expression<Func<T, object>> expression)
        {
            return FindMethod(expression);
        }

        public static MethodInfo ForMethod(Expression<Action<T>> expression)
        {
            return FindMethod(expression);
        }

        public static MethodInfo ForMethod(Expression<Func<object>> expression)
        {
            return FindMethod(expression);
        }

        public static MethodInfo ForMethod(Expression<Action> expression)
        {
            return FindMethod(expression);
        }

        #endregion

        #region Methods

        private static TMember FindMember<TMember>(LambdaExpression expression)
            where TMember : Expression
        {
            Expression body = expression.Body;
            var unaryExpression = body as UnaryExpression;
            return (unaryExpression == null ? body : unaryExpression.Operand) as TMember;
        }

        private static MethodInfo FindMethod(LambdaExpression expression)
        {
            var callExpression = FindMember<MethodCallExpression>(expression);
            if (callExpression == null)
                throw new ArgumentException("Invalid method call expression", "expression");

            return callExpression.Method;
        }

        #endregion
    }
}