using System;
using NHibernate;

namespace Core.Data.SqlFunctions.Interfaces
{
    public interface ISqlFunctionScalar
    {        
        TScalar ExecuteScalar<TScalar>(Func<string, ISQLQuery> action, SqlFunctionDetails details);
    }
}