using System;
using System.Configuration;

namespace Core.Data.Helpers
{
    public class ConfigurationHelper
    {
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static int GetValueInt(string key)
        {
            return int.Parse(GetValue(key));
        }

        public static double GetValueDouble(string key)
        {
            return double.Parse(GetValue(key));
        }

        public static bool GetValueBool(string key)
        {
            return GetValue(key).ToLower() == Boolean.TrueString.ToLower();
        }
    }
}
