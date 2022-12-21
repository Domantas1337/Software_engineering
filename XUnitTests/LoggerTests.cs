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
using PSI.Logging;

namespace XUnitTests
{
    [Collection("Our Test Collection #1")]
    public class LoggerTests
    {

        private readonly ITestOutputHelper _testOutputHelper;
        public LoggerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }


        [Fact]
        public async void LogAsync_ExceptionWritten_ReadTheSameException()
        {
            string exceptionMsg = "exception thrown";
            string tempPath = Path.GetTempFileName();
            Exception ex = new(message: exceptionMsg);

            await Logger.LogAsync(ex, diffPath: tempPath);
            var result = JSONManager.Read<LogItem>(tempPath);

            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.True(result.First().Details == exceptionMsg);
        }

        [Fact]
        public async void LogAsync_ExceptionWrittenWithExtraMsg_ReadTheSameException()
        {
            string exceptionMsg = "exception thrown";
            string extraMsg = "extra message";
            string tempPath = Path.GetTempFileName();
            Exception ex = new(message: exceptionMsg);

            await Logger.LogAsync(ex, extraMsg: extraMsg, diffPath: tempPath);
            var result = JSONManager.Read<LogItem>(tempPath);

            Assert.NotNull(result);
            Assert.True(result.Count == 1);
            Assert.True(result.First().Details == extraMsg);
        }

        [Fact]
        public async void SendLogs_SentTwoLogs_TwoExpectedLogsSent()
        {
            var logServiceMock = new Mock<ILogService>();
            logServiceMock.Setup((obj) => obj.AddLogItemAsync(It.IsAny<LogItem>()));
            string tempPath = Path.GetTempFileName();
            Exception ex1 = new(message: "first msg");
            Exception ex2 = new(message: "second msg");

            await Logger.LogAsync(ex1, diffPath: tempPath);
            await Logger.LogAsync(ex2, diffPath: tempPath);
            Logger.SendLogs(logServiceMock.Object, diffFromPath: tempPath);
            try
            {
                logServiceMock.Verify((obj) => obj.AddLogItemAsync(It.IsAny<LogItem>()), Times.Exactly(2));
            }
            catch (MockException)
            {
                Assert.True(false);
            }
            Assert.True(true);
        }

        [Fact]
        public async void SendLogs_SentNoLogs_NoLogsSentExpected()
        {
            var logServiceMock = new Mock<ILogService>();
            logServiceMock.Setup((obj) => obj.AddLogItemAsync(It.IsAny<LogItem>()));
            string tempPath = Path.GetTempFileName();

            Logger.SendLogs(logServiceMock.Object, diffFromPath: tempPath);
            try
            {
                logServiceMock.Verify((obj) => obj.AddLogItemAsync(It.IsAny<LogItem>()), Times.Exactly(0));
            }
            catch (MockException)
            {
                Assert.True(false);
            }
            Assert.True(true);
        }

        [Fact]
        public async void GetLogs_CallsLogService_LogServiceCalled()
        {
            List<LogItem> logItems = new()
            {
                new LogItem()
                {
                    ID = IDGenerator.GenerateID(),
                    Date = DateTime.UtcNow.ToString(),
                    Details = "log details",
                    Trace = "log trace"
                }
            };
            var logServiceMock = new Mock<ILogService>();
            logServiceMock.Setup((obj) => obj.GetAllLogItemsAsync()).ReturnsAsync(logItems);

            var result = await Logger.GetLogs(logServiceMock.Object);

            Assert.True(result.Count == 1);
            Assert.True(result.First().ID == logItems.First().ID);
        }
    }
}
