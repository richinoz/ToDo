using System.Collections.Generic;
using System;
using System.Linq;
using Core.Data.Helpers;
using Core.Data.SqlStoredProcedures.Interfaces;
using NHibernate;

namespace Core.Data.SqlStoredProcedures
{
    public class SqlStoredProcedure<T> : 
                 ISqlStoredProcedure<T>
    {
        public IList<T> Execute(Func<string, ISQLQuery> action, SqlStoredProcedureDetails details)
        {
            var questionMarks = SqlBuildingHelper.SqlParameterList(details.Parameters.Count, "?", ", ");
            var query = action(string.Format("{0} {1}", details.Name, questionMarks))
                .AddEntity(typeof(T));

            int parameterCounter = 0;

            foreach (var parameter in details.Parameters)
            {
                if (parameter == null)
                    query.SetParameter(parameterCounter, details.Parameters[parameterCounter], NHibernateUtil.Object);
                else
                    query.SetParameter(parameterCounter, details.Parameters[parameterCounter]);

                parameterCounter++;
            }

            return query.List<T>();
        }

        public T ExecuteUnique(Func<string, ISQLQuery> action, SqlStoredProcedureDetails details)
        {
            var questionMarks = SqlBuildingHelper.SqlParameterList(details.Parameters.Count, "?", ", ");
            var query = action(string.Format("{0} {1}", details.Name, questionMarks));

            var parameterCounter = 0;

            foreach (var parameter in details.Parameters)
            {
                query.SetParameter(parameterCounter, parameter);
                parameterCounter++;
            }

            return query.UniqueResult<T>();
        }

        public TScalar ExecuteScalar<TScalar>(Func<string, ISQLQuery> action, SqlStoredProcedureDetails details)
        {
            var questionMarks = SqlBuildingHelper.SqlParameterList(details.Parameters.Count, "?", ", ");
            var query = action(string.Format("{0} {1}", details.Name, questionMarks));

            var parameterCounter = 0;

            foreach (var parameter in details.Parameters) {
                query.SetParameter(parameterCounter, parameter);
                parameterCounter++;
            }

            var vals = query.List();
            return vals != null && vals.Count > 0 && vals[0] is TScalar ? (TScalar)vals[0] : 
                                                                          default(TScalar);
        }
    }
}
