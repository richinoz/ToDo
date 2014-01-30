using Core.Data.Repositories;
using NHibernate;

namespace Core.Data.Repositories
{
    public class DbStatelessRepository<T> : 
                 StatelessRepository<T> where T : class, new()
    {
        public DbStatelessRepository(IStatelessSession dbSession) 
            : base(dbSession) 
        {
        }
    }
}