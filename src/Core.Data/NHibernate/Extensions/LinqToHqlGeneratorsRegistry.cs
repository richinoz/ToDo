using Core.Data.NHibernate.Extensions.Methods;
using NHibernate.Linq.Functions;

namespace Core.Data.NHibernate.Extensions
{
    public class LinqToHqlGeneratorsRegistry : DefaultLinqToHqlGeneratorsRegistry
    {
        public LinqToHqlGeneratorsRegistry() 
        {
            this.Merge(new ConcatHqlGenerator());
            this.Merge(new CastHqlGenerator());
            this.Merge(new IsLikeGenerator());
        }
    }
}
