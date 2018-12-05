using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services;
using SmartDormitary.Services.Hubs;
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
    public class GetUserSensorsAsync_Should
    {

        [TestMethod]
        public async Task Return_EmptyList_IfUser_DontHaveSensors()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Return_EmptyList_IfUser_DontHaveSensors_Async")
                .Options;

            var user = TestHelpers.TestUser1();

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);
                var result = await service.GetUserSensorsAsync(user.Id);

                Assert.AreEqual(0, result.Count);
            }
        }

        [TestMethod]
        public async Task Return_AllSensors_OfUser()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Return_AllSensors_OfUser_Async")
                .Options;

            var user1 = TestHelpers.TestUser1();
            var user2 = TestHelpers.TestUser2();

            var sensor1 = TestHelpers.TestPublicSensor();
            sensor1.Id = TestHelpers.TestGuid1();
            sensor1.User = user1;
            sensor1.UserId = user1.Id;

            var sensor2 = TestHelpers.TestPrivateSensor();
            sensor2.Id = TestHelpers.TestGuid2();
            sensor2.User = user1;
            sensor2.UserId = user1.Id;

            var sensor3 = TestHelpers.TestPublicSensor();
            sensor3.Id = TestHelpers.TestGuid3();
            sensor3.User = user2;
            sensor3.UserId = user2.Id;

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Sensors.Add(sensor1);
                actContext.Sensors.Add(sensor2);
                actContext.Sensors.Add(sensor3);
                await actContext.SaveChangesAsync();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var hubServiceMock = new Mock<IHubService>();
                var service = new SensorsService(assertContext, hubServiceMock.Object);
                var result1 = await service.GetUserSensorsAsync(user1.Id);
                var result2 = await service.GetUserSensorsAsync(user2.Id);

                Assert.AreEqual(2, result1.Count);
                Assert.AreEqual(1, result2.Count);
            }
        }
    }
}
