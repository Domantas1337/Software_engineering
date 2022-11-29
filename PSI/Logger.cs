using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSI.FileManagers;
using PSI.Generators;
using PSI.Models;
using PSI.Services;

namespace PSI
{
    public static class Logger
    {
        static private readonly string _fileName = "logs.json";
        static private readonly string _filePath = $"{Constants.CurrentAssemblyPath}\\{_fileName}";
        
        static public async Task LogAsync(Exception ex, string extraMsg = null)
        {
            LogItem logItem = new()
            {
                ID = IDGenerator.GenerateID(),
                Date = DateTime.UtcNow.ToString(),
                Details = extraMsg ?? ex.Message,
                Trace = ex.StackTrace
            };
            await JSONManager<LogItem>.WriteAsync(_filePath, logItem);
        }

        static public async Task SendLogsAsync(ILogService logService)
        {
            List<LogItem> logItems = await JSONManager<LogItem>.ReadAsync(_filePath);
            logItems.ForEach(logItem =>
            {
                logService.AddLogItemAsync(logItem);
            }
            );
        }
    }
}
