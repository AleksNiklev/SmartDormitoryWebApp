using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Services.UsersServiceTests
{
    [TestClass]
    public class DeleteUserSensorsAsync_Should
    {
        [TestMethod]
        public async Task Delete_User_Sensors()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Delete_User_Sensors_Async")
                .Options;

            var user = TestHelpers.TestUser1();
            var sensor1 = TestHelpers.TestPrivateSensor();
            user.Sensors.Add(sensor1);

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                await actContext.Users.AddAsync(user);
                await actContext.SaveChangesAsync();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new UsersService(assertContext);
                await service.DeleteUserSensorsAsync(new Guid(user.Id));
                var result = await assertContext.Users.SingleAsync();

                Assert.AreEqual(0, result.Sensors.Count);
            }
        }
    }
}