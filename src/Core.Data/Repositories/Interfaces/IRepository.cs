using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Core.Data.SqlFunctions;
using Core.Data.SqlFunctions.Interfaces;
using Core.Data.SqlStoredProcedures;
using Core.Data.SqlStoredProcedures.Interfaces;
using NHibernate;

namespace Core.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> : IRepositoryBase, IReadOnlyRepository<TEntity> where TEntity : class
    {
        void SetBatchSize(int batchSize);
        TEntity SaveNoFlush(TEntity entity);        
        TEntity Add(TEntity entity);        
        bool Add(IEnumerable<TEntity> items);
        bool Update(TEntity entity);
        bool Update(IEnumerable<TEntity> items);
        bool Delete(TEntity entity);
        bool Delete(IEnumerable<TEntity> entities);
        bool DeleteBy(object id);
        TEntity Merge(TEntity entity);
        TEntity Save(TEntity entity);

        IList<TSubEntity> ExecuteStoredProcedure<TSubEntity>(ISqlStoredProcedure<TSubEntity> proc, SqlStoredProcedureDetails details);
        IList<TSubEntity> ExecuteStoredProcedure<TSubEntity>(ISqlStoredProcedure<TSubEntity> proc, string procName, params object[] parameters);

        TSubEntity ExecuteScalarFunction<TSubEntity>(ISqlFunction<TSubEntity> func, SqlFunctionDetails details);

        IList<TSubEntity> ExecuteFunction<TSubEntity>(ISqlFunction<TSubEntity> func, SqlFunctionDetails details);
        IList<TSubEntity> ExecuteFunction<TSubEntity>(ISqlFunction<TSubEntity> func, string funcName, params object[] parameters);

        TSubEntity ExecuteUniqueStoredProcedure<TSubEntity>(ISqlStoredProcedure<TSubEntity> proc, SqlStoredProcedureDetails details);
        void ExecuteStoredProcedure(SqlStoredProcedureDetails details);
        TScalar ExecuteScalarStoredProcedure<TScalar>(SqlStoredProcedureDetails details);

        int ExecuteSql(string query, params object[] parameters);
        T2 ExecuteSqlQuery<T2>(string query, params object[] parameters);

        bool Refresh(TEntity entity);

      
        void DeleteBy(Expression<Func<TEntity, bool>> expression);

        IList<T2> ExecuteSqlQueryMulti<T2>(string query, params object[] parameters);
    }

    public interface IRepositoryBase
    {
        ISession GetSession();
    }
}
