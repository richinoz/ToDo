using System;
using Core.Data.Repositories.Interfaces;
using NHibernate;

namespace Core.Data.Repositories
{
    public class UnitOfWorkSecureRepository<T> : Repository<T>, IUnitOfWorkRepository<T>, IDisposable where T : class, new()
    {
        private ISession _session;

        public UnitOfWorkSecureRepository(ISession dbSession)
            : base(dbSession)
        {
            _session = dbSession;
        }

        public void Begin()
        {
            _session.BeginTransaction();
        }

        public void End()
        {
            if (_session.Transaction != null &&
                _session.Transaction.IsActive)
            {
                _session.Transaction.Commit();
            }
        }

        public void Dispose()
        {
            if (_session.Transaction != null &&
                _session.Transaction.IsActive)
            {
                _session.Transaction.Commit();
            }
        }
    }
}