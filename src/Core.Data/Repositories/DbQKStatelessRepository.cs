using Core.Data.Repositories;
using NHibernate;

namespace Core.Data.Repositories
{
    public class DbQKStatelessRepository<T> : 
                 StatelessRepository<T> where T : class, new()
    {
        public DbQKStatelessRepository(IStatelessSession dbSession) 
            : base(dbSession) 
        {
        }
    }
}