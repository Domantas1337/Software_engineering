using System.Reflection;

namespace PSI
{
    public static class Constants
    {
        // For testing
        public static string CurrentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        public static string LocationsJsonName = "locations.json";
        public static string LocationsFilePath = $"{CurrentAssemblyPath}\\{LocationsJsonName}";

        public static string ReportsJsonName = "reports.json";
        public static string ReportsFilePath = $"{CurrentAssemblyPath}\\{ReportsJsonName}";
    }
}
