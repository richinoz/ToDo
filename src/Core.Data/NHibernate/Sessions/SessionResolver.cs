using Core.Domain;
using Utils;

namespace Core.Data.NHibernate.Sessions
 {

    public abstract class SessionResolver {
        private readonly IEnvironmentHelper _environmentHelper;

        protected SessionResolver(IEnvironmentHelper environmentHelper) {
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

        
    }
 }