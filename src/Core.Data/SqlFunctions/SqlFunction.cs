using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Data.Helpers;
using Core.Data.NHibernate;
using Core.Data.SqlFunctions.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Type;

namespace Core.Data.SqlFunctions {
    public class SqlFunction<T> : ISqlFunction<T>
    {
        public IList<T> Execute(Func<string, ISQLQuery> action, SqlFunctionDetails details)
        {
            if (details.ColumnNames == null)
                details.ColumnNames = new List<string>() { "*" };

            var columns = string.Join(", ", details.ColumnNames);
            var questionMarks = SqlBuildingHelper.SqlParameterList(details.Parameters.Count, "?", ", ");
            var query = action(string.Format("SELECT {0} FROM {1}({2})", columns, details.Name, questionMarks))
                .AddEntity(typeof(T));

            int parameterCounter = 0;

            foreach (var parameter in details.Parameters)
            {
                if (parameter is NHibernateNullDateTime)
                    query.SetParameter(parameterCounter, null, NHibernateUtil.DateTime);
                else if (parameter is NHibernateNullInteger)
                    query.SetParameter(parameterCounter, null, NHibernateUtil.Int32);
                else
                    query.SetParameter(parameterCounter, details.Parameters[parameterCounter]);


                parameterCounter++;
            }

            return query.List<T>();
        }

        public TScalar ExecuteScalar<TScalar>(Func<string, ISQLQuery> action, SqlFunctionDetails details)
        {
            var questionMarks = SqlBuildingHelper.SqlParameterList(details.Parameters.Count, "?", ", ");
            var query = action(string.Format("SELECT {0}({1})", details.Name, questionMarks));              

            int parameterCounter = 0;

            foreach (var parameter in details.Parameters)
            {
                if (parameter is NHibernateNullDateTime)
                    query.SetParameter(parameterCounter, null, NHibernateUtil.DateTime);
                else if (parameter is NHibernateNullInteger)
                    query.SetParameter(parameterCounter, null, NHibernateUtil.Int32);
                else
                    query.SetParameter(parameterCounter, details.Parameters[parameterCounter]);


                parameterCounter++;
            }

            var vals = query.List();
            return vals != null && vals.Count > 0 && vals[0] is TScalar ? (TScalar)vals[0] :
                                                                         default(TScalar);
        }
    }
}
