using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitary.Services.Hubs.Service;
using SmartDormitory.API.DormitaryAPI;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Services.SensorTypesServiceTests
{
    [TestClass]
    public class GetAllSensorTypesAsync_Should
    {
        [TestMethod]
        public async Task Return_ListOfPublicSensors()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_ListOfAllSensorTypes_Async")
                .Options;

            var sensorType = TestHelpers.TestSensorType();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.SensorTypes.Add(sensorType);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var sensorApiMock = new Mock<ISensorsAPI>();
                var service = new SensorTypesService(assertContext, sensorApiMock.Object);
                var result = await service.GetAllSensorTypesAsync();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(sensorType.Id, result.First().Id);
            }
        }
    }
}