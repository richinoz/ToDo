using System;

namespace Core.Data.Repositories.Interfaces
{
    public interface IBillingTransaction :
                     IDisposable
    {
        void Begin();
        void Commit();
        void Rollback();
    }
}
