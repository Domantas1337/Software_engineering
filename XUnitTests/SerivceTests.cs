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
    [Collection("Our Test Collection #6")]

    public class SerivceTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly LocationItemController _service;
        private readonly Mock<ILocationItemRepository> _locationItemRepoMock = new ();
        public SerivceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _service = new LocationItemController(_locationItemRepoMock.Object);
        }

        [Fact]
        public async void Integration_AddDeleteCorrectItem_ItemWasAddedAndDeleted()
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
            await restService.DeleteLocationItemAsync(itemsList.First().ID);
            var items2 = await restService.GetAllLocationItemsAsync();
            var itemsList2 = items2.FindAll(x => x.ID.Equals("42")).ToList();


            Assert.True(itemsList.Count() == 1 && itemsList2.Count() == 0);
        }


        [Fact]
        public async void Integration_AddAndUpdateItem_ItemWasAddedAndUpdated()
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
        public async void Integration_AddAndDeleteItem_ItemWAsAddedAndDeleted()
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