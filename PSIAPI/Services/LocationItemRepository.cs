using Microsoft.EntityFrameworkCore;
using PSIAPI.Interfaces;
using PSIAPI.Models;
using PSIAPI.Data;

namespace PSIAPI.Services
{
    public class LocationItemRepository : ILocationItemRepository
    {
        private readonly AppDbContext _context;

        public LocationItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LocationItemDto?> GetByIdAsync(string id)
        {
            var item = await _context.LocationItems.FindAsync(id);
            return item;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var exists = await _context.LocationItems.AnyAsync(t => t.ID.Equals(id));
            return exists;
        }

        public async Task<LocationItemDto?> FindAsync(string id)
        {
            var locationItemModel = await _context.LocationItems.FirstOrDefaultAsync(t => t.ID.Equals(id));
            return locationItemModel;
        }

        public async Task DeleteAsync(LocationItemDto item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(LocationItemDto item)
        {
            await _context.LocationItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LocationItemDto>> GetAllAsync()
        {
            var items = await _context.LocationItems.ToListAsync();
            return items;
        }

        public async Task UpdateAsync(LocationItemDto existingItem, LocationItemDto item)
        {
            existingItem.State = item.State;
            existingItem.City = item.City;
            existingItem.Street = item.Street;
            existingItem.Longitude = item.Longitude;
            existingItem.Latitude = item.Latitude;
            await _context.SaveChangesAsync();
        }
    }
}
