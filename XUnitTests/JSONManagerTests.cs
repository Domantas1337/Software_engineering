using PSI.Models;
using PSI.Services;
using PSI.Views.ManageLocation;
using PSIAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSI.FileManagers;
using Xunit.Abstractions;
using Newtonsoft.Json;
using Moq;
using System.Reflection;
using NSubstitute.ReturnsExtensions;
using PSI.Generators;

namespace XUnitTests
{
    [Collection("Our Test Collection #1")]
    public class JSONManagerTests
    {

        private readonly ITestOutputHelper _testOutputHelper;
        public JSONManagerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }


        [Fact]
        public void DeserializeFromJSONString_NullProvided_ThrowsArgumentNullException()
        {
            string nullString = null;
            Assert.ThrowsAny<ArgumentNullException>(() =>
            {
                var locationItemDtos = JSONManager.DeserializeFromJSONString<LocationItemDto>(nullString);
            });
        }

        [Fact]
        public void DeserializeFromJSONString_NumberStringProvided_ThrowsArgumentNullException()
        {
            string numberString = "143";
            Assert.ThrowsAny<JsonSerializationException>(() =>
            {
                var locationItemDtos = JSONManager.DeserializeFromJSONString<LocationItemDto>(numberString);
            });
        }

        [Fact]
        public void DeserializeFromJSONString_EmptyStringProvided_ReturnsDefaultList()
        {
            string emptyString = "";
            var locationItemDtos = JSONManager.DeserializeFromJSONString<LocationItemDto>(emptyString);
            Assert.NotNull(locationItemDtos);
        }

        [Fact]
        public void Read_NullPathProvided_ThrowsArgumentNullException()
        {
            string nullPath = null;
            Assert.ThrowsAny<ArgumentNullException>(() =>
            {
                var locationItemDtos = JSONManager.Read<LocationItemDto>(nullPath);
            });
        }

        [Fact]
        public void Read_PathStartsWithNumber_ThrowsIOException()
        {
            string invalidPath = "2C:\\Program Files";
            Assert.ThrowsAny<IOException>(() =>
            {
                var locationItemDtos = JSONManager.Read<LocationItemDto>(invalidPath);
            });
        }

        [Fact]
        public void ReadWrite_WriteToTempReadFromTemp_SameObjectsReturned()
        {
            string path = Path.GetTempFileName();
            List<LocationItemDto> locationItemDtos = new()
            {
                new() {ID = IDGenerator.GenerateID()},
                new() {ID = IDGenerator.GenerateID()}
            };

            JSONManager.WriteAsync(path, items: locationItemDtos).Wait();
            var result = JSONManager.Read<LocationItemDto>(path);

            Assert.True(result.Count == 2);
            Assert.True(result[0].ID.Equals(locationItemDtos[0].ID));
            Assert.True(result[1].ID.Equals(locationItemDtos[1].ID));
        }

        [Fact]
        public void Read_EmptyPath_ThrowsArgumentException()
        {
            string emptyPath = "";
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var locationItemDtos = JSONManager.Read<LocationItemDto>(emptyPath);
            });
        }

        [Fact]
        public void WriteAsync_NullPathPovided_ThrowsArgumentNullException()
        {
            string nullPath = null;
            Assert.ThrowsAnyAsync<ArgumentNullException>(async () =>
            {
                await JSONManager.WriteAsync<LocationItemDto>(nullPath);
            });
        }

        [Fact]
        public void WriteAsync_PathStartsWithNumber_ThrowsIOException()
        {
            string invalidPath = "2C:\\Program Files";
            Assert.ThrowsAnyAsync<IOException>(async () =>
            {
                await JSONManager.WriteAsync<LocationItemDto>(invalidPath);
            });
        }

        [Fact]
        public void WriteAsync_EmptyPath_ThrowsArgumentException()
        {
            string emptyPath = "";
            Assert.ThrowsAnyAsync<ArgumentException>(async () =>
            {
                await JSONManager.WriteAsync<LocationItemDto>(emptyPath);
            });
        }
    }
}
