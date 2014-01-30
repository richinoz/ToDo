using Core.Data.NHibernate.Sessions;
using NHibernate;

namespace Core.Data.NHibernate.Interfaces {
    public interface INHibernateSessionFactoryBuilder
    {
        ISessionFactory GetSessionFactory(CampAusSession campAusSession);
    }
}
