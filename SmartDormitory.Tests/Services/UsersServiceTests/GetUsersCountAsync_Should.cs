using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Services.UsersServiceTests
{
    [TestClass]
    public class GetUsersCountAsync_Should
    {
        [TestMethod]
        public async Task Return_UsersCount()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_UsersCount_Async")
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
                var result = await service.GetUsersCountAsync();

                Assert.AreEqual(1, result);
            }
        }
    }
}