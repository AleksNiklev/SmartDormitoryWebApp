using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services;
using SmartDormitary.Services.Hubs;
using SmartDormitory.Tests.HelpersMethods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.Services.SensorsServiceTests
{
    [TestClass]
    public class UpdateSensorAsync_Should
    {
        [TestMethod]
        public async Task Update_Sensor()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Update_Sensor_Async")
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
                var hubMock = new Mock<IHubContext<NotifyHub>>();
                var service = new SensorsService(assertContext, hubMock.Object);
                var toUbdate = await assertContext.Sensors.SingleAsync();

                toUbdate.Name = name;
                var result = await service.UpdateSensorAsync(toUbdate);

                Assert.AreEqual(name, result.Entity.Name);
            }
        }
    }
}
