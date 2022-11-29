using System.Reflection;

namespace PSI
{
    public static class Constants
    {
        public static readonly string CurrentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        public static readonly string LocationsJsonName = "locations.json";
        public static readonly string LocationsFilePath = $"{CurrentAssemblyPath}\\{LocationsJsonName}";

        public static readonly string ReportsJsonName = "reports.json";
        public static readonly string ReportsFilePath = $"{CurrentAssemblyPath}\\{ReportsJsonName}";

        public static readonly string UsersJsonName = "users.json";
        public static readonly string UsersFilePath = $"{CurrentAssemblyPath}\\{UsersJsonName}";
    }
}
