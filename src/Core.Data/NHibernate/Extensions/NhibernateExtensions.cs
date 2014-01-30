using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using NHibernate.Linq;

namespace Core.Data.NHibernate.Extensions
{
    public static class NhibernateExtensions
    {
        public static bool IsLike(this string source, string pattern)
        {
            pattern = Regex.Escape(pattern);
            pattern = pattern.Replace("%", ".*?").Replace("_", ".");
            pattern = pattern.Replace(@"\[", "[").Replace(@"\]", "]").Replace(@"\^", "^");

            return Regex.IsMatch(source, pattern);
        }

        public static TTarget Cast<TTarget>(this Object source)
        {
            return ((TTarget)source);
        }


        public static IQueryable<TOriginating> Include<TOriginating, TRelated>(this IQueryable<TOriginating> query, Expression<Func<TOriginating, TRelated>> path)
            where TOriginating : class
            where TRelated : class
        {
            var queryable = query as NhQueryable<TOriginating>;
            if (queryable != null)
                return (queryable.Fetch(path));
            return query.Fetch(path);
        }

        public static IQueryable<TOriginating> IncludeMany<TOriginating, TRelated>(this IQueryable<TOriginating> query, Expression<Func<TOriginating, IEnumerable<TRelated>>> path)
            where TOriginating : class
            where TRelated : class
        {
            var queryable = query as NhQueryable<TOriginating>;
            if (queryable != null)
                return (queryable.FetchMany(path));
            return query.FetchMany(path);
        }

        public static IQueryable<TQueried> ThenInclude<TQueried, TFetch, TRelated>(this IQueryable<TQueried> query, Expression<Func<TFetch, TRelated>> path)
            where TQueried : class
            where TRelated : class
        {
            var queryable = query as INhFetchRequest<TQueried, TFetch>;
            return queryable.ThenFetch(path);
        }

        public static IQueryable<TQueried> ThenIncludeMany<TQueried, TFetch, TRelated>(this IQueryable<TQueried> query, Expression<Func<TFetch, IEnumerable<TRelated>>> path)
            where TQueried : class
            where TRelated : class
        {
            var queryable = query as INhFetchRequest<TQueried, TFetch>;
            return queryable.ThenFetchMany(path);
        }

    }
}
