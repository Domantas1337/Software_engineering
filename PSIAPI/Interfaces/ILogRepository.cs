using PSIAPI.Models;

namespace PSIAPI.Interfaces
{
	public interface ILogRepository
	{
		bool DoesItemExist(string id);
		IEnumerable<LogItem> All { get; }
		LogItem Find(string id);
		void Insert(LogItem item);
		void Update(LogItem item);
		void Delete(string id);
	}
}
