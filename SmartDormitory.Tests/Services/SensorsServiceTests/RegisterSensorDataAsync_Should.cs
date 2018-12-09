using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitary.Services.Hubs.Service;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Services.SensorsServiceTests
{
    [TestClass]
    public class RegisterSensorDataAsync_Should
    {
        [TestMethod]
        public async Task Register_SensorData()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Register_SensorData_Async")
                .Options;

            var sensorData = TestHelpers.TestSensorData();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(actContext, hubServiceMock.Object);
                await service.RegisterSensorDataAsync(sensorData);
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var sensorDatas = assertContext.SensorData;
                Assert.AreEqual(1, sensorDatas.Count());
                Assert.IsTrue(sensorDatas.Contains(sensorData));
            }
        }
    }
}