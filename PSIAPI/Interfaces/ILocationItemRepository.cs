using PSIAPI.Models;

namespace PSIAPI.Interfaces
{
    public interface ILocationItemRepository
    {
        public Task<bool> ExistsAsync(string id);
        public Task<LocationItem?> FindAsync(string id);
        public void DeleteAsync(string id);
        public void AddAsync(LocationItem locationItem);
        public Task<List<LocationItem>> GetAllAsync();
        public void UpdateAsync(string id, LocationItem locationItem, LocationItem locationItemModel);
    }
}
