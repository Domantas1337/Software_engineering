using PSI.Models;
using PSI.Services;
using PSI.Views.ManageLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace XUnitTests
{
    [Collection("Our Test Collection #1")]
    public class AddLocationViewTests
    {

        private readonly ITestOutputHelper _testOutputHelper;
        public AddLocationViewTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async void Integration_AddedCorrectItem_SameItemReceived()
        {
            LocationService locationService = new(new HttpClient());
            AddLocationView locationViewModel = new(locationService);

            locationViewModel.LongitudeText = "12";
            locationViewModel.City = "adfsd1234322";
            locationViewModel.Street = "sdfres12dfds35asda";
            locationViewModel.LatitudeText = "43";


            locationViewModel.OnSaveButtonClicked(this, new EventArgs());
            
            var items = await locationService.GetAllLocationItemsAsync();
            var itemsList = items.FindAll(x => x.City.Equals("adfsd1234322")).ToList();
            foreach(LocationItem item in items)
            {
                _testOutputHelper.WriteLine(item.ID + "\n" + item.City);
            }

            _testOutputHelper.WriteLine(locationViewModel.ErrorBody);

            foreach (LocationItem item in itemsList)
            {
                await locationService.DeleteLocationItemAsync(item.ID);
                _testOutputHelper.WriteLine(item.ID);
            }
            Assert.True(itemsList.Count() == 1);
        }

        [Fact]
        public async void Integration_NoLatitudeProvided_NoItemReceived()
        {
            LocationService locationService = new(new HttpClient());
            AddLocationView locationViewModel = new(locationService);

            locationViewModel.Title = "title";
            locationViewModel.LongitudeText = "12";
            locationViewModel.City = "adfsd12343221";
            locationViewModel.Street = "sdfres12dfds35asda";
            locationViewModel.OnSaveButtonClicked(this, new EventArgs());


            var items = await locationService.GetAllLocationItemsAsync();
            var itemsList = items.FindAll(x => x.City.Equals("adfsd12343221")).ToList();

            foreach (LocationItem item in itemsList)
            {
                await locationService.DeleteLocationItemAsync(item.ID);
                _testOutputHelper.WriteLine(item.ID);

            }

            _testOutputHelper.WriteLine(locationViewModel.ErrorBody);
            _testOutputHelper.WriteLine(itemsList.Count().ToString());

            Assert.True(itemsList.Count() == 0);
        }

        [Fact]
        public void Integration_NoCityProvided_NoItemReceived()
        {
            LocationService locationService = new(new HttpClient());
            AddLocationView locationViewModel = new(locationService);

            locationViewModel.Title = "title";
            locationViewModel.LongitudeText = "12";
            locationViewModel.LatitudeText = "12";
            locationViewModel.City = "";
            locationViewModel.Street = "sdfres12dfds35asda";
            locationViewModel.OnSaveButtonClicked(this, new EventArgs());

            _testOutputHelper.WriteLine(locationViewModel.ErrorBody);
            Assert.True(locationViewModel.ErrorBody == "Invalid:\n\n* city\n");
        }

        [Fact]
        public void Integration_NoStreetProvided_NoItemReceived()
        {
            LocationService locationService = new(new HttpClient());
            AddLocationView locationViewModel = new(locationService);



            locationViewModel.Title = "title";
            locationViewModel.LongitudeText = "12";
            locationViewModel.LatitudeText = "12";
            locationViewModel.City = "city";
            locationViewModel.Street = "";
            locationViewModel.OnSaveButtonClicked(this, new EventArgs());

            _testOutputHelper.WriteLine(locationViewModel.ErrorBody);
            Assert.True(locationViewModel.ErrorBody == "Invalid:\n\n* street\n");
        }

        [Fact]
        public void Integration_InvalidLatitudeProvided_NoItemReceived()
        {
            LocationService locationService = new(new HttpClient());
            AddLocationView locationViewModel = new(locationService);



            locationViewModel.Title = "title";
            locationViewModel.LongitudeText = "12";
            locationViewModel.LatitudeText = "b1a2b";
            locationViewModel.City = "cities";
            locationViewModel.Street = "abcdef";
            locationViewModel.OnSaveButtonClicked(this, new EventArgs());

            _testOutputHelper.WriteLine(locationViewModel.ErrorBody);
            Assert.True(locationViewModel.ErrorBody == "Invalid:\n\n* latitude\n");
        }

        [Fact]
        public void Integration_InvalidLongitudeProvided_NoItemReceived()
        {
            LocationService locationService = new(new HttpClient());
            AddLocationView locationViewModel = new(locationService);



            locationViewModel.Title = "title";
            locationViewModel.LongitudeText = "a12";
            locationViewModel.LatitudeText = "12";
            locationViewModel.City = "cities";
            locationViewModel.Street = "abcdef";
            locationViewModel.OnSaveButtonClicked(this, new EventArgs());

            _testOutputHelper.WriteLine(locationViewModel.ErrorBody);
            Assert.True(locationViewModel.ErrorBody == "Invalid:\n\n* longitude\n");
        }

        [Fact]
        public async void Integration_NoLongitudeProvided_NoItemReceived()
        {
            LocationService locationService = new(new HttpClient());
            AddLocationView locationViewModel = new(locationService);

            locationViewModel.Title = "title";
            locationViewModel.LatitudeText = "12";
            locationViewModel.City = "adfsd12343223";
            locationViewModel.Street = "sdfres12dfds35asda";
            locationViewModel.OnSaveButtonClicked(this, new EventArgs());

            var items = await locationService.GetAllLocationItemsAsync();
            var itemsList = items.FindAll(x => x.City.Equals("adfsd12343223")).ToList();

            foreach (LocationItem item in itemsList)
            {
                await locationService.DeleteLocationItemAsync(item.ID);
                _testOutputHelper.WriteLine(item.ID);

            }

            _testOutputHelper.WriteLine(locationViewModel.ErrorBody);
            Assert.True(itemsList.Count() == 0);
        }
    }

}
