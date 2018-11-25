using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitory.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.Services.SensorsServiceTests
{
    [TestClass]
    public class GetAllPublicSensorsAsync_Should
    {
        [TestMethod]
        public async Task Return_ListOfPublicSensors()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Return_ListOfPublicSensors_Async")
                .Options;

            var publicSensor = TestHelpers.TestPublicSensor();
            var privateSensor = TestHelpers.TestPrivateSensor();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Sensors.Add(publicSensor);
                actContext.Sensors.Add(privateSensor);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new SensorsService(assertContext);
                var result = await service.GetAllPublicSensorsAsync();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(publicSensor.Id, result.First().Id);

            }
        }

        [TestMethod]
        public async Task Return_EmptyList()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Return_EmptyList_Async")
                .Options;

            var privateSensor = TestHelpers.TestPrivateSensor();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Sensors.Add(privateSensor);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new SensorsService(assertContext);
                var result = await service.GetAllPublicSensorsAsync();

                Assert.AreEqual(0, result.Count);
            }
        }
    }
}
