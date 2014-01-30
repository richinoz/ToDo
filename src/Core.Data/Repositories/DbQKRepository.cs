using Core.Data.Repositories;
using NHibernate;

namespace Core.Data.Repositories
{
    public class DbQKRepository<T> : Repository<T> where T : class, new()
    {
        public DbQKRepository(ISession dbSession) : base(dbSession) { }

    }
}