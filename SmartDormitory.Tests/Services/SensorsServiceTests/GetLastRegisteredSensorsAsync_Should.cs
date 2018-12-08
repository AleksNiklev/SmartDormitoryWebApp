using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitary.Services.Hubs.Service;
using SmartDormitory.Tests.HelpersMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.Services.SensorsServiceTests
{
    [TestClass]
    public class GetLastRegisteredSensorsAsync_Should
    {
        [TestMethod]
        [DataRow(11)]
        [DataRow(15)]
        [DataRow(20)]
        public async Task Return_Ten_Sensors(int count)
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_Ten_Sensors_" + count + "_Async")
                .Options;

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                for (int i = 1; i <= count; i++)
                {
                    var sensor = TestHelpers.TestPublicSensorWhitoutId();
                    sensor.SensorData.Timestamp = new DateTime(2018, 12, i);
                    await actContext.Sensors.AddAsync(sensor);
                }

                await actContext.SaveChangesAsync();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);

                var result = await service.GetLastRegisteredSensorsAsync();

                Assert.AreEqual(10, result.Count());
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(11)]
        [DataRow(15)]
        [DataRow(20)]
        public async Task Return_Requested_Sensors(int count)
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_Requested_Sensors_" + count + "_Async")
                .Options;

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                for (int i = 1; i <= count; i++)
                {
                    var sensor = TestHelpers.TestPublicSensorWhitoutId();
                    sensor.SensorData.Timestamp = new DateTime(2018, 12, i);
                    await actContext.Sensors.AddAsync(sensor);
                }

                await actContext.SaveChangesAsync();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);

                var result = await service.GetLastRegisteredSensorsAsync(count);

                Assert.AreEqual(count, result.Count());
            }
        }

        [TestMethod]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(9)]
        [DataRow(15)]
        [DataRow(25)]
        public async Task Return_OrderedByDesending_SensorList(int count)
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_OrderedByDesending_SensorList_" + count + "_Async")
                .Options;

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                for (int i = 1; i <= count; i++)
                {
                    var sensor = TestHelpers.TestPublicSensorWhitoutId();
                    sensor.SensorData.Timestamp = new DateTime(2018, 12, i);
                    await actContext.Sensors.AddAsync(sensor);
                }

                await actContext.SaveChangesAsync();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);
                var orderedSensors = await assertContext.Sensors
                    .OrderByDescending(s => s.SensorData.Timestamp)
                    .Include(s => s.SensorData)
                    .Take(10)
                    .ToListAsync();

                var result = await service.GetLastRegisteredSensorsAsync(count);
                for (int i = 0; i < orderedSensors.Count; i++)
                {
                    Assert.AreEqual(orderedSensors[i].SensorData.Timestamp, result[i].SensorData.Timestamp);
                }
            }
        }
    }
}
