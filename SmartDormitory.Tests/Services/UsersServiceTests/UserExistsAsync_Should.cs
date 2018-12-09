using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitory.API.DormitaryAPI;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Services.UsersServiceTests
{
    [TestClass]
    public class UserExistsAsync_Should
    {
        [TestMethod]
        public async Task Return_UserExists()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_UserExists_Async")
                .Options;

            var user = TestHelpers.TestUser1();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Users.Add(user);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new UsersService(assertContext);
                var result = await service.UserExistsAsync(new Guid(user.Id));

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public async Task Return_User_DoesntExists()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_User_DoesntExists_Async")
                .Options;

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new UsersService(assertContext);
                var result = await service.UserExistsAsync(new Guid());

                Assert.IsFalse(result);
            }
        }
    }
}