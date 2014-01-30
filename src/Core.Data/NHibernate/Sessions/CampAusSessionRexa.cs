using Core.Domain;
using Utils;

namespace Core.Data.NHibernate.Sessions
 {

    public abstract class CampAusSession {
        private readonly IEnvironmentHelper _environmentHelper;

        protected CampAusSession(IEnvironmentHelper environmentHelper) {
            _environmentHelper = environmentHelper;
            EnableCache = true;
        }

        public abstract string NameSpace { get; }
        public abstract Database Database { get; }
        public bool EnableCache { get; set; }

        public string ConnectionStringName {
            get {
                var setting = _environmentHelper.GetDatabaseConnectionString(this.Database);
                return setting;
            }
        }

        public class CampAusSessionRexa : CampAusSession {
            public CampAusSessionRexa(IEnvironmentHelper environmentHelper) : base(environmentHelper) {
            }

            public override string NameSpace {
                get {
                    return "Core.Data.Mappings.dbo";
                }
            }

            public override Database Database {
                get { return Database.ToDoDb; }
            }
        }
    }
 }