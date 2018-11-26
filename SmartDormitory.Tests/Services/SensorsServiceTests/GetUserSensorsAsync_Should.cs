using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services;
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
        public async Task MyTestMethod() //Todo
        {
            var contextOptions = new DbContextOptionsBuilder<SmartDormitaryContext>()
                .UseInMemoryDatabase(databaseName: "Register_Sensor_Async")
                .Options;


            // Act
            using (var actContext = new SmartDormitaryContext(contextOptions))
            {

            }

            // Assert
            using (var assertContext = new SmartDormitaryContext(contextOptions))
            {

            }

        }
    }
}
