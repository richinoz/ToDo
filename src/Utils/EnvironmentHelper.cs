using System;
using System.Configuration;
using Core.Domain;

namespace Utils
{
    public class EnvironmentHelper : IEnvironmentHelper
    {
      
        public string GetDatabaseConnectionString(Database database)
        {
            var connectionString = string.Empty;

            switch (database) {
                case Database.ToDoDb:
                    connectionString = Constants.Database.DefaultConnectionNameKey;
                               //ConfigurationManager.ConnectionStrings[Constants.Database.DefaultConnectionNameKey].ConnectionString;
                    break;
            }


            return connectionString;

        }

        public T GetConfigSetting<T>(string configKey) where T : struct {
            
            var ret =ConfigurationManager.AppSettings.Get(configKey);
            return (T)Convert.ChangeType(ret, typeof (T));
        }

    }
}
