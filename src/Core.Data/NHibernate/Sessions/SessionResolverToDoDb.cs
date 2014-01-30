using Core.Domain;
using Utils;

namespace Core.Data.NHibernate.Sessions
{
    public class SessionResolverToDoDb : SessionResolver
    {
        public SessionResolverToDoDb(IEnvironmentHelper environmentHelper)
            : base(environmentHelper)
        {
        }

        public override string NameSpace
        {
            get
            {
                return "Core.Data.Mappings.dbo";
            }
        }

        public override Database Database
        {
            get { return Database.ToDoDb; }
        }
    }
}