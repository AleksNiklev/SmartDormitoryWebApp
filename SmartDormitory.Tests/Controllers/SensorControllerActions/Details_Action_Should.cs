using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Controllers;
using SmartDormitary.Models.SensorViewModels;
using SmartDormitary.Services.Contracts;
using SmartDormitory.API.DormitaryAPI;
using SmartDormitory.Tests.HelpersMethods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartDormitory.Tests.Controllers.SensorControllerActions
{
    [TestClass]
    public class Details_Action_Should
    {
        [TestMethod]
        public async Task Return_DetailsView()
        {
            var sensorTypesService = new Mock<ISensorTypesService>();
            var sensorsService = new Mock<ISensorsService>();
            var sensorsApi = new Mock<ISensorsAPI>();
            var mockUserManager = TestHelpers.GetTestUserManager();
            var testSensor = TestHelpers.TestPublicSensor();

            sensorsService.Setup(s => s.GetSensorByGuidAsync(It.IsAny<Guid>())).
                ReturnsAsync(testSensor);

            var controler = new SensorController(sensorTypesService.Object, sensorsService.Object, mockUserManager.Object, sensorsApi.Object);

            var result = await controler.Details(testSensor.Id) as ViewResult;

            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public async Task Return_Sensor_AsSensorViewModel()
        {
            var sensorTypesService = new Mock<ISensorTypesService>();
            var sensorsService = new Mock<ISensorsService>();
            var sensorsApi = new Mock<ISensorsAPI>();
            var mockUserManager = TestHelpers.GetTestUserManager();
            var testSensor = TestHelpers.TestPublicSensor();

            sensorsService.Setup(s => s.GetSensorByGuidAsync(It.IsAny<Guid>())).
                ReturnsAsync(testSensor);

            var controler = new SensorController(sensorTypesService.Object, sensorsService.Object, mockUserManager.Object, sensorsApi.Object);

            var result = await controler.Details(testSensor.Id) as ViewResult;
            var viewModel = (SensorViewModel)result.ViewData.Model;

            Assert.AreEqual(testSensor.Id, viewModel.Id);
        }

        [TestMethod]
        public async Task Redirect_ToIndex_IfInvalidId()
        {
            var sensorTypesService = new Mock<ISensorTypesService>();
            var sensorsService = new Mock<ISensorsService>();
            var sensorsApi = new Mock<ISensorsAPI>();
            var mockUserManager = TestHelpers.GetTestUserManager();
            
            var controler = new SensorController(sensorTypesService.Object, sensorsService.Object, mockUserManager.Object, sensorsApi.Object);

            var result = (RedirectToActionResult) await controler.Details(TestHelpers.TestGuid());

            Assert.AreEqual("Index", result.ActionName);
        }
    }
}
