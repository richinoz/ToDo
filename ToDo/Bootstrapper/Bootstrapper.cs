using System.Reflection;
using System.Web;
using Ninject;
using Ninject.Extensions.Conventions;
namespace ToDo.Bootstrapper
{
    public class Bootstrapper
    {
        public IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind(x => x
                   .FromThisAssembly()
                       .SelectAllClasses()
                   .BindDefaultInterface()
                   .Configure(b => b.InScope(ctx => HttpContext.Current)
              ));

            kernel.Bind(x => x
                  .FromAssemblyContaining<Core.Services.ToDoService>()
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