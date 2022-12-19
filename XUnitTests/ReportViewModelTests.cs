using PSI.Services;
using PSI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace XUnitTests
{
    [Collection("Our Test Collection #5")]

    public class ReportViewModelTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public ReportViewModelTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async void TestAddItem()
        {
            ReportRestService reportService = new(new HttpClient());
            ReportViewModel reportViewModel = new ReportViewModel(reportService);
            
            reportViewModel.Date = DateTime.Now;
            reportViewModel.Report = "This is aaaaa report";
            reportViewModel.ReportTitle = "Title";

            reportViewModel.Add();

            var items = await reportService.GetAllLocationItemsAsync();
            var itemsList = items.FindAll(x => x.Report.Equals("This is aaaaa report")).ToList();

            _testOutputHelper.WriteLine(itemsList.First().Title);
            _testOutputHelper.WriteLine(itemsList.First().Report);
            _testOutputHelper.WriteLine(itemsList.First().ID);
            _testOutputHelper.WriteLine(itemsList.Count().ToString());

            await reportService.DeleteLocationItemAsync(itemsList.First().ID);
        
            Assert.True(itemsList.Count() == 1);
        }


    }
}
