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
    public class GetSensorCountAsync_Should
    {
        [TestMethod]
        public async Task Return_Integer()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_Integer_Async")
                .Options;

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                await actContext.Sensors.AddAsync(TestHelpers.TestPublicSensor());
                await actContext.SaveChangesAsync();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);

                var result = await service.GetSensorCountAsync();
                Assert.IsInstanceOfType(result, typeof(int));
            }
        }

        [TestMethod]
        public async Task Return_CountOfAllSensors()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_CountOfAllSensors")
                .Options;

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                await actContext.Sensors.AddAsync(TestHelpers.TestPublicSensor());
                await actContext.Sensors.AddAsync(TestHelpers.TestPrivateSensor());
                await actContext.SaveChangesAsync();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);

                var result = await service.GetSensorCountAsync();
                Assert.AreEqual(2, result);
            }
        }

        [TestMethod]
        public async Task Return_Zero_IfNoSensors()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_Zero_IfNoSensors")
                .Options;
            
            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);

                var result = await service.GetSensorCountAsync();
                Assert.AreEqual(0, result);
            }

        }
    }
}
