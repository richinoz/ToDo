using System;
using NHibernate;

namespace Core.Data.SqlStoredProcedures.Interfaces 
{
    public interface ISqlStoredProcedureScalar 
    {
        TScalar ExecuteScalar<TScalar>(Func<string, ISQLQuery> action, SqlStoredProcedureDetails details);
    }
}
