using PSIAPI.Models;

namespace PSIAPI.Interfaces
{
	public interface IReportItemRepository
	{
        public Task<bool> ExistsAsync(string id);
        public Task<ReportItem?> FindAsync(string id);
        public Task DeleteAsync(ReportItem item);
        public Task AddAsync(ReportItem item);
        public Task<List<ReportItem>> GetAllAsync();
    }
}
