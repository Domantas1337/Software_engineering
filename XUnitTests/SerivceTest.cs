using PSIAPI.Services;
using PSIAPI.Models;
using PSI.Models;
using PSI.States;
using PSIAPI.States;
using Xunit.Abstractions;
using Moq;
using PSI.Services;
using PSIAPI.Interfaces;
using PSIAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace XUnitTests
{
    public class SerivceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly LocationItemController _service;
        private readonly Mock<ILocationItemRepository> _locationItemRepoMock = new ();
        public SerivceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _service = new LocationItemController(_locationItemRepoMock.Object);
        }

/*        [Fact]
        public async void GetByIdAsync_NotExists_ReturnsBadRequest()
        {
            var id = Guid.NewGuid().ToString();
            var item = new LocationItemDto
            {
                ID = id,
                City = "New York",
                Street = "Wall Street",
                Latitude = 100,
                Longitude = 100,
                State = PSIAPI.States.UtilityState.Taromat
            };
            _locationItemRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(item);
            var response = await _service.GetByIdAsync(id);
            Assert.Equals(item);
        }*/

        [Fact]
        public async void TestRemoveItem()
        {
            LocationService restService = new(new HttpClient());
            LocationItem locationItem = new()
            {
                ID = "42",
                Street = "Wall Street",
                City = "New York",
                State = PSI.UtilityState.Taromat,
                Longitude = 11,
                Latitude = 12,
            };

            await restService.AddLocationItemAsync(locationItem);
            var items = await restService.GetAllLocationItemsAsync();
            var itemsList = items.FindAll(x => x.ID.Equals("42")).ToList();
            await restService.DeleteLocationItemAsync("42");
            var items2 = await restService.GetAllLocationItemsAsync();
            var itemsList2 = items2.FindAll(x => x.ID.Equals("42")).ToList();


            Assert.True(itemsList.Count() == 1 && itemsList2.Count() == 0);
        }


        [Fact]
        public async void UpdateItem()
        {
            // ARRANGE
            LocationService restService = new(new HttpClient());
            LocationItem locationItem = new()
            {
                ID = "abcdef54321",
                Street = "Wall Street",
                City = "New York",
                State = PSI.UtilityState.Taromat,
                Longitude = 11,
                Latitude = 12
            };
            LocationItem locationItemUpdated = new()
            {
                ID = "abcdef54321",
                Street = "Wall",
                City = "York",
                State = PSI.UtilityState.Litter,
                Longitude = 14,
                Latitude = 19
            };

            // ACT
            await restService.AddLocationItemAsync(locationItem);
            await restService.UpdateLocationItemAsync(locationItemUpdated);
            var itemsAfterUpdate = await restService.GetAllLocationItemsAsync();
            var itemsListAfterUpdate = itemsAfterUpdate.FindAll(x => x.ID.Equals("abcdef54321")).ToList();

            Assert.True(itemsListAfterUpdate.First().CompareTo(locationItemUpdated) == 1);
            await restService.DeleteLocationItemAsync("abcdef54321");

        }
        [Fact]
        public async void AddItem()
        {
            // ARRANGE
            LocationService restService = new(new HttpClient());
            LocationItem locationItem = new()
            {
                ID = "abcdef543212",
                Street = "Wall Street",
                City = "New York",
                State = PSI.UtilityState.Taromat,
                Longitude = 11,
                Latitude = 12
            };

            // ACT
            await restService.AddLocationItemAsync(locationItem);
            var itemsAfterAdd = await restService.GetAllLocationItemsAsync();
            var itemsListAfterAdd = itemsAfterAdd.FindAll(x => x.ID.Equals("abcdef543212")).ToList();

            Assert.True(itemsListAfterAdd.Count() == 1);
            await restService.DeleteLocationItemAsync("abcdef543212");

        }
    }
}