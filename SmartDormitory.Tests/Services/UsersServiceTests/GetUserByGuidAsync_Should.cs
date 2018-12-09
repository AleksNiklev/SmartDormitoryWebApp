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
    public class GetUserByGuidAsync_Should
    {
        [TestMethod]
        public async Task Return_User()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_User_Async")
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
                var result = await service.GetUserByGuidAsync(new Guid(user.Id));

                Assert.IsNotNull(result);
                Assert.AreEqual(user.Id, result.Id);
            }
        }

        [TestMethod]
        public async Task Return_Null_If_No_User_Matching_Guid()
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase("Return_Null_If_No_User_Matching_Guid_Async")
                .Options;

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {
                var service = new UsersService(assertContext);
                var result = await service.GetUserByGuidAsync(new Guid());

                Assert.IsNull(result);
            }
        }
    }
}