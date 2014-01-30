using System.Collections.Generic;
using CampAustralia.Core.Helpers;
using Core.Data.NHibernate.Extensions;
using Core.Data.NHibernate.Sessions;
using Core.Data.NHibernate.Interfaces;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;

namespace Core.Data.NHibernate
{
    public class NHibernateBillingSessionFactoryBuilder : 
                 INHibernateSessionFactoryBuilder
    {
        private readonly Dictionary<string, ISessionFactory> _sessionFactories = new Dictionary<string, ISessionFactory>();

        public ISessionFactory GetSessionFactory(CampAusSession campAusSession)
        {
            ISessionFactory sessionFactory; 

            if (_sessionFactories.ContainsKey(campAusSession.NameSpace))
                sessionFactory = _sessionFactories[campAusSession.NameSpace];
            else
            {
                // parse connection string for a custom timeout setting
                string conn = "connectionHEre";// EnvironmentHelper.GetDatabaseConnectionSettings(campAusSession.Database, false).ConnectionString;
                var csb = new System.Data.SqlClient.SqlConnectionStringBuilder(conn);

                sessionFactory = CreateSessionFactory(campAusSession.NameSpace, campAusSession.ConnectionStringName, csb.ConnectTimeout);
                _sessionFactories.Add(campAusSession.NameSpace, sessionFactory);
            }

            return sessionFactory;
        }

        private ISessionFactory CreateSessionFactory(string mapToNamespace, string connectionStringName,int timeout = 0)
        {
            var config = Fluently.Configure(new Configuration()
                            {
                                Interceptor = new SqlStatementInterceptor() //RecompileInterceptor 
                            });

            config.Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey(connectionStringName)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TransactionManager>(t => t.Namespace.StartsWith(mapToNamespace))
                            .Conventions.Add(ForeignKey.EndsWith("Id"))
                            .Conventions.Setup(x => x.Add(AutoImport.Never())))
                .Cache(x => x.Not.UseSecondLevelCache())
                .ExposeConfiguration(c => c.LinqToHqlGeneratorsRegistry<LinqToHqlGeneratorsRegistry>())
                .ExposeConfiguration(c => c.SetProperty("command_timeout", string.Format("{0}", (timeout > 0 ? timeout : 600))));

            return config.BuildSessionFactory();
        }

    }
}
