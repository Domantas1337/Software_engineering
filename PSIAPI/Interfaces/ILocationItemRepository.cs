using PSIAPI.Models;

namespace PSIAPI.Interfaces
{
    public interface ILocationItemRepository
    {
        public Task<bool> ExistsAsync(string id);
        public Task<LocationItem?> FindAsync(string id);
        public Task DeleteAsync(string id);
        public Task AddAsync(LocationItem item);
        public Task<List<LocationItem>> GetAllAsync();
        public Task UpdateAsync(LocationItem existingItem, LocationItem item);
    }
}
