using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using NHibernate;
using NHibernate.Driver;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;

namespace Core.Data.NHibernate
{
    internal class SqlStatementInterceptor : EmptyInterceptor
    { 
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            Debug.WriteLine(sql);
            return sql;
        }
    }
}