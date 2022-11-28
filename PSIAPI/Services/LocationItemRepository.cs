using Microsoft.EntityFrameworkCore;
using PSIAPI.Interfaces;
using PSIAPI.Models;
using PSIAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace PSIAPI.Services
{
    public class LocationItemRepository : ILocationItemRepository
    {
        private readonly AppDbContext _context;

        public LocationItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var exists = await _context.LocationItems.AnyAsync(t => t.Id.Equals(id));
            return exists;
        }

        public async Task<LocationItem?> FindAsync(string id)
        {
            var locationItemModel = await _context.LocationItems.FirstOrDefaultAsync(t => t.Id.Equals(id));
            return locationItemModel;
        }

        public async void DeleteAsync(string id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }

        public async void AddAsync(LocationItem locationItem)
        {
            await _context.LocationItems.AddAsync(locationItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LocationItem>> GetAllAsync()
        {
            var items = await _context.LocationItems.ToListAsync();
            return items;
        }

        public async void UpdateAsync(string id, LocationItem locationItem, LocationItem locationItemModel)
        {
            locationItemModel.State = locationItem.State;
            locationItemModel.City = locationItem.City;
            locationItemModel.Street = locationItem.Street;
            locationItemModel.Longitude = locationItem.Longitude;
            locationItemModel.Latitude = locationItem.Latitude;
            await _context.SaveChangesAsync();
        }
    }
}
