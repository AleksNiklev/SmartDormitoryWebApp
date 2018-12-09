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
    public class GetLastRegisteredSensorsAsync_Should
    {
        [TestMethod]
        public async Task Return_ListOfLastSensors()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_ListOfLastSensors_Async")
                .Options;

            var sensor1 = TestHelpers.TestPrivateSensor();
            var sensor2 = TestHelpers.TestPublicSensor();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Sensors.Add(sensor1);
                actContext.Sensors.Add(sensor2);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);
                var result = await service.GetLastRegisteredSensorsAsync();

                Assert.AreEqual(2, result.Count);
            }
        }

        [TestMethod]
        public async Task Return_ListOfLastSensors_DescendingOrder()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_ListOfLastSensors_DescendingOrder_Async")
                .Options;

            var sensor1 = TestHelpers.TestPrivateSensor();
            var sensor2 = TestHelpers.TestPublicSensor();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Sensors.Add(sensor1);
                actContext.Sensors.Add(sensor2);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);
                var result = await service.GetLastRegisteredSensorsAsync();

                Assert.AreEqual(sensor2.Id, result[0].Id);
                Assert.AreEqual(sensor1.Id, result[1].Id);
            }
        }
    }
}