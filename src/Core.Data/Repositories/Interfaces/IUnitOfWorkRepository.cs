using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Repositories.Interfaces
{
    public interface IUnitOfWorkRepository<T> : IRepository<T>, IDisposable where T :class 
    {
        void Begin();
        void End();
    }
}
