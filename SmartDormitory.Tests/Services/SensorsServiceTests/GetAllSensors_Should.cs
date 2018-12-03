using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitary.Services.Hubs;
using SmartDormitory.Tests.HelpersMethods;
using System.Linq;

namespace SmartDormitory.Tests.Services.SensorsServiceTests
{
    [TestClass]
    public class GetAllSensors_Should
    {
        [TestMethod]
        public void Return_EmptySensorList()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Return_EmptySensorList")
                .Options;
            
            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubMock = new Mock<IHubContext<NotifyHub>>();
                var service = new SensorsService(assertContext, hubMock.Object);

                Assert.AreEqual(0, service.GetAllSensors().Count);
            }
        }


        [TestMethod]
        public void Return_AllSensors()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Return_AllSensors")
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
                var result = service.GetAllSensors();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("test", result.First().Name);
            }
        }
    }
}
