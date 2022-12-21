using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls.Internals;
using Moq;
using PSIAPI.Controllers;
using PSIAPI.Data;
using PSIAPI.Interfaces;
using PSIAPI.Models;
using PSIAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace XUnitTests
{
    [Collection("Our Test Collection 2")]

    public class LocationControllerTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private DbContextOptions<AppDbContext> options;
        private AppDbContext appDbContext;
        private ILocationItemRepository locationItemRepo;
        public LocationControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "LocationItemDatabase")
            .Options;

            appDbContext = new AppDbContext(options);
            locationItemRepo = new LocationItemRepository(appDbContext);

        }

        [Fact]
        public async void GetAll()
        {
            string itemID = Guid.NewGuid().ToString("N");
            string itemID2 = Guid.NewGuid().ToString("N");

            var controller = new LocationItemController(locationItemRepo);

            LocationItemDto locationItemDto = new LocationItemDto();
            locationItemDto.Street = "street";
            locationItemDto.City = "city";
            locationItemDto.ID = itemID;
            locationItemDto.Latitude = 11.1;
            locationItemDto.Longitude = 22.2;

            LocationItemDto locationItemDto2 = new LocationItemDto();
            locationItemDto2.Street = "street";
            locationItemDto2.City = "city";
            locationItemDto2.ID = itemID2;
            locationItemDto2.Latitude = 11.1;
            locationItemDto2.Longitude = 22.2;


            await controller.PostAsync(locationItemDto);
            await controller.PostAsync(locationItemDto2);
            var result = controller.GetAllAsync();
            var result3 = (OkObjectResult)result.Result; // <-- Cast is before using it.
            var result4 = result3.Value as List<LocationItemDto>;

            _testOutputHelper.WriteLine(result4.Count().ToString());
            Assert.True(result4.Count() == 2);
        }

        [Fact]
        public async void PostItem()
        {
            var controller = new LocationItemController(locationItemRepo);

            string itemID = Guid.NewGuid().ToString("N");


            LocationItemDto locationItemDto = new LocationItemDto();
            locationItemDto.Street = "street";
            locationItemDto.City = "city";
            locationItemDto.ID = itemID;
            locationItemDto.Latitude = 11.1;
            locationItemDto.Longitude = 22.2;


            var result =  await controller.PostAsync(locationItemDto);
            CreatedResult result2 = (CreatedResult)result;

            Assert.NotNull(result2.Value);
            await controller.DeleteAsync(itemID);
        }

        [Fact]
        public async void DeleteItem()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new LocationItemController(locationItemRepo);

            LocationItemDto locationItemDto = new LocationItemDto();
            locationItemDto.Street = "street";
            locationItemDto.City = "city";
            locationItemDto.ID = itemID;
            locationItemDto.Latitude = 11.1;
            locationItemDto.Longitude = 22.2;


            var result = await controller.PostAsync(locationItemDto);
            var result2 = await controller.DeleteAsync(itemID);

            Assert.True(result2.GetType().Name == "NoContentResult");
        }

        [Fact]
        public async void DeleteItemIncorrectId()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new LocationItemController(locationItemRepo);

            LocationItemDto locationItemDto = new LocationItemDto();
            locationItemDto.Street = "street";
            locationItemDto.City = "city";
            locationItemDto.ID = itemID;
            locationItemDto.Latitude = 11.1;
            locationItemDto.Longitude = 22.2;


            var result = await controller.PostAsync(locationItemDto);
            var result2 = await controller.DeleteAsync("1234");

            Assert.True(result2.GetType().Name == "NotFoundObjectResult");
            await controller.DeleteAsync(itemID);

        }


        [Fact]
        public async void UpdateItem()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new LocationItemController(locationItemRepo);

            LocationItemDto locationItemDto = new LocationItemDto();
            locationItemDto.Street = "street";
            locationItemDto.City = "city";
            locationItemDto.ID = itemID;
            locationItemDto.Latitude = 11.1;
            locationItemDto.Longitude = 22.2;

            LocationItemDto locationItemDto2 = new LocationItemDto();
            locationItemDto2.Street = "streetttttttt";
            locationItemDto2.City = "city";
            locationItemDto2.ID = itemID;
            locationItemDto2.Latitude = 11.1;
            locationItemDto2.Longitude = 22.2;


            await controller.PostAsync(locationItemDto);
            await controller.PutAsync(itemID, locationItemDto2);
            var secondResult = controller.GetByIdAsync(itemID);
            var okSecondResult2 = (OkObjectResult)secondResult.Result;
            var resultValue2 = okSecondResult2.Value as LocationItemDto;


            Assert.True(resultValue2.Street == "streetttttttt");

            await controller.DeleteAsync(itemID);
        }

        [Fact]
        public async void UpdateItem_DoesNotExist()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new LocationItemController(locationItemRepo);

            LocationItemDto locationItemDto = new LocationItemDto();
            locationItemDto.Street = "street";
            locationItemDto.City = "city";
            locationItemDto.ID = itemID;
            locationItemDto.Latitude = 11.1;
            locationItemDto.Longitude = 22.2;

            LocationItemDto locationItemDto2 = new LocationItemDto();
            locationItemDto2.Street = "streetttttttt";
            locationItemDto2.City = "city";
            locationItemDto2.ID = itemID;
            locationItemDto2.Latitude = 11.1;
            locationItemDto2.Longitude = 22.2;


            await controller.PostAsync(locationItemDto);
            var result = await controller.PutAsync("123", locationItemDto2);

            Assert.True(result.GetType().Name == "NotFoundObjectResult");

            await controller.DeleteAsync(itemID);
        }

        [Fact]
        public async void GetItem()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new LocationItemController(locationItemRepo);

            LocationItemDto locationItemDto = new LocationItemDto();
            locationItemDto.Street = "street";
            locationItemDto.City = "city";
            locationItemDto.ID = itemID;
            locationItemDto.Latitude = 11.1;
            locationItemDto.Longitude = 22.2;

                        
            await controller.PostAsync(locationItemDto);
            var result = controller.GetByIdAsync(itemID);
            var result3 = (OkObjectResult)result.Result; // <-- Cast is before using it.
            var result4 = result3.Value as LocationItemDto;

            _testOutputHelper.WriteLine(result4.ID);

            Assert.True(result4.ID == itemID);
            await controller.DeleteAsync(itemID);

        }

        [Fact]
        public async void GetItem_IncorrectID()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new LocationItemController(locationItemRepo);

            LocationItemDto locationItemDto = new LocationItemDto();
            locationItemDto.Street = "street";
            locationItemDto.City = "city";
            locationItemDto.ID = itemID;
            locationItemDto.Latitude = 11.1;
            locationItemDto.Longitude = 22.2;


            await controller.PostAsync(locationItemDto);
            var result = controller.GetByIdAsync("123");
            var result2 = result.Result;

            _testOutputHelper.WriteLine(result2.GetType().ToString());

            Assert.True(result2.GetType().Name == "BadRequestObjectResult");
            await controller.DeleteAsync(itemID);

        }

    }
}
