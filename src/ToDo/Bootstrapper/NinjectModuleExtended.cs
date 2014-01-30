using System;
using System.Linq;
using System.Web;
using Ninject.Infrastructure;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using Ninject.Syntax;

namespace ToDo.Bootstrapper
{
    public class NinjectModuleExtended : NinjectModule
    {
        public enum NinjectBindingScope
        {
            Native,
            Transient,
            Singleton,
            Thread,
            Request
        }

        internal Func<Ninject.Activation.IContext, object> _scope;

        public NinjectModuleExtended(NinjectBindingScope bindingScope)
        {
            _scope = GetScope(bindingScope);
        }

        public static Func<Ninject.Activation.IContext, object> GetScope(NinjectBindingScope bindingScope)
        {
            switch (bindingScope)
            {
                case NinjectBindingScope.Request:
                    return ctx => HttpContext.Current;
                case NinjectBindingScope.Singleton:
                    return StandardScopeCallbacks.Singleton;
                case NinjectBindingScope.Transient:
                    return StandardScopeCallbacks.Transient;
                case NinjectBindingScope.Thread:
                    return StandardScopeCallbacks.Thread;
                case NinjectBindingScope.Native:
                    return null;
                default:
                    return null;
            }
        }

        public new IBindingToSyntax<T> Bind<T>()
        {
            var bindingExists = this.Kernel.GetBindings(typeof(T));
           
            if (bindingExists.Any())
            {
                Console.Write("bust");
                // throw new Ninject.ActivationException(string.Format("More than one binding for '{0}'", typeof(T).Name));
            }
            //return !bindingExists.Any() ? base.Bind<T>() : Rebind<T>();
            return base.Bind<T>();
        }

        public override void AddBinding(IBinding binding)
        {

            if (_scope != null)
                binding.ScopeCallback = _scope;

            base.AddBinding(binding);
        }

        public override void Load()
        {
            //this is never called!
        }
    }
}