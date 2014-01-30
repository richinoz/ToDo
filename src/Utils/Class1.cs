using System.Configuration;
using Core.Domain;

namespace Utils
{
    public interface IEnvironmentHelper {
        string GetDatabaseConnectionString(Database database);
    }
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
    }
}
