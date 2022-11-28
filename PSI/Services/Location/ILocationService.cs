using PSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Services
{
    public interface ILocationService
    {
        Task<List<LocationItem>> GetAllLocationItemsAsync();
        Task AddLocationItemAsync(LocationItem locationItem);
        Task UpdateLocationItemAsync(LocationItem locationItem);
        Task DeleteLocationItemAsync(int id);

        event EventHandler<LocationEventArgs> LocationsExist;
    }
}