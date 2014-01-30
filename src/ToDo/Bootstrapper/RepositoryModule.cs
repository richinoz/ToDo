using Core.Data.Repositories;
using Core.Data.Repositories.Interfaces;

namespace ToDo.Bootstrapper
{
    public class RepositoryModule : NinjectModuleExtended
    {
        public RepositoryModule()
            : base(NinjectBindingScope.Request)
        {
        }

        public override void Load()
        {
            Bind(typeof(IRepository<>)).To(typeof(DbRepository<>));
            //Bind(typeof(IUnitOfWorkRepository<>)).To(typeof(UnitOfWorkRepository<>));

        }
    }
}