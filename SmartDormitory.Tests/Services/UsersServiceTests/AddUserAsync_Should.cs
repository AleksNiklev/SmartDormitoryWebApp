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
    public class AddUserAsync_Should
    {
        [TestMethod]
        public async Task Add_User()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Add_User_Async")
                .Options;

            var user = TestHelpers.TestUser1();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new UsersService(actContext);
                await service.AddUserAsync(user);
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var users = assertContext.Users;
                Assert.AreEqual(1, users.Count());
                Assert.IsTrue(users.Contains(user));
            }
        }

        [TestMethod]
        public async Task Register_AddedUser()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Register_AddedUser_Async")
                .Options;

            var user = TestHelpers.TestUser1();

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new UsersService(assertContext);
                var result = await service.AddUserAsync(user);

                Assert.AreEqual(user.UserName, result.Entity.UserName);
            }
        }
    }
}