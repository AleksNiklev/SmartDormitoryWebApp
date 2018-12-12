using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitary.Data.Context;
using SmartDormitary.Services;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Services.UsersServiceTests
{
    [TestClass]
    public class UpdateUserAsync_Should
    {
        [TestMethod]
        public async Task Update_User()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Update_User_Async")
                .Options;

            var user = TestHelpers.TestUser1();
            var name = "Update";

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
                var toUpdate = await assertContext.Users.SingleAsync();

                toUpdate.UserName = name;
                var result = await service.UpdateUserAsync(toUpdate);

                Assert.AreEqual(name, result.Entity.UserName);
            }
        }
    }
}