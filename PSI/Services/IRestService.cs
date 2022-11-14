using PSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Services
{
    public interface IRestService
    {
        Task<List<LocationItem>> GetAllToDosAsync();
        Task AddToDoAsync(LocationItem toDo);
        Task UpdateToDoAsync(LocationItem toDo);
        Task DeleteToDoAsync(int id);
    }
}