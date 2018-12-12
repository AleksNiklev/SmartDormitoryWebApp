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
    public class GetAllPublicSensorsAsync_Should
    {
        [TestMethod]
        public async Task Return_ListOfPublicSensors()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_ListOfPublicSensors_Async")
                .Options;

            var publicSensor = TestHelpers.TestPublicSensor();
            var privateSensor = TestHelpers.TestPrivateSensor();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Sensors.Add(publicSensor);
                actContext.Sensors.Add(privateSensor);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);
                var result = await service.GetAllPublicSensorsAsync();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(publicSensor.Id, result.First().Id);
            }
        }

        [TestMethod]
        public async Task Return_EmptyList()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_EmptyList_Async")
                .Options;

            var privateSensor = TestHelpers.TestPrivateSensor();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Sensors.Add(privateSensor);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);
                var result = await service.GetAllPublicSensorsAsync();

                Assert.AreEqual(0, result.Count);
            }
        }
    }
}