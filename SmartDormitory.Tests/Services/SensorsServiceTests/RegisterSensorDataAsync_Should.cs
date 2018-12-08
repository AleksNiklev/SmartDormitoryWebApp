using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services;
using SmartDormitary.Services.Hubs.Service;
using SmartDormitory.Tests.HelpersMethods;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.Services.SensorsServiceTests
{
    [TestClass]
    public class RegisterSensorDataAsync_Should
    {
        [TestMethod]
        public async Task Register_SensorData()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Register_SensorData_Async")
                .Options;
            var sensorData = new SensorData() { Id = 1, Value = "1", Timestamp = new DateTime(2018, 12, 12) };
            
            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(actContext, hubServiceMock.Object);
                await service.RegisterSensorDataAsync(sensorData);
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                Assert.AreEqual(1, assertContext.SensorData.Count());
                Assert.AreEqual(sensorData.Value, assertContext.SensorData.SingleOrDefault().Value);
            }
        }
    }
}