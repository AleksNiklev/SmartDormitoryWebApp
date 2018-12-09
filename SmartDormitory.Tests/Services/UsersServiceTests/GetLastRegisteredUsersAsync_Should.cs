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
    public class GetLastRegisteredUsersAsync_Should
    {
        [TestMethod]
        public async Task Return_ListOfLastUsers()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_ListOfLastUsers_Async")
                .Options;

            var user1 = TestHelpers.TestUser1();
            var user2 = TestHelpers.TestUser2();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Users.Add(user1);
                actContext.Users.Add(user2);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new UsersService(assertContext);
                var result = await service.GetLastRegisteredUsersAsync();

                Assert.AreEqual(2, result.Count);
            }
        }

        [TestMethod]
        public async Task Return_ListOfLastUsers_DescendingOrder()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_ListOfLastUsers_DescendingOrder_Async")
                .Options;

            var user1 = TestHelpers.TestUser1();
            var user2 = TestHelpers.TestUser2();

            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {
                actContext.Users.Add(user1);
                actContext.Users.Add(user2);
                actContext.SaveChanges();
            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new UsersService(assertContext);
                var result = await service.GetLastRegisteredUsersAsync();

                Assert.AreEqual(user2.Id, result[0].Id);
                Assert.AreEqual(user1.Id, result[1].Id);
            }
        }
    }
}