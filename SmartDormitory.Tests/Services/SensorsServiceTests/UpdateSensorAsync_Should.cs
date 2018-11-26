using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services;
using SmartDormitory.Tests.HelpersMethods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.Services.SensorsServiceTests
{
//    [TestClass]
//    public class UpdateSensorAsync_Should
//    {
//        [TestMethod]
//        [DataRow("Update1")]
//        [DataRow("Update2")]
//        [DataRow("Update3")]
//        public async Task Update_Sensor(string name)
//        {
//            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
//                .UseInMemoryDatabase(databaseName: "Update_Sensor_Async")
//                .Options;
            
//            // Act
//            using (var actContext = new SmartDormitaryContext(contextOptions))
//            {
//                var service = new SensorsService(actContext);

//                var sensor = actContext.Sensors.Add(TestHelpers.TestPublicSensor());
//                var entity = sensor.Entity;
//                entity.Name = name;

//                var result = await service.UpdateSensorAsync(entity);
//            }

//            // Assert
//            using (var assertContext = new SmartDormitaryContext(contextOptions))
//            {
//                var result = await assertContext.Sensors.FirstOrDefaultAsync();

//                Assert.AreEqual(name, result.Name);
//            }
//        }
//    }
}
