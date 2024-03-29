using Microsoft.EntityFrameworkCore;
using PSIAPI.Interfaces;
using PSIAPI.Models;
using PSIAPI.Data;

namespace PSIAPI.Services
{
    public class ReportItemRepository : IReportItemRepository
    {
        private readonly AppDbContext _context;

        public ReportItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var exists = await _context.ReportItems.AnyAsync(t => t.ID.Equals(id));
            return exists;
        }

        public async Task<ReportItemDto?> FindAsync(string id)
        {
            var reportItemModel = await _context.ReportItems.FirstOrDefaultAsync(t => t.ID.Equals(id));
            return reportItemModel;
        }

        public async Task DeleteAsync(ReportItemDto item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(ReportItemDto item)
        {
            await _context.ReportItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReportItemDto>> GetAllAsync()
        {
            var items = await _context.ReportItems.ToListAsync();
            return items;
        }

        public async Task UpdateAsync(ReportItemDto existingItem, ReportItemDto item)
        {
            existingItem.Date = item.Date;
            existingItem.Report = item.Report;
            existingItem.Title = item.Title;
            await _context.SaveChangesAsync();
        }
    }
}
