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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace XUnitTests
{
    [Collection("Our Test Collection 1")]

    public class LogItemControllerTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private DbContextOptions<AppDbContext> options;
        private AppDbContext appDbContext;
        private ILogItemRepository logItemRepository;
        public LogItemControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "LocationItemDatabase")
            .Options;

            appDbContext = new AppDbContext(options);
            logItemRepository = new LogItemRepository(appDbContext);

        }


        [Fact]
        public async void PostAsync_AddedItem_CreatedResultReturned()
        {

            string itemID = Guid.NewGuid().ToString("N");

            var controller = new LogItemController(logItemRepository);

            LogItemDto logItemDto = new();
            logItemDto.ID = itemID;
            logItemDto.Date = "9999-09-09";
            logItemDto.Details = "details";
            logItemDto.Trace = "trace";

            var result = await controller.PostAsync(logItemDto);
            CreatedResult result2 = (CreatedResult)result;

            Assert.NotNull(result2.Value);
        }

        [Fact]
        public async void DeleteAsync_ItemDeleted_NoContentResultReturned()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new LogItemController(logItemRepository);

            LogItemDto logItemDto = new();
            logItemDto.ID = itemID;
            logItemDto.Date = "9999-09-09";
            logItemDto.Details = "details";
            logItemDto.Trace = "trace";



            var result = await controller.PostAsync(logItemDto);
            var result2 = await controller.DeleteAsync(itemID);

            Assert.True(result2.GetType().Name == "NoContentResult");
        }

        [Fact]
        public async void GetAllAsync_PostedTwoItems_SameTwoItemsReceived()
        {
            string itemID = Guid.NewGuid().ToString("N");
            string itemID2 = Guid.NewGuid().ToString("N");


            var controller = new LogItemController(logItemRepository);

            LogItemDto logItemDto = new();
            logItemDto.ID = itemID;
            logItemDto.Date = "9999-09-09";
            logItemDto.Details = "details";
            logItemDto.Trace = "trace";

            LogItemDto logItemDto2 = new();
            logItemDto2.ID = itemID2;
            logItemDto2.Date = "9999-09-09";
            logItemDto2.Details = "details";
            logItemDto2.Trace = "trace";

            await controller.PostAsync(logItemDto);
            await controller.PostAsync(logItemDto2);
            var result = controller.GetAllAsync();
            var result3 = (OkObjectResult)result.Result;
            var result4 = result3.Value as List<LogItemDto>;

            _testOutputHelper.WriteLine(result4.Count().ToString());
            Assert.True(result4.Count() == 2);
        }

        [Fact]
        public async void DeleteAsync_DeletingNonExistingItem_GotNotFoundObjectResult()
        {
            string itemID = Guid.NewGuid().ToString("N");
            var controller = new LogItemController(logItemRepository);

            LogItemDto logItemDto = new();
            logItemDto.ID = itemID;
            logItemDto.Date = "9999-09-09";
            logItemDto.Details = "details";
            logItemDto.Trace = "trace";



            var result = await controller.PostAsync(logItemDto);
            var result2 = await controller.DeleteAsync("123");

            _testOutputHelper.WriteLine(result2.GetType().Name);
            Assert.True(result2.GetType().Name == "NotFoundObjectResult");
        }


    }
}
