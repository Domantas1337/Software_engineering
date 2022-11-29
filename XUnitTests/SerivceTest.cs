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
                Latitude = 12
            };

            await restService.AddLocationItemAsync(locationItem);
            await restService.DeleteLocationItemAsync("42");
            var items = await restService.GetAllLocationItemsAsync();
            var itemsList = items.FindAll(x => x.ID.Equals("42")).ToList();

            Assert.True(itemsList.Count == 0);
        }

        [Fact]
        public async void TestAddItem()
        {
            // ARRANGE
            LocationService restService = new(new HttpClient());
            LocationItem locationItem = new ()
            {
                ID = "42",
                Street = "Wall Street",
                City = "New York",
                State = (PSI.UtilityState)UtilityState.Taromat,
                Longitude = 11,
                Latitude = 12
            };

            // ACT
            var items0 = await restService.GetAllLocationItemsAsync();
            await restService.AddLocationItemAsync(locationItem);
            var items = await restService.GetAllLocationItemsAsync();
            var itemsList = items.FindAll(x => x.ID.Equals("42")).ToList();

            // ASSERT
            _testOutputHelper.WriteLine(itemsList.Count.ToString());
            _testOutputHelper.WriteLine(itemsList.Count.ToString());
            Assert.True(itemsList.Count == 1);
            Assert.True(itemsList.First().Equals(locationItem));

        }
    }
}