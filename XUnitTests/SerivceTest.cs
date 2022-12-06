using PSI.Services;
using PSI.Models;
using PSI.States;
using System.Diagnostics;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitTests
{
    public class SerivceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public SerivceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async void TestRemoveItem()
        {
            LocationService restService = new(new HttpClient());
            LocationItem locationItem = new()
            {
                ID = "42",
                Street = "Wall Street",
                City = "New York",
                State = (PSI.UtilityState)UtilityState.Taromat,
                Longitude = 11,
                Latitude = 12,
            };

            await restService.AddLocationItemAsync(locationItem);
            var items = await restService.GetAllLocationItemsAsync();
            var itemsList = items.FindAll(x => x.ID.Equals("42")).ToList();

            Assert.True(itemsList.Count == 1);
        }


        [Fact]
        public async void TestAddItem()
        {
            // ARRANGE
            LocationService restService = new(new HttpClient());
            LocationItem locationItem = new()
            {
                ID = "abcdef54321",
                Street = "Wall Street",
                City = "New York",
                State = (PSI.UtilityState)UtilityState.Taromat,
                Longitude = 11,
                Latitude = 12,
                Position = new Location(12, 11)
            };
            LocationItem locationItemUpdated = new()
            {
                ID = "abcdef54321",
                Street = "Wall",
                City = "York",
                State = (PSI.UtilityState)UtilityState.Litter,
                Longitude = 14,
                Latitude = 19
            };

            // ACT
            await restService.AddLocationItemAsync(locationItem);
            var itemsAfterAdd = await restService.GetAllLocationItemsAsync();
            var itemsListAfterAdd = itemsAfterAdd.FindAll(x => x.ID.Equals("abcdef54321")).ToList();
            await restService.UpdateLocationItemAsync(locationItemUpdated);
            var itemsAfterUpdate = await restService.GetAllLocationItemsAsync();
            var itemsListAfterUpdate = itemsAfterUpdate.FindAll(x => x.ID.Equals("abcdef54321")).ToList();
            await restService.DeleteLocationItemAsync("abcdef54321");
            var itemsAfterDelete = await restService.GetAllLocationItemsAsync();
            var itemsListAfterDelete = itemsAfterDelete.FindAll(x => x.ID.Equals("abcdef54321")).ToList();

            // ASSERT
            _testOutputHelper.WriteLine(itemsListAfterAdd.Count.ToString());
            Assert.True(itemsListAfterAdd.Count == 1);
            Assert.True(itemsListAfterAdd.First().CompareTo(locationItem) == 1);
            Assert.True(itemsListAfterUpdate.First().CompareTo(locationItemUpdated) == 1);
            Assert.True(itemsListAfterDelete.Count == 0);
        }
    }
}