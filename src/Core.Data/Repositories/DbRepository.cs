using Core.Data.Repositories;
using NHibernate;

namespace Core.Data.Repositories
{
    public class DbRepository<T> : Repository<T> where T : class, new()
    {
        public DbRepository(ISession dbSession) : base(dbSession) { }
    }
}