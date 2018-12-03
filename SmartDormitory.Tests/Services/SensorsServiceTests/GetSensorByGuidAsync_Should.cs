using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Context;
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
    public class GetSensorByGuidAsync_Should
    {
        [TestMethod]
        public async Task Return_Null_IfNoSensorsMatch()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Return_Null_IfNoSensorsMatch_Async")
                .Options;

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubMock = new Mock<IHubContext<NotifyHub>>();
                var service = new SensorsService(assertContext, hubMock.Object);
                var result = await service.GetSensorByGuidAsync(TestHelpers.TestGuid());

                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public async Task Return_RightSensor()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Return_RightSensor_Async")
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
                var hubMock = new Mock<IHubContext<NotifyHub>>();
                var service = new SensorsService(assertContext, hubMock.Object);
                var result = await service.GetSensorByGuidAsync(sensor.Id);

                Assert.AreEqual(sensor.Id, result.Id);
            }
        }
    }
}
