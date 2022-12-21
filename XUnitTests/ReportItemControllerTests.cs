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
    [Collection("Our Test Collection 3")]

    public class ReportItemControllerTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private DbContextOptions<AppDbContext> options;
        private AppDbContext appDbContext;
        private IReportItemRepository reportItemRepository;
        public ReportItemControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "LocationItemDatabase")
            .Options;

            appDbContext = new AppDbContext(options);
            reportItemRepository = new ReportItemRepository(appDbContext);

        }

        [Fact]
        public async void PutAsync_UpdatedItemDoesntExist_GotNotFoundObjectResult()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new ReportItemController(reportItemRepository);

            ReportItemDto reportItemDto = new ReportItemDto();
            reportItemDto.ID = itemID;
            reportItemDto.Report = "report";
            reportItemDto.Title = "title";
            reportItemDto.Date = "2022-01-01";


            ReportItemDto reportItemDto2 = new ReportItemDto();
            reportItemDto.ID = itemID;
            reportItemDto.Report = "report";
            reportItemDto.Title = "title";
            reportItemDto.Date = "2022-01-01";


            await controller.PostAsync(reportItemDto);
            var result = await controller.PutAsync("123", reportItemDto2);

            Assert.True(result.GetType().Name == "NotFoundObjectResult");

            await controller.DeleteAsync(itemID);
        }

        [Fact]
        public async void PostAsync_UpdatingAddedItem_GotNoContentResult()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new ReportItemController(reportItemRepository);

            ReportItemDto reportItemDto = new ReportItemDto();
            reportItemDto.ID = itemID;
            reportItemDto.Report = "report";
            reportItemDto.Title = "title";
            reportItemDto.Date = "2022-01-01";


            ReportItemDto reportItemDto2 = new ReportItemDto();
            reportItemDto.ID = itemID;
            reportItemDto.Report = "report";
            reportItemDto.Title = "title";
            reportItemDto.Date = "2022-01-01";


            await controller.PostAsync(reportItemDto);
            var result = await controller.PutAsync(itemID, reportItemDto2);

            _testOutputHelper.WriteLine(result.GetType().Name);
            Assert.True(result.GetType().Name == "NoContentResult");

            await controller.DeleteAsync(itemID);
        }

        [Fact]
        public async void PostAsync_AddingTwoItems_TwoItemsWereAdded()
        {
            string itemID = Guid.NewGuid().ToString("N");
            string itemID2 = Guid.NewGuid().ToString("N");

            var controller = new ReportItemController(reportItemRepository);

            ReportItemDto reportItemDto = new();
            reportItemDto.ID = itemID;
            reportItemDto.Report = "report";
            reportItemDto.Title = "title";
            reportItemDto.Date = "2022-02-02";

            ReportItemDto reportItemDto2 = new();
            reportItemDto2.ID = itemID2;
            reportItemDto2.Report = "report";
            reportItemDto2.Title = "title";
            reportItemDto2.Date = "2022-02-02";

            await controller.PostAsync(reportItemDto);
            await controller.PostAsync(reportItemDto2);
            var result = controller.GetAllAsync();
            var result3 = (OkObjectResult)result.Result;
            var result4 = result3.Value as List<ReportItemDto>;

            Assert.True(result4.Count() == 2);
        }

        [Fact]
        public async void PostAsync_AddingItem_ItemSuccessfullyAdded()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new  ReportItemController(reportItemRepository);

            ReportItemDto reportItemDto = new();
            reportItemDto.ID = itemID;
            reportItemDto.Report = "report";
            reportItemDto.Title = "title";
            reportItemDto.Date = "2022-02-02";


            var result = await controller.PostAsync(reportItemDto);
            CreatedResult result2 = (CreatedResult)result;

            Assert.NotNull(result2.Value);
            await controller.DeleteAsync(itemID);

        }

        [Fact]
        public async void DeleteAsync_DeletingAddedItem_NoContentResult()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new ReportItemController(reportItemRepository);

            ReportItemDto reportItemDto = new();
            reportItemDto.ID = itemID;
            reportItemDto.Report = "report";
            reportItemDto.Title = "title";
            reportItemDto.Date = "2022-02-02";

            var result = await controller.PostAsync(reportItemDto);
            var result2 = await controller.DeleteAsync(itemID);

            Assert.True(result2.GetType().Name == "NoContentResult");
            await controller.DeleteAsync(itemID);

        }

        [Fact]
        public async void DeleteAsync_DeletingItemNonExistingIdItem_GotNotFoundObjectResult()
        {
            string itemID = Guid.NewGuid().ToString("N");

            var controller = new ReportItemController(reportItemRepository);

            ReportItemDto reportItemDto = new();
            reportItemDto.ID = itemID;
            reportItemDto.Report = "report";
            reportItemDto.Title = "title";
            reportItemDto.Date = "2022-02-02";

            var result = await controller.PostAsync(reportItemDto);
            var result2 = await controller.DeleteAsync("123");

            Assert.True(result2.GetType().Name == "NotFoundObjectResult");
            await controller.DeleteAsync(itemID);

        }


    }
}
