using PSI.Services;
using PSI.Models;
using PSI.States;
using System.Diagnostics;
using Xunit.Abstractions;
using Xunit.Sdk;
using PSIAPI.Controllers;
using PSIAPI.Models;

namespace XUnitTests
{
    [Collection("Our Test Collection #4")]

    public class ReportSerivceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public ReportSerivceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }


        [Fact]
        public async void AddLocationItemAsync_AddingCorrectItem_ItemWasAdded()
        {
            ReportRestService reportService = new(new HttpClient());
            ReportItem reportItem = new()
            {
                ID = "423",
                Date = "2022-02-03",
                Report = "report",
                Title = "title"
            };

            await reportService.AddLocationItemAsync(reportItem);
            var items = await reportService.GetAllLocationItemsAsync();
            var itemsList = items.FindAll(x => x.ID.Equals("423")).ToList();
            
            _testOutputHelper.WriteLine( itemsList.First().Title);
            _testOutputHelper.WriteLine(itemsList.First().ID);

            Assert.True(itemsList.Count == 1);
        }

        [Fact]
        public async void Integration_DeletingAddedLocation_ItemWasDeleted()
        {
            ReportRestService reportRestService = new(new HttpClient());
            ReportItem reportItem = new()
            {
                ID = "521",
                Date = "2022-02-03",
                Report = "report",
                Title = "title"
            };


            await reportRestService.AddLocationItemAsync(reportItem);
            var items1 = await reportRestService.GetAllLocationItemsAsync();
            var itemsList1 = items1.FindAll(x => x.ID.Equals("521")).ToList();
            await reportRestService.DeleteLocationItemAsync("521");
            var items2 = await reportRestService.GetAllLocationItemsAsync();
            var itemsList2 = items2.FindAll(x => x.ID.Equals("521")).ToList();

            _testOutputHelper.WriteLine(itemsList1.Count.ToString());
            _testOutputHelper.WriteLine(itemsList2.Count.ToString());

            Assert.True(itemsList1.Count == 1 && itemsList2.Count == 0);
        }

        [Fact]
        public async void Integration_UpdatingAddedItem_ItemWasUpdated()
        {
            ReportRestService reportRestService = new(new HttpClient());
            ReportItem reportItem = new()
            {
                ID = "521",
                Date = "2022-02-03",
                Report = "report",
                Title = "title"
            };
            ReportItem reportItem2 = new()
            {
                ID = "521",
                Date = "2022-02-03",
                Report = "reportttt",
                Title = "title"
            };


            await reportRestService.AddLocationItemAsync(reportItem);
            var items1 = await reportRestService.GetAllLocationItemsAsync();
            var itemsList1 = items1.FindAll(x => x.ID.Equals("521")).ToList();
            await reportRestService.UpdateLocationItemAsync(reportItem2);
            var items2 = await reportRestService.GetAllLocationItemsAsync();
            var itemsList2 = items2.FindAll(x => x.ID.Equals("521")).ToList();

            _testOutputHelper.WriteLine(itemsList1.First().Report);
            _testOutputHelper.WriteLine(itemsList2.First().Report);
            _testOutputHelper.WriteLine(itemsList1.First().ID);
            _testOutputHelper.WriteLine(itemsList2.First().ID);
            Assert.True(!itemsList1.First().Report.Equals(itemsList2.First().Report));
            await reportRestService.DeleteLocationItemAsync("521");
        }


    }
}