using PSI.Models;

namespace PSI.Services
{
    public interface ITodoService
    {
        Task<List<Car>> GetTasksAsync();
    }
}
