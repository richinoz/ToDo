using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Data.Repositories {
    // Written by Simon. If you have questions.. just ask!
    // This has to be in the Data layer so that we can access the extension methods that NHibernate adds to strings.
    public static class NHibernateExpressionHelper {
        public static Expression<Func<T, bool>> CreateFunctionCall<T>(string functionName, string propertyName,
            string argument, bool invertLogic = false) {
            var parameter = Expression.Parameter(typeof(T), typeof(T).Name);
            var propertyInfo = typeof(T).GetProperty(propertyName);
            var property = Expression.Property(parameter, propertyInfo);

            MethodInfo method = null;
            MethodCallExpression methodCallExpression = null;
            method = typeof(string).GetMethod(functionName, BindingFlags.Public | BindingFlags.Static);
            if (method == null) {
                var value = Expression.Constant(argument, property.Type);
                method = typeof(global::NHibernate.Criterion.RestrictionExtensions).GetMethod(functionName,
                    new[] { typeof(string), typeof(string) });
                methodCallExpression = Expression.Call(null, method,
                    new Expression[] { property, value });
            }
            else {
                var value = Expression.Constant(argument, property.Type);
                methodCallExpression = Expression.Call(property, method, new Expression[] { parameter, value });
            }

            if (invertLogic) {
                return Expression.Lambda<Func<T, bool>>(Expression.Not(methodCallExpression), parameter);
            }

            return Expression.Lambda<Func<T, bool>>(methodCallExpression, parameter);
        }
    }
}