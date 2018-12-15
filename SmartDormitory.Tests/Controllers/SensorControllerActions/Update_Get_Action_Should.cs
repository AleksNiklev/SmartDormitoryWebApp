using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Controllers;
using SmartDormitary.Data.Models;
using SmartDormitary.Models.SensorViewModels;
using SmartDormitary.Services.Contracts;
using SmartDormitory.API.DormitaryAPI;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Controllers.SensorControllerActions
{
    [TestClass]
    public class Update_Get_Action_Should
    {
        [TestMethod]
        public async Task Return_UpdateView()
        {
            var sensorTypesService = new Mock<ISensorTypesService>();
            var sensorsService = new Mock<ISensorsService>();
            var sensorsApi = new Mock<ISensorsAPI>();
            var mockUserManager = TestHelpers.GetTestUserManager();
            var testSensor = TestHelpers.TestPublicSensor();

            sensorsService.Setup(s => s.GetSensorByGuidAsync(It.IsAny<Guid>())).ReturnsAsync(testSensor);

            var controler = new SensorController(sensorTypesService.Object, sensorsService.Object,
                mockUserManager.Object, sensorsApi.Object);

            var result = await controler.Update(testSensor.Id) as ViewResult;

            Assert.AreEqual("Update", result.ViewName);
        }

        [TestMethod]
        public async Task Return_Sensor_AsRegisterSensorViewModel()
        {
            var sensorTypesService = new Mock<ISensorTypesService>();
            var sensorsService = new Mock<ISensorsService>();
            var userStoreMock = new Mock<IUserStore<User>>();
            var sensorsApi = new Mock<ISensorsAPI>();
            var mockUserManager = TestHelpers.GetTestUserManager();
            var testSensor = TestHelpers.TestPublicSensor();

            sensorsService.Setup(s => s.GetSensorByGuidAsync(It.IsAny<Guid>())).ReturnsAsync(testSensor);

            var controler = new SensorController(sensorTypesService.Object, sensorsService.Object,
                mockUserManager.Object, sensorsApi.Object);

            var result = await controler.Update(testSensor.Id) as ViewResult;
            var viewModel = (RegisterSensorViewModel) result.ViewData.Model;

            Assert.AreEqual(testSensor.Id, viewModel.Id);
        }
    }
}