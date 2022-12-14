using PSI.Services;
using PSI.Models;
using PSI.States;
using System.Diagnostics;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitTests
{
    public class LogSerivceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public LogSerivceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async void TestAddItem()
        {
            LogService logService = new(new HttpClient());
            LogItem logItem = new()
            {
                ID = "123",
                Date = "2022-02-03",
                Details = "details",
                Trace = "trace"
            };

            await logService.AddLogItemAsync(logItem);
            var items = await logService.GetAllLogItemsAsync();
            var itemsList = items.FindAll(x => x.ID.Equals("123")).ToList();

            Assert.True(itemsList.Count == 1);
        }

        [Fact]
        public async void TestDeleteItem()
        {
            LogService logService = new(new HttpClient());
            LogItem logItem = new()
            {
                ID = "321",
                Date = "2022-02-03",
                Details = "details",
                Trace = "trace"
            };
            LogItem logItem2 = new()
            {
                ID = "321",
                Date = "2022-02-03",
                Details = "detail",
                Trace = "trace"
            };

            await logService.AddLogItemAsync(logItem);
            var items1 = await logService.GetAllLogItemsAsync();
            var itemsList1 = items1.FindAll(x => x.ID.Equals("321")).ToList();
            await logService.DeleteLogItemAsync("321");
            var items2 = await logService.GetAllLogItemsAsync();
            var itemsList2 = items2.FindAll(x => x.ID.Equals("321")).ToList();

            Assert.True(itemsList1.Count == 1 && itemsList2.Count == 0);
        }


    }
}