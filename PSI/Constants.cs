using System.Reflection;

namespace PSI
{
    public class Constants
    {
        // For testing
        public static string currentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string locationsJsonName = "locations.json";
        public static string locationsFilePath = $"{currentAssemblyPath}\\{locationsJsonName}";
    }
}
