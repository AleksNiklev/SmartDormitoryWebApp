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
    public class GetSensorByGuid_Should
    {
        [TestMethod]
        public void Return_Null_IfNoSensorsMatch()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_Null_IfNoSensorsMatch")
                .Options;

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);

                Assert.IsNull(service.GetSensorByGuid(TestHelpers.TestGuid()));
            }
        }

        [TestMethod]
        public void Return_RightSensor()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_RightSensor")
                .Options;

            var sensor = TestHelpers.TestPublicSensor();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Sensors.Add(sensor);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);
                var result = service.GetSensorByGuid(sensor.Id);

                Assert.AreEqual(sensor.Id, result.Id);
            }
        }
    }
}