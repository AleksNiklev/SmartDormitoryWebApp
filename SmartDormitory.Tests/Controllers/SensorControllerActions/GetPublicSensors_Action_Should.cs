﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using SmartDormitary.Controllers;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;
using SmartDormitory.API.DormitaryAPI;
using SmartDormitory.Tests.HelpersMethods;

namespace SmartDormitory.Tests.Controllers.SensorControllerActions
{
    [TestClass]
    public class GetPublicSensors_Action_Should
    {
        [TestMethod]
        public async Task Return_Sensor_AsSensorViewModel()
        {
            var sensorTypesService = new Mock<ISensorTypesService>();
            var sensorsService = new Mock<ISensorsService>();
            var sensorsApi = new Mock<ISensorsAPI>();
            var mockUserManager = TestHelpers.GetTestUserManager();

            sensorsService.Setup(s => s.GetAllPublicSensorsAsync()).ReturnsAsync(new List<Sensor>
            {
                TestHelpers.TestPublicSensor(),
                TestHelpers.TestPrivateSensor()
            });

            var controller = new SensorController(sensorTypesService.Object, sensorsService.Object,
                mockUserManager.Object, sensorsApi.Object);

            var jsonResult = await controller.GetPublicSensors();
            dynamic result = JsonConvert.DeserializeObject(jsonResult.Value.ToString());

            Assert.AreEqual(2, result.GetType().GetProperty("Count").GetValue(result, null));
        }

        [TestMethod]
        public async Task Invoce_GetAllPublicSensorsAsync_FromSensorService()
        {
            var sensorTypesService = new Mock<ISensorTypesService>();
            var sensorsService = new Mock<ISensorsService>();
            var sensorsApi = new Mock<ISensorsAPI>();
            var mockUserManager = TestHelpers.GetTestUserManager();

            sensorsService.Setup(s => s.GetAllPublicSensorsAsync()).ReturnsAsync(new List<Sensor>
            {
                TestHelpers.TestPublicSensor(),
                TestHelpers.TestPrivateSensor()
            });

            var controller = new SensorController(sensorTypesService.Object, sensorsService.Object,
                mockUserManager.Object, sensorsApi.Object);

            await controller.GetPublicSensors();

            sensorsService.Verify(s => s.GetAllPublicSensorsAsync(), Times.Once());
        }
    }
}