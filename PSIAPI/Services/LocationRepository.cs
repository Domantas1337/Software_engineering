using PSIAPI.Interfaces;
using PSIAPI.Models;

namespace PSIAPI.Services
{
    public class LocationRepository : ILocationRepository
    {
        private List<LocationItem> _locationList;

        public LocationRepository()
        {
            _locationList = new List<LocationItem>();
            InitializeData();
        }

        public IEnumerable<LocationItem> All
        {
            get { return _locationList; }
        }

        public bool DoesItemExist(string id)
        {
            return _locationList.Any(item => item.Id == id);
        }

        public LocationItem Find(string id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _locationList.FirstOrDefault(item => item.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Insert(LocationItem item)
        {
            _locationList.Add(item);
        }

        public void Update(LocationItem item)
        {
            var todoItem = this.Find(item.Id);
            var index = _locationList.IndexOf(todoItem);
            _locationList.RemoveAt(index);
            _locationList.Insert(index, item);
        }

        public void Delete(string id)
        {
            _locationList.Remove(this.Find(id));
        }

        private void InitializeData()
        {
            var locationItem = new LocationItem
            {
                Id = new Guid().ToString("N"),
                State = States.UtilityState.Taromat,
                City = "TestCity",
                Street = "Test",
                Longitude = 0,
                Latitude = 0
            };

            _locationList.Add(locationItem);
        }
    }
}
