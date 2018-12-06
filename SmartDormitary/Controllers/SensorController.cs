using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartDormitary.Data.Models;
using SmartDormitary.Models.SensorViewModels;
using SmartDormitary.Services.Contracts;
using SmartDormitary.Services.Cron;
using Newtonsoft.Json;
using SmartDormitory.API.DormitaryAPI;

namespace SmartDormitary.Controllers
{
    [Authorize]
    public class SensorController : Controller
    {
        private readonly ISensorTypesService sensorTypesService;
        private readonly ISensorsService sensorsService;
        private readonly UserManager<User> userManager;
        private readonly ISensorsAPI sensorApi;

        public SensorController(ISensorTypesService sensorTypesService, ISensorsService sensorsService, UserManager<User> userManager, ISensorsAPI sensorApi)
        {
            this.sensorTypesService = sensorTypesService;
            this.sensorsService = sensorsService;
            this.userManager = userManager;
            this.sensorApi = sensorApi;
        }

        public async Task<IActionResult> Index()
        {
            var sensorTypes = await this.sensorTypesService.GetAllSensorTypesAsync();
            var result = sensorTypes.Select(s => new SensorTypeViewModel(s));
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Register(Guid Id)
        {
            var model = await this.sensorTypesService.GetSensorTypeByIdAsync(Id);
            var sensor = new RegisterSensorViewModel(new SensorTypeViewModel(model));
            return View(sensor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterSensorViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Index");
            }

            var typeId = model.Id;
            var sensorApi = await this.sensorApi.GetSensorAsync(typeId);
            model.Id = Guid.Empty;

            var sensorData = new SensorData()
            {
                Value = sensorApi.Value,
                Timestamp = sensorApi.Timestamp
            };

            var sensrDataEntity = await this.sensorsService.RegisterSensorDataAsync(sensorData);

            var sensor = new Sensor()
            {
                Name = model.Name,
                Description = model.Description,
                RefreshTime = model.PullingInterval,
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                IsPublic = model.IsPublic,
                TickOff = model.TickOff,
                MinAcceptableValue = model.MinAcceptableValue,
                MaxAcceptableValue = model.MaxAcceptableValue,
                SensorTypeId = typeId,
                SensorDataId = sensrDataEntity.Entity.Id,
                UserId = this.userManager.Users.First(u => u.UserName == User.Identity.Name).Id
            };

            var result = await this.sensorsService.RegisterSensorAsync(sensor);
            return RedirectToAction("Details", new { id = result.Entity.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var model = await this.sensorsService.GetSensorByGuidAsync(Id);
            var sensor = new RegisterSensorViewModel()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                MinAcceptableValue = model.MinAcceptableValue,
                MaxAcceptableValue = model.MaxAcceptableValue,
                IsPublic = model.IsPublic,
                TickOff = model.TickOff,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                PullingInterval = model.RefreshTime,
                DefaultMaxAcceptableValue = model.SensorType.MaxAcceptableValue,
                DefaultMinAcceptableValue = model.SensorType.MinAcceptableValue,
                DefaultPullingInterval = model.SensorType.MinRefreshTime
            };

            return View(sensor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RegisterSensorViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Update");
            }

            var sensor = await this.sensorsService.GetSensorByGuidAsync(model.Id);
            sensor.Name = model.Name;
            sensor.Description = model.Description;
            sensor.RefreshTime = model.PullingInterval;
            sensor.MinAcceptableValue = model.MinAcceptableValue;
            sensor.MaxAcceptableValue = model.MaxAcceptableValue;
            sensor.Longitude = model.Longitude;
            sensor.Latitude = model.Latitude;
            sensor.IsPublic = model.IsPublic;
            sensor.TickOff = model.TickOff;

            var result = await this.sensorsService.UpdateSensorAsync(sensor);
            return RedirectToAction("Details", new { id = result.Entity.Id });
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var sensor = await this.sensorsService.GetSensorByGuidAsync(id);
            if (sensor == null)
            {
                return RedirectToAction("Index");
            }

            var model = new SensorViewModel(sensor);
            return View(model);
        }
        
        [AllowAnonymous]
        public async Task<JsonResult> GetPublicSensors()
        {
            var sensors = await this.sensorsService.GetAllPublicSensorsAsync();
            var result = JsonConvert.SerializeObject(sensors.Select(s => new { x = s.Latitude, y = s.Longitude, name = s.Name }));

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateSensor(SensorViewModel sensorModel)
        {
            var sensor = await this.sensorsService.GetSensorByGuidAsync(sensorModel.Id);
            sensor.Name = sensorModel.Name;
            sensor.Description = sensorModel.Description;
            sensor.Longitude = sensorModel.Longitude;
            sensor.Latitude = sensorModel.Latitude;
            var result = await this.sensorsService.UpdateSensorAsync(sensor);

            return Json(new {
                result.Entity.Name,
                result.Entity.SensorData.Value,
                result.Entity.SensorData.Timestamp,
                result.Entity.RefreshTime,
                result.Entity.MinAcceptableValue,
                result.Entity.MaxAcceptableValue,
                result.Entity.Longitude,
                result.Entity.Latitude,
                result.Entity.Description,
                result.Entity.SensorType,
                result.Entity.SensorTypeId,
                result.Entity.IsPublic,
                result.Entity.TickOff,
                result.Entity.UserId
            });
        }

        public async Task<JsonResult> GetSensorById(Guid id)
        {
            var result = await this.sensorsService.GetSensorByGuidAsync(id);
            return Json(new { name = result.Name, value = result.SensorData.Value, timeStamp = result.SensorData.Timestamp, refreshTime = result.RefreshTime });
        }
    }
}