using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Controllers;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;
using SmartDormitory.API.DormitaryAPI;
using SmartDormitory.Tests.HelpersMethods;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

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

            sensorsServiceMock.Setup(s => s.GetAllPublicSensorsAsync()).
                ReturnsAsync(new List<Sensor>()
                {
                    TestHelpers.TestPublicSensor(),
                    TestHelpers.TestPrivateSensor()
                });

            userManagerMock.Setup(s => s.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(TestHelpers.TestUser1().Id);

            var controller = TestHelpers.GetHomeController(sensorTypesServiceMock.Object, sensorsServiceMock.Object, userManagerMock.Object);

            var result = await controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

    }
}
