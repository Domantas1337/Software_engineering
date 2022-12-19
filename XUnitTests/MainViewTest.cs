using NSubstitute;
using PSI;
using PSI.Services;
using PSI.ViewModels;
using PSI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitTests
{
    public class MainViewTest : MainView
    {
        ITestOutputHelper _testOutputHelper;
        public MainViewTest(ITestOutputHelper testOutputHelper) : base(new ReportViewModel(new ReportRestService(new HttpClient())),
                                     new LocationService(new HttpClient()),
                                     new LogService(new HttpClient()))
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void OnAppearingTest()
        {
            OnAppearing();

            _testOutputHelper.WriteLine(locations.Value.ToString());
            Assert.True(locations.IsValueCreated);
        }

        [Fact]
        public void OnNewLocation()
        {

        }

    }
}
