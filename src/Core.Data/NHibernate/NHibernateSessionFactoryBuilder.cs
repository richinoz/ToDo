using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data.Mappings.dbo;
using Core.Data.NHibernate.Extensions;
using Core.Data.NHibernate.Sessions;
using Core.Data.NHibernate.Interfaces;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Instances;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Core.Data.NHibernate
{
    public class NHibernateSessionFactoryBuilder : INHibernateSessionFactoryBuilder
    {
        private readonly Dictionary<string, ISessionFactory> _sessionFactories = new Dictionary<string, ISessionFactory>();

        public ISessionFactory GetSessionFactory(SessionResolver sessionResolver)
        {
            ISessionFactory sessionFactory;

            if (_sessionFactories.ContainsKey(sessionResolver.NameSpace))
                sessionFactory = _sessionFactories[sessionResolver.NameSpace];
            else
            {
                sessionFactory = CreateSessionFactory(sessionResolver.NameSpace, sessionResolver.ConnectionStringName, sessionResolver.EnableCache);
                _sessionFactories.Add(sessionResolver.NameSpace, sessionFactory);
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

         
            return config
               .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey(connectionStringName)))
               .Mappings(m =>
                         m.AutoMappings.Add(CreateAutomappings))
               .ExposeConfiguration(BuildSchema)
               .BuildSessionFactory();
        }

        private static AutoPersistenceModel CreateAutomappings()
        {
            // This is the actual automapping - use AutoMap to start automapping,
            // then pick one of the static methods to specify what to map (in this case
            // all the classes in the assembly that contains Employee), and then either
            // use the Setup and Where methods to restrict that behaviour, or (preferably)
            // supply a configuration instance of your definition to control the automapper.
            return
                AutoMap.AssemblyOf<ATestClass>(new ExampleAutomappingConfiguration())
                .Conventions
                    .Add<CascadeConvention>()
                .Conventions
                    .Add(ForeignKey.EndsWith("Id"))                            
                .Conventions.Setup(x => x.Add(AutoImport.Never()));
        }
        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            //if (File.Exists(DbFile))
            //    File.Delete(DbFile);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            //new SchemaExport(config)
            //    .Create(false, true);

            //new SchemaExport(config).SetOutputFile(@"C:\Test.sql").Create(true, true);
            new SchemaExport(config).Create(true, true);
        }

    }

    public class CascadeConvention : IReferenceConvention, IHasManyConvention, IHasManyToManyConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Cascade.All();
        }

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Cascade.All();
        }

        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Cascade.All();
        }
    }
    public class ExampleAutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "Core.Data.Mappings.dbo";
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
