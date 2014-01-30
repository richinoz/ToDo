using System;
using System.Collections.Generic;
using NHibernate;

namespace Core.Data.SqlStoredProcedures.Interfaces 
{
    public interface ISqlStoredProcedure<T> : 
                     ISqlStoredProcedureScalar
    {
        IList<T> Execute(Func<string, ISQLQuery> action, SqlStoredProcedureDetails details);
        T ExecuteUnique(Func<string, ISQLQuery> action, SqlStoredProcedureDetails details);
    }
}
