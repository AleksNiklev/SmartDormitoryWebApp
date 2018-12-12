using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Services.UsersServiceTests
{
    [TestClass]
    public class GetAllUsersAsync_Should
    {
        [TestMethod]
        public async Task Return_ListOfUsers()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_ListOfUsers_Async")
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
                var result = await service.GetAllUsersAsync();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual(user.Id, result.First().Id);
            }
        }
    }
}