using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSI.FileManagers;
using PSI.Generators;
using PSI.Models;
using PSI.Services;

namespace PSI.Logging
{

    public static class Logger
    {

        static private readonly string _fileName = "logs.json";
        static private readonly string _filePath = $"{Constants.CurrentAssemblyPath}\\{_fileName}";

        static public async Task LogAsync(Exception ex, string extraMsg = null, string diffPath = null)
        {
            Debug.WriteLine(diffPath ?? _filePath);
            LogItem logItem = new()
            {
                ID = IDGenerator.GenerateID(),
                Date = DateTime.UtcNow.ToString(),
                Details = extraMsg ?? ex.Message,
                Trace = ex.StackTrace
            };

            await JSONManager.WriteAsync(diffPath ?? _filePath, logItem);
        }

        static public void SendLogs(ILogService logService, string diffFromPath = null)
        {
            List<LogItem> logItems = JSONManager.Read<LogItem>(diffFromPath ?? _filePath);
            logItems.ForEach(async logItem =>
            {
                await logService.AddLogItemAsync(logItem);
            }
            );
        }

        static public async Task<List<LogItem>> GetLogs(ILogService logService)
        {
            List<LogItem> logItems = await logService.GetAllLogItemsAsync();

            return logItems;

        }

    }
}
