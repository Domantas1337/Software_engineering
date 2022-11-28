using PSIAPI.Interfaces;
using PSIAPI.Models;

namespace PSIAPI.Services
{
    public class LogRepository : ILogRepository
    {
        private List<LogItem> _logList;

        public LogRepository()
        {
            _logList = new List<LogItem>();
            InitializeData();
        }

        public IEnumerable<LogItem> All
        {
            get { return _logList; }
        }

        public bool DoesItemExist(string id)
        {
            return _logList.Any(item => item.Id == id);
        }

        public LogItem Find(string id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _logList.FirstOrDefault(item => item.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Insert(LogItem item)
        {
            _logList.Add(item);
        }

        public void Update(LogItem item)
        {
            var todoItem = this.Find(item.Id);
            var index = _logList.IndexOf(todoItem);
            _logList.RemoveAt(index);
            _logList.Insert(index, item);
        }

        public void Delete(string id)
        {
            _logList.Remove(this.Find(id));
        }

        private void InitializeData()
        {
            var logItem = new LogItem
            {
                Id = new Guid().ToString("N"),
                exceptionDetails = "TestCity",
                dateTime = "Test",
            };

            _logList.Add(logItem);
        }
    }
}
