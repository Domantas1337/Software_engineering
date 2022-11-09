using PSI.Models;

namespace PSI.Services
{
    public interface IRestService
    {
        Task<List<Car>> RefreshDataAsync();

    }
}
