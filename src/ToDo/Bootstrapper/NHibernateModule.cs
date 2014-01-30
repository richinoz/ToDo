using System.Data;
using System.Linq;
using Core.Data.NHibernate;
using Core.Data.NHibernate.Interfaces;
using Core.Data.NHibernate.Sessions;
using Core.Data.Repositories;
using NHibernate;
using Ninject;
using Utils;

namespace ToDo.Bootstrapper
{
    public class NHibernateModule : NinjectModuleExtended
    {
        
        public NHibernateModule():base(NinjectBindingScope.Request) {
            
        }
        public override void Load()
        {
            //NOTE: We must call InScope *after* WhenInjectedInto - because WhenInjectedInto seems to blow away
            // the ScopeCallback on the binding (which we are setting in 
            var environmentHelper = Kernel.GetAll<IEnvironmentHelper>().First();            

            Bind<INHibernateSessionFactoryBuilder>().To<NHibernateSessionFactoryBuilder>().InSingletonScope();

            Bind<ISession>().ToMethod(ctx => ctx.Kernel.Get<INHibernateSessionFactoryBuilder>()
                .GetSessionFactory(new SessionResolverToDoDb(environmentHelper))
                .OpenSession())
                .WhenInjectedInto(typeof(DbRepository<>))
                .InScope(_scope)
                .OnActivation(OpenTransaction)
                .OnDeactivation(CloseTransaction);

            //Bind<IStatelessSession>().ToMethod(ctx => ctx.Kernel.Get<INHibernateSessionFactoryBuilder>()
            //    .GetSessionFactory(new SessionResolverToDoDb())
            //    .OpenStatelessSession())
            //    .WhenInjectedInto(typeof(DbStatelessRepository<>))
            //    .InScope(_scope);

          
        }

        private void OpenTransaction(ISession session)
        {
            session.BeginTransaction();
        }

        private void CloseTransaction(ISession session)
        {
           
            try
            {
                session.Transaction.Commit();
                session.Transaction.Dispose();
            }
            catch
            {
                session.Transaction.Rollback();
                session.Dispose();
            }
            finally
            {
                session.Dispose();
            }
        }
    }
}
