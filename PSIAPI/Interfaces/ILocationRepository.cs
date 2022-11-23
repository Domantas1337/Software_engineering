using PSIAPI.Models;

namespace PSIAPI.Interfaces
{
    public interface ILocationRepository
    {
        bool DoesItemExist(string id);
        IEnumerable<LocationItem> All { get; }
        LocationItem Find(string id);
        void Insert(LocationItem item);
        void Update(LocationItem item);
        void Delete(string id);
    }
}
