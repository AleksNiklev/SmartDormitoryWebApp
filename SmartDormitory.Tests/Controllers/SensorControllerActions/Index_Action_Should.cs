using System.Collections.Generic;
using System.Linq;
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
    public class Index_Action_Should
    {
        [TestMethod]
        public async Task Return_IndexView()
        {
            var sensorTypesService = new Mock<ISensorTypesService>();
            var sensorsService = new Mock<ISensorsService>();
            var userStoreMock = new Mock<IUserStore<User>>();
            var sensorsApi = new Mock<ISensorsAPI>();
            var mockUserManager = TestHelpers.GetTestUserManager();

            sensorTypesService.Setup(s => s.GetAllSensorTypesAsync())
                .ReturnsAsync(new List<SensorType> {TestHelpers.TestSensorType()});

            var controler = new SensorController(sensorTypesService.Object, sensorsService.Object,
                mockUserManager.Object, sensorsApi.Object);

            var result = await controler.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public async Task Return_ListOfAll_SensorTypes_AsSensorTypeViewModel()
        {
            var sensorTypesService = new Mock<ISensorTypesService>();
            var sensorsService = new Mock<ISensorsService>();
            var userStoreMock = new Mock<IUserStore<User>>();
            var sensorsApi = new Mock<ISensorsAPI>();
            var mockUserManager = TestHelpers.GetTestUserManager();

            sensorTypesService.Setup(s => s.GetAllSensorTypesAsync())
                .ReturnsAsync(new List<SensorType> {TestHelpers.TestSensorType()});

            var controler = new SensorController(sensorTypesService.Object, sensorsService.Object,
                mockUserManager.Object, sensorsApi.Object);

            var result = await controler.Index() as ViewResult;
            var viewModel = (IEnumerable<SensorTypeViewModel>) result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Count());
        }
    }
}