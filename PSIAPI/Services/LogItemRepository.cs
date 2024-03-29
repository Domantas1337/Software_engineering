﻿using Microsoft.EntityFrameworkCore;
using PSIAPI.Interfaces;
using PSIAPI.Models;
using PSIAPI.Data;

namespace PSIAPI.Services
{
    public class LogItemRepository : ILogItemRepository
    {
        private readonly AppDbContext _context;

        public LogItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var exists = await _context.LogItems.AnyAsync(t => t.ID.Equals(id));
            return exists;
        }

        public async Task<LogItemDto?> FindAsync(string id)
        {
            var locationItemModel = await _context.LogItems.FirstOrDefaultAsync(t => t.ID.Equals(id));
            return locationItemModel;
        }

        public async Task DeleteAsync(LogItemDto item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(LogItemDto item)
        {
            await _context.LogItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LogItemDto>> GetAllAsync()
        {
            var items = await _context.LogItems.ToListAsync();
            return items;
        }
    }
}
