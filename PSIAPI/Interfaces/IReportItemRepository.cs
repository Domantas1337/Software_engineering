using PSIAPI.Models;

namespace PSIAPI.Interfaces
{
	public interface IReportItemRepository
	{
        public Task<bool> ExistsAsync(string id);
        public Task<ReportItemDto?> FindAsync(string id);
        public Task DeleteAsync(ReportItemDto item);
        public Task AddAsync(ReportItemDto item);
        public Task<List<ReportItemDto>> GetAllAsync();
    }
}
