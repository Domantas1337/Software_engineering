using System.Reflection;

namespace PSI
{
    public static class Constants
    {
        // For testing
        public static string currentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        public static string locationsJsonName = "locations.json";
        public static string locationsFilePath = $"{currentAssemblyPath}\\{locationsJsonName}";

        public static string reportsJsonName = "reports.json";
        public static string reportsFilePath = $"{currentAssemblyPath}\\{reportsJsonName}";
    }
}
