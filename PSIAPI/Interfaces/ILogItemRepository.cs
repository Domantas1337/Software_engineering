using PSIAPI.Models;

namespace PSIAPI.Interfaces
{
    public interface ILogItemRepository
    {
        public Task<bool> ExistsAsync(string id);
        public Task<LogItem?> FindAsync(string id);
        public Task DeleteAsync(LogItem item);
        public Task AddAsync(LogItem item);
        public Task<List<LogItem>> GetAllAsync();
    }
}
