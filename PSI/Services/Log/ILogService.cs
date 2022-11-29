using PSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Services
{
    public interface ILogService
    {
        Task<List<LogItem>> GetAllLogItemsAsync();
        Task AddLogItemAsync(LogItem item);
        Task DeleteLogItemAsync(string id);
    }
}