using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                var service = new SensorsService(assertContext);

                Assert.AreEqual(0, service.GetAllSensors().Count);
            }
        }


        [TestMethod]
        public void Return_AllSensors()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Return_AllSensors")
                .Options;
            var sensorType = new SensorType(){ Id = Guid.Parse("00000000-0000-0000-0000-000000000000") };
            var user = new User(){ Id = "00000000-0000-0000-0000-000000000000" };
            var sensor = new Sensor()
            {
                Name = "test",
                Description = "test",
                MaxAcceptableValue = 1,
                MinAcceptableValue = 0,
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                IsPublic = true,
                TickOff = true,
                SensorTypeId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                SensorType = sensorType,
                Latitude = 41.1231,
                Longitude = 41.1231,
                RefreshTime = 40,
                Value = "123",
                Timestamp = new DateTime(),
                User = user
            };
            

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Sensors.Add(sensor);
                actContext.SaveChanges();
                var service = new SensorsService(actContext);
                var result = service.GetAllSensors().Count;

            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new SensorsService(assertContext);
                var result = service.GetAllSensors().Count;

                Assert.AreEqual(1, result);
                Assert.AreEqual("test", service.GetAllSensors().First().Name);
            }
        }
    }
}
