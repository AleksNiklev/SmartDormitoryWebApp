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
    public class UpdateSensorAsync_Should
    {
        [TestMethod]
        public async Task Update_Sensor()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Update_Sensor_Async")
                .Options;

            var sensor = TestHelpers.TestPublicSensor();
            var name = "Update";

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                await actContext.Sensors.AddAsync(sensor);
                await actContext.SaveChangesAsync();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                hubServiceMock
                    .Setup(s => s.Notify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                        It.IsAny<string>())).Returns(Task.CompletedTask);

                var service = new SensorsService(assertContext, hubServiceMock.Object);
                var toUbdate = await assertContext.Sensors.SingleAsync();

                toUbdate.Name = name;
                var result = await service.UpdateSensorAsync(toUbdate);

                Assert.AreEqual(name, result.Entity.Name);
            }
        }
    }
}