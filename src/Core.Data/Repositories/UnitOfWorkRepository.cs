using System;
using Core.Data.Repositories.Interfaces;
using Core.Data.Repositories;
using NHibernate;

namespace Core.Data.Repositories
{
    public class UnitOfWorkRepository<T> : Repository<T>, IUnitOfWorkRepository<T>, IDisposable where T : class, new()
    {
        private ISession _session;

        public UnitOfWorkRepository(ISession dbSession)
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
