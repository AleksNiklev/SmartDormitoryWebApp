using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartDormitary.Controllers;
using SmartDormitary.Services.Contracts;
using SmartDormitory.API.DormitaryAPI;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Controllers.SensorControllerActions
{
    [TestClass]
    public class GetSensorById_Action_Should
    {
        [TestMethod]
        public async Task Return_Sensor_JsonResult()
        {
            var sensorTypesService = new Mock<ISensorTypesService>();
            var sensorsService = new Mock<ISensorsService>();
            var sensorsApi = new Mock<ISensorsAPI>();
            var mockUserManager = TestHelpers.GetTestUserManager();
            var testSensor = TestHelpers.TestPublicSensor();

            sensorsService.Setup(s => s.GetSensorByGuidAsync(It.IsAny<Guid>())).ReturnsAsync(testSensor);

            var controller = new SensorController(sensorTypesService.Object, sensorsService.Object,
                mockUserManager.Object, sensorsApi.Object);

            var result = await controller.GetSensorById(testSensor.Id);

            var data = result.Value;

            Assert.AreEqual(testSensor.SensorData.Value, data.GetType().GetProperty("value").GetValue(data, null));
        }

        [TestMethod]
        public async Task Invoce_GetSensorByGuidAsync_FromSensorService()
        {
            var sensorTypesService = new Mock<ISensorTypesService>();
            var sensorsService = new Mock<ISensorsService>();
            var sensorsApi = new Mock<ISensorsAPI>();
            var mockUserManager = TestHelpers.GetTestUserManager();
            var testSensor = TestHelpers.TestPublicSensor();

            sensorsService.Setup(s => s.GetSensorByGuidAsync(It.IsAny<Guid>())).ReturnsAsync(testSensor);

            var controller = new SensorController(sensorTypesService.Object, sensorsService.Object,
                mockUserManager.Object, sensorsApi.Object);

            await controller.GetSensorById(testSensor.Id);

            sensorsService.Verify(s => s.GetSensorByGuidAsync(testSensor.Id), Times.Once());
        }
    }
}