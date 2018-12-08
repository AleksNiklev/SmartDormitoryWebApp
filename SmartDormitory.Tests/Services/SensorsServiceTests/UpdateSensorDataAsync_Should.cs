using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitary.Services.Hubs.Service;
using SmartDormitory.Tests.HelpersMethods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.Services.SensorsServiceTests
{
    [TestClass]
    public class UpdateSensorDataAsync_Should
    {
        [TestMethod]
        public async Task Update_SensorData_Value()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Update_SensorData_Value")
                .Options;

            var sensor = TestHelpers.TestPublicSensor();
            var val = "Update";

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
                hubServiceMock.Setup(s => s.Notify(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

                var service = new SensorsService(assertContext, hubServiceMock.Object);
                var toUbdate = await assertContext.Sensors
                    .Include(s => s.SensorType)
                    .Include(s => s.SensorData).SingleAsync();

                toUbdate.SensorData.Value = val;
                var result = await service.UpdateSensorDataAsync(toUbdate);

                Assert.AreEqual(val, result.Entity.Value);
            }

        }
    }
}
