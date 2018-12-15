using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Controllers.HomeControllerActions
{
    [TestClass]
    public class Index_Action_Should
    {
        [TestMethod]
        public async Task Return_IndexView()
        {
            var sensorTypesServiceMock = new Mock<ISensorTypesService>();
            var sensorsServiceMock = new Mock<ISensorsService>();
            var userManagerMock = TestHelpers.GetTestUserManager();

            sensorsServiceMock.Setup(s => s.GetAllPublicSensorsAsync()).ReturnsAsync(new List<Sensor>
            {
                TestHelpers.TestPublicSensor(),
                TestHelpers.TestPrivateSensor()
            });

            userManagerMock.Setup(s => s.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(TestHelpers.TestUser1().Id);

            var controller = TestHelpers.GetHomeController(sensorTypesServiceMock.Object, sensorsServiceMock.Object,
                userManagerMock.Object);

            var result = await controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }
    }
}