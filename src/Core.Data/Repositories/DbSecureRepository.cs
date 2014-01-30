using Core.Data.Repositories;
using NHibernate;

namespace Core.Data.Repositories
{
    public class DbSecureRepository<T> : Repository<T> where T : class, new()
    {
        public DbSecureRepository(ISession dbSession) : base(dbSession)
        {
        }
    }
}