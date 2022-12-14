using PSIAPI.Models;

namespace PSIAPI.Interfaces
{
    public interface ILogItemRepository
    {
        public Task<bool> ExistsAsync(string id);
        public Task<LogItemDto?> FindAsync(string id);
        public Task DeleteAsync(LogItemDto item);
        public Task AddAsync(LogItemDto item);
        public Task<List<LogItemDto>> GetAllAsync();
    }
}
