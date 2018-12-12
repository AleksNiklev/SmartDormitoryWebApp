using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitory.API.DormitaryAPI;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Services.SensorTypesServiceTests
{
    [TestClass]
    public class GetSensorTypeByIdAsync_Should
    {
        [TestMethod]
        public async Task Return_Sensor()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_Sensor_Async")
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
                var result = await service.GetSensorTypeByIdAsync(sensorType.Id);

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public async Task Return_Null_IfNoSensorsTypeMatch()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_Null_IfNoSensorsTypeMatch_Async")
                .Options;

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var sensorApiMock = new Mock<ISensorsAPI>();
                var service = new SensorTypesService(assertContext, sensorApiMock.Object);
                var result = await service.GetSensorTypeByIdAsync(TestHelpers.TestGuid());

                Assert.IsNull(result);
            }
        }
    }
}