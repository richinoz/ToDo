using System.Runtime.CompilerServices;
using Core.Domain;

namespace Utils {
    public interface IEnvironmentHelper {
        string GetDatabaseConnectionString(Database database);
        T GetConfigSetting<T>(string configKey) where T : struct;
    }
}