using System.Reflection;
using System.Web;
using Core.Data.Helpers;
using Core.Data.NHibernate;
using Core.Services.Concrete;
using Ninject;
using Ninject.Extensions.Conventions;
using Utils;

namespace ToDo.Bootstrapper
{
    public class Bootstrapper
    {
        public IKernel CreateKernel()
        {
            var kernel = new StandardKernel();


            kernel.Bind(x => x
                 .FromAssemblyContaining<EnvironmentHelper>()
                     .SelectAllClasses()
                 .BindDefaultInterface()
                 .Configure(b => b.InScope(ctx => HttpContext.Current)
            ));

            kernel.Bind(x => x
                  .FromAssemblyContaining<ToDoService>()
                      .SelectAllClasses()                   
                  .BindDefaultInterface()
                  .Configure(b => b.InScope(ctx => HttpContext.Current)
             ));

        
            //load all ninjectmodules in this assembly!
            kernel.Load(Assembly.GetExecutingAssembly().Location);

            return kernel;
        }
    }
}