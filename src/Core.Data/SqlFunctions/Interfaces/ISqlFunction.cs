using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate;

namespace Core.Data.SqlFunctions.Interfaces 
{
    public interface ISqlFunction<T> : ISqlFunctionScalar
    {
        IList<T> Execute(Func<string, ISQLQuery> action, SqlFunctionDetails details);
        
    }
}
