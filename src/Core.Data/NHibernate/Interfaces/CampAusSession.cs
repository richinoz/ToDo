//using Core.Data.Helpers;
//using CampAustralia.Domain.Enums;

//namespace Core.Data.NHibernate.Interfaces
//{
//    public abstract class CampAusSession
//    {
//        protected CampAusSession()
//        {
//            EnableCache = true;
//        }

//        public abstract string NameSpace { get; }
//        public abstract Database Database { get; }
//        public bool EnableCache { get; set; }

//        public string ConnectionStringName
//        {
//            get
//            {
//                var setting = EnvironmentHelper.GetDatabaseConnectionSettings(this.Database, false);
//                return setting.Name;
//            }
//        }
//    }
//}