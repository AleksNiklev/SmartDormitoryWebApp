using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitory.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartDormitory.Tests.Services.SensorsServiceTests
{
    [TestClass]
    public class GetSensorByGuid_Should
    {
        [TestMethod]
        public void Return_Null_IfNoSensorsMatch()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Return_Null_IfNoSensorsMatch")
                .Options;

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new SensorsService(assertContext);

                Assert.IsNull(service.GetSensorByGuid(TestHelpers.TestGuid()));
            }
        }

        [TestMethod]
        public void Return_RightSensor()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Return_RightSensor")
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
                var service = new SensorsService(assertContext);
                var result = service.GetSensorByGuid(sensor.Id);

                Assert.AreEqual(sensor.Id, result.Id);
            }
        }
    }
}
