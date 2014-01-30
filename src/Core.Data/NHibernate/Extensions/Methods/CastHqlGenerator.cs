using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate.Hql.Ast;
using NHibernate.Linq;
using NHibernate.Linq.Functions;
using NHibernate.Linq.Visitors;

namespace Core.Data.NHibernate.Extensions.Methods
{
    public class CastHqlGenerator : BaseHqlGeneratorForMethod
    {
        public CastHqlGenerator()
        {
            SupportedMethods = new[] { ReflectionHelper.GetMethodDefinition(() => NhibernateExtensions.Cast<Object>(null)) };
        }

        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return (treeBuilder.Cast(visitor.Visit(arguments[0]).AsExpression(), method.ReturnType));
        }
    }
}
