using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;

namespace Core.Data.Repositories.Interfaces {
    public interface IReadOnlyRepository<TEntity> where TEntity : class {

        IQueryable<TEntity> All();

        TEntity FindBy(Expression<Func<TEntity, bool>> expression);
        TEntity FindBy(int id);

        IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression);
        //IList<TEntity> FilterByEagerFetch(Expression<Func<TEntity, bool>> expression, IList<SortField<TEntity>> orderBy, IList<Expression<Func<TEntity, object>>> eagerLoadAssociations, JoinType joinType = JoinType.Left);

        //IList<TEntity> FilterByPage(Expression<Func<TEntity, bool>> expression, IList<SortField<TEntity>> orderBy, int pageIndex, int pageSize, out int totalResults);
        //IList<TEntity> FilterByPageEagerFetch(Expression<Func<TEntity, bool>> expression, IList<SortField<TEntity>> orderBy, IList<Expression<Func<TEntity, object>>> eagerLoadAssociations, int pageIndex, int pageSize, out int totalResults, JoinType joinType = JoinType.Left);

        IQueryOver<TEntity, TEntity> QueryOver();

        TEntity Load<TKey>(TKey id);
    }
}
