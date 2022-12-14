using PSIAPI.Models;

namespace PSIAPI.Interfaces
{
    public interface ILocationItemRepository
    {
        public Task<LocationItemDto?> GetByIdAsync(string id);
        public Task<bool> ExistsAsync(string id);
        public Task<LocationItemDto?> FindAsync(string id);
        public Task DeleteAsync(LocationItemDto item);
        public Task AddAsync(LocationItemDto item);
        public Task<List<LocationItemDto>> GetAllAsync();
        public Task UpdateAsync(LocationItemDto existingItem, LocationItemDto item);
    }
}
