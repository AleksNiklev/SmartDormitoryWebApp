using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services;
using SmartDormitory.Tests.HelpersMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.Services.SensorsServiceTests
{
    [TestClass]
    public class RegisterSensorAsync_Should
    {
        [TestMethod]
        public async Task Register_Sensor()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Register_Sensor_Async")
                .Options;

            var sensor1 = TestHelpers.TestPublicSensor();
            var sensor2 = TestHelpers.TestPrivateSensor();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new SensorsService(actContext);
                await service.RegisterSensorAsync(sensor1);
                await service.RegisterSensorAsync(sensor2);
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var sensors = assertContext.Sensors;
                Assert.AreEqual(2, sensors.Count());
                Assert.IsTrue(sensors.Contains<Sensor>(sensor1));
                Assert.IsTrue(sensors.Contains<Sensor>(sensor2));
            }
        }

        [TestMethod]
        public async Task Register_AddedSensor()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Register_AddedSensor_Async")
                .Options;

            var sensor = TestHelpers.TestPublicSensor();
            
            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new SensorsService(assertContext);
                var result = await service.RegisterSensorAsync(sensor);

                Assert.AreEqual(sensor.Name, result.Entity.Name);
            }
        }
    }
}
