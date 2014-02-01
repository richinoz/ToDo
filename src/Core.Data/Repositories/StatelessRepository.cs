using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Data.Repositories;
using Core.Data.Repositories.Interfaces;
using Core.Data.SqlFunctions;
using Core.Data.SqlFunctions.Interfaces;
using Core.Data.SqlStoredProcedures;
using Core.Data.SqlStoredProcedures.Interfaces;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Linq;
using NHibernate.Transform;

namespace Core.Data.Repositories 
{
    public class StatelessRepository<T> : 
                 IRepository<T> where T : class, new() 
    {
        private readonly IStatelessSession _session;
        public StatelessRepository(IStatelessSession session)
        {
            _session = session;
        }


        public void SetBatchSize(int batchSize)
        {
            _session.SetBatchSize(batchSize);
        }

        public T SaveNoFlush(T entity)
        {
            _session.Insert(entity);
            return entity;
        }

        public T Add(T entity) {
            _session.Insert(entity);

            return entity;
        }

        public bool Add(IEnumerable<T> items) {
            foreach (T item in items) {
                _session.Insert(item);
            }

            return true;
        }

        public bool Update(T entity) {
            _session.Update(entity);

            return true;
        }

        public T Merge(T entity)
        {
            throw new NotImplementedException();
        }

        public T Save(T entity) {
            throw new NotImplementedException();
        }

        public bool Update(IEnumerable<T> items) {
            foreach (T item in items) {
                _session.Update(item);
            }

            return true;
        }

        public bool Delete(T entity) {
            _session.Delete(entity);

            return true;
        }

        public bool DeleteBy(object key) {
            var obj = _session.Get<T>(key);
            return Delete(obj);
        }

        public bool Delete(IEnumerable<T> entities) {
            foreach (T entity in entities) {
                _session.Delete(entity);
            }

            return true;
        }

        public T FindBy(int id) {
            return _session.Get<T>(id);
        }

        public IQueryable<T> All() {
            return _session.Query<T>();
        }

        public T FindBy(Expression<Func<T, bool>> expression) {
            return FilterBy(expression).FirstOrDefault();
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression) {
            return All().Where(expression);
        }

        //public IList<T> FilterByEagerFetch(Expression<Func<T, bool>> expression, IList<SortField<T>> orderBy,
        //    IList<Expression<Func<T, object>>> eagerLoadAssociations, JoinType joinType = JoinType.Left) {
        //    var initialQuery = _session.QueryOver<T>();

        //    var futures = new List<object>(50);

        //    if (eagerLoadAssociations != null) {
        //        foreach (var expr in eagerLoadAssociations) {
        //            if (joinType == JoinType.Inner)
        //                futures.Add(_session.QueryOver<T>().JoinQueryOver(expr).Future());
        //            else
        //                futures.Add(_session.QueryOver<T>().Left.JoinQueryOver(expr).Future());

        //        }
        //    }

        //    if (orderBy != null) {
        //        int orderByNumber = 0;
        //        foreach (var sortField in orderBy) {
        //            if (orderByNumber == 0) {
        //                initialQuery = sortField.Ascending
        //                    ? initialQuery.OrderBy(sortField.Field).Asc
        //                    : initialQuery.OrderBy(sortField.Field).Desc;
        //            }
        //            else {
        //                initialQuery = sortField.Ascending
        //                    ? initialQuery.ThenBy(sortField.Field).Asc
        //                    : initialQuery.ThenBy(sortField.Field).Desc;
        //            }
        //            orderByNumber++;
        //        }
        //    }

        //    if (expression != null)
        //        futures.Add(initialQuery.Where(expression).Future());

        //    var i = futures.Count();

        //    return initialQuery
        //                .TransformUsing(Transformers.DistinctRootEntity)
        //                .Future().ToList();
        //}

        //public IList<T> FilterByPage(Expression<Func<T, bool>> expression, IList<SortField<T>> orderBy, int pageIndex, int pageSize,
        //    out int totalResults) {
        //    var initialQuery = _session.QueryOver<T>();

        //    if (expression != null) {
        //        initialQuery = initialQuery.Where(expression);
        //    }

        //    totalResults = initialQuery.ToRowCountQuery().FutureValue<int>().Value; // need to do this before the paging happens

        //    int orderByNumber = 0;
        //    foreach (var sortField in orderBy) {
        //        if (orderByNumber == 0) {
        //            initialQuery = sortField.Ascending ? initialQuery.OrderBy(sortField.Field).Asc : initialQuery.OrderBy(sortField.Field).Desc;
        //        }
        //        else {
        //            initialQuery = sortField.Ascending ? initialQuery.ThenBy(sortField.Field).Asc : initialQuery.ThenBy(sortField.Field).Desc;
        //        }
        //        orderByNumber++;
        //    }

        //    return initialQuery
        //                .Skip((pageIndex - 1) * pageSize)
        //                .Take(pageSize)
        //                .List<T>();
        //}

        //public IList<T> FilterByPageEagerFetch(Expression<Func<T, bool>> expression, IList<SortField<T>> orderBy, IList<Expression<Func<T, object>>> eagerLoadAssociations, int pageIndex, int pageSize,
        //    out int totalResults, JoinType joinType = JoinType.Left) {
        //    var initialQuery = _session.QueryOver<T>();

        //    var futures = new List<object>(50);

        //    if (eagerLoadAssociations != null) {
        //        foreach (var expr in eagerLoadAssociations) {
        //            if (joinType == JoinType.Inner)
        //                futures.Add(_session.QueryOver<T>().JoinQueryOver(expr).Future());
        //            else
        //                futures.Add(_session.QueryOver<T>().Left.JoinQueryOver(expr).Future());

        //        }
        //    }

        //    if (orderBy != null) {
        //        int orderByNumber = 0;
        //        foreach (var sortField in orderBy) {
        //            if (orderByNumber == 0) {
        //                initialQuery = sortField.Ascending
        //                    ? initialQuery.OrderBy(sortField.Field).Asc
        //                    : initialQuery.OrderBy(sortField.Field).Desc;
        //            }
        //            else {
        //                initialQuery = sortField.Ascending
        //                    ? initialQuery.ThenBy(sortField.Field).Asc
        //                    : initialQuery.ThenBy(sortField.Field).Desc;
        //            }
        //            orderByNumber++;
        //        }
        //    }

        //    if (expression != null)
        //        futures.Add(initialQuery.Where(expression).Future());

        //    var i = futures.Count();

        //    totalResults = initialQuery.ToRowCountQuery().FutureValue<int>().Value; // need to do this before the paging & eager loading happens

        //    return initialQuery
        //                .TransformUsing(Transformers.DistinctRootEntity)
        //                .Skip((pageIndex - 1) * pageSize)
        //                .Take(pageSize)
        //                .Future().ToList();
        //}

        public IQueryOver<T, T> QueryOver() {
            return _session.QueryOver<T>();
        }

        public IList<TSubEntity> ExecuteStoredProcedure<TSubEntity>(ISqlStoredProcedure<TSubEntity> proc, SqlStoredProcedureDetails details) {
            return proc.Execute(x => _session.CreateSQLQuery(x), details);
        }

        public IList<TSubEntity> ExecuteStoredProcedure<TSubEntity>(ISqlStoredProcedure<TSubEntity> proc, string procName, params object[] parameters) {
            return ExecuteStoredProcedure(proc, new SqlStoredProcedureDetails() { Name = procName, Parameters = parameters });
        }

        public TSubEntity ExecuteScalarFunction<TSubEntity>(ISqlFunction<TSubEntity> func, SqlFunctionDetails details)
        {
            return func.ExecuteScalar<TSubEntity>(x => _session.CreateSQLQuery(x), details);
        }

        public IList<TSubEntity> ExecuteFunction<TSubEntity>(ISqlFunction<TSubEntity> proc, SqlFunctionDetails details) {
            return proc.Execute(x => _session.CreateSQLQuery(x), details);
        }

        public IList<TSubEntity> ExecuteFunction<TSubEntity>(ISqlFunction<TSubEntity> func, string procName, params object[] parameters) {
            return ExecuteFunction(func, new SqlFunctionDetails() { Name = procName, Parameters = parameters, ColumnNames = new List<string>() { "*" } });
        }

        public TSubEntity ExecuteUniqueStoredProcedure<TSubEntity>(ISqlStoredProcedure<TSubEntity> proc, SqlStoredProcedureDetails details) {
            return proc.ExecuteUnique(x => _session.CreateSQLQuery(x), details);
        }

        public void ExecuteStoredProcedure(SqlStoredProcedureDetails details) {
            var placeHolder = new SqlStoredProcedure<int>();
            placeHolder.ExecuteScalar<int>(x => _session.CreateSQLQuery(x), details);
        }

        public int ExecuteSql(string query, params object[] parameters) {
            return _session.CreateSQLQuery(string.Format(query, parameters)).ExecuteUpdate();
        }

        public T2 ExecuteSqlQuery<T2>(string query, params object[] parameters){
            return _session.CreateSQLQuery(string.Format(query, parameters)).UniqueResult<T2>();
        }

        public TScalar ExecuteScalarStoredProcedure<TScalar>(SqlStoredProcedureDetails details)
        {
            var placeHolder = new SqlStoredProcedure<TScalar>();
            return placeHolder.ExecuteScalar<TScalar>(x => _session.CreateSQLQuery(x), details);
        }

        public bool Refresh(T entity)
        {
            _session.Refresh(entity);

            return true;
        }

        public T Load<TKey>(TKey id) {
            return _session.Get<T>(id);
        }

        public void DeleteBy(Expression<Func<T, bool>> expression)
        {
            _session.Query<T>()
                .Where(expression).ForEach(p => _session.Delete(p));
        }

        public IList<T2> ExecuteSqlQueryMulti<T2>(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void FlushUnfinishedBatch()
        {
            var sessionImpl = (ISessionImplementor)_session;
            sessionImpl.Batcher.ExecuteBatch();
        }

        public ISession GetSession()
        {
            throw new NotImplementedException();
        }
    }
}
