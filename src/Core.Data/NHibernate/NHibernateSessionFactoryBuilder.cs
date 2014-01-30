using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data.NHibernate.Extensions;
using Core.Data.NHibernate.Sessions;
using Core.Data.NHibernate.Interfaces;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;

namespace Core.Data.NHibernate
{
    public class NHibernateSessionFactoryBuilder : INHibernateSessionFactoryBuilder
    {
        private readonly Dictionary<string, ISessionFactory> _sessionFactories = new Dictionary<string, ISessionFactory>();

        public ISessionFactory GetSessionFactory(CampAusSession campAusSession)
        {
            ISessionFactory sessionFactory;

            if (_sessionFactories.ContainsKey(campAusSession.NameSpace))
                sessionFactory = _sessionFactories[campAusSession.NameSpace];
            else
            {
                sessionFactory = CreateSessionFactory(campAusSession.NameSpace, campAusSession.ConnectionStringName, campAusSession.EnableCache);
                _sessionFactories.Add(campAusSession.NameSpace, sessionFactory);
            }

            return sessionFactory;
        }

        private ISessionFactory CreateSessionFactory(string mapToNamespace, string connectionStringName, bool enableCache)
        {
            var config = Fluently.Configure(new Configuration()
            {
#if DEBUG 
                Interceptor = new SqlStatementInterceptor() //RecompileInterceptor 
#endif
            });

            if (enableCache)
                config.Cache(x => x.UseQueryCache().UseSecondLevelCache().ProviderClass(typeof(global::NHibernate.Caches.SysCache.SysCacheProvider).AssemblyQualifiedName));
            
            config.Database(
                MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey(connectionStringName)))
                .Mappings(
                    m =>
                        m.FluentMappings.AddFromAssemblyOf<TransactionException>(t => t.Namespace.StartsWith(mapToNamespace))
                            .Conventions.Add(ForeignKey.EndsWith("Id"))
                            .Conventions.Setup(x => x.Add(AutoImport.Never()))
                        );

            config.ExposeConfiguration(c => c.LinqToHqlGeneratorsRegistry<LinqToHqlGeneratorsRegistry>());

            return config.BuildSessionFactory();
        }

    }

    public static class FluentNHibernateExtensions
    {
        public static FluentMappingsContainer AddFromAssemblyOf<T>(this FluentMappingsContainer mappings, Predicate<Type> where)
        {
            if (where == null)
                return mappings.AddFromAssemblyOf<T>();

            var mappingClasses = typeof(T).Assembly.GetExportedTypes()
                .Where(x => typeof(IMappingProvider).IsAssignableFrom(x)
                    && where(x));

            foreach (var type in mappingClasses)
            {
                mappings.Add(type);
            }

            return mappings;
        }
    }

    public class RecompileInterceptor : EmptyInterceptor
    {
        public override global::NHibernate.SqlCommand.SqlString OnPrepareStatement(global::NHibernate.SqlCommand.SqlString sql)
        {
            return sql.Append(" option(recompile)");
        }
    }


}
