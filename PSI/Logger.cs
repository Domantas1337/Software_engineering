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
            Debug.WriteLine(_filePath);
            LogItem logItem = new()
            {
                ID = IDGenerator.GenerateID(),
                Date = DateTime.UtcNow.ToString(),
                Details = extraMsg ?? ex.Message,
                Trace = ex.StackTrace
            };

            OnLogAdded(this, logItem);
            await JSONManager.WriteAsync<LogItem>(_filePath, logItem);
        }

        static public void SendLogs(ILogService logService)
        {
            List<LogItem> logItems = JSONManager.Read<LogItem>(_filePath);
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
