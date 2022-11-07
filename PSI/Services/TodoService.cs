using PSI.Models;

namespace PSI.Services
{
    public class TodoService : ITodoService
    {
        IRestService _restService;

        public TodoService(IRestService service)
        {
            _restService = service;
        }

        public Task<List<Car>> GetTasksAsync()
        {
            return _restService.RefreshDataAsync();
        }

    }
}
