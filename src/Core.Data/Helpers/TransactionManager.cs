using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Repositories.Interfaces;
using NHibernate;

namespace CampAustralia.Core.Helpers
{
    public class TransactionManager : IDisposable
    {
        private readonly IRepositoryBase _repo;

        public TransactionManager(IRepositoryBase repo)
        {
            _repo = repo; 
            _repo.GetSession().BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        public void Dispose()
        {
            try
            {
                _repo.GetSession().Transaction.Commit();
            }
            catch
            {
                _repo.GetSession().Transaction.Rollback();
            }
        }
    }
}
