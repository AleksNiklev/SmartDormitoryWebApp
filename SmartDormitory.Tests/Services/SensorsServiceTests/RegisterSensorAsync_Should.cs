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
    public class RegisterSensorAsync_Should
    {
        [TestMethod]
        public async Task Register_Sensor()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Register_Sensor_Async")
                .Options;

            var sensor1 = TestHelpers.TestPublicSensor();
            var sensor2 = TestHelpers.TestPrivateSensor();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(actContext, hubServiceMock.Object);
                await service.RegisterSensorAsync(sensor1);
                await service.RegisterSensorAsync(sensor2);
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var sensors = assertContext.Sensors;
                Assert.AreEqual(2, sensors.Count());
                Assert.IsTrue(sensors.Contains(sensor1));
                Assert.IsTrue(sensors.Contains(sensor2));
            }
        }

        [TestMethod]
        public async Task Register_AddedSensor()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Register_AddedSensor_Async")
                .Options;

            var sensor = TestHelpers.TestPublicSensor();

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);
                var result = await service.RegisterSensorAsync(sensor);

                Assert.AreEqual(sensor.Name, result.Entity.Name);
            }
        }
    }
}