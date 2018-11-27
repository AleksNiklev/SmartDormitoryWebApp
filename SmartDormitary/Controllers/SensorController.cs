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
using SmartDormitary.Services.Cron.Contracts;
using Newtonsoft.Json;

namespace SmartDormitary.Controllers
{
    public class SensorController : Controller
    {
        private readonly ISensorTypesService sensorTypesService;
        private readonly ISensorsService sensorsService;
        private readonly UserManager<User> userManeger;
        private readonly IJobScheduleService scheduleService;

        public SensorController(ISensorTypesService sensorTypesService, ISensorsService sensorsService, UserManager<User> userManeger, IJobScheduleService scheduleService)
        {
            this.sensorTypesService = sensorTypesService;
            this.sensorsService = sensorsService;
            this.userManeger = userManeger;
            this.scheduleService = scheduleService;
        }

        [Authorize(Roles = "Administrator, User")]
        public async Task<IActionResult> Index()
        {
            var sensorTypes = await this.sensorTypesService.GetAllSensorTypesAsync();
            var result = sensorTypes.Select(s => new SensorTypeViewModel(s));
            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, User")]
        public async Task<IActionResult> Register(Guid Id)
        {
            var model = await this.sensorTypesService.GetSensorTypesByIdAsync(Id);
            var sensor = new RegisterSensorViewModel(new SensorTypeViewModel(model));
            return View(sensor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, User")]
        public async Task<IActionResult> Register(RegisterSensorViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var typeId = model.Id;
            model.Id = Guid.Empty;
            var sensor = new Sensor()
            {
                Name = model.Name,
                Description = model.Description,
                RefreshTime = model.PullingInterval,
                Timestamp = new DateTime(),
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                IsPublic = model.IsPublic,
                TickOff = model.TickOff,
                MinAcceptableValue = model.MinAcceptableValue,
                MaxAcceptableValue = model.MaxAcceptableValue,
                SensorTypeId = typeId,
                Value = "0",
                UserId = this.userManeger.Users.Where(u => u.UserName == User.Identity.Name).First().Id
            };

            var result = await this.sensorsService.RegisterSensorAsync(sensor);
            //this.scheduleService.RunOneJob(result.Entity.Id, result.Entity.RefreshTime);
            return RedirectToAction("Details", new { id = result.Entity.Id });
        }

        [Authorize(Roles = "Administrator, User")]
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

        public async Task<JsonResult> GetPublicSensors()
        {
            var sensors = await this.sensorsService.GetAllPublicSensorsAsync();
            var result = JsonConvert.SerializeObject(sensors.Select(s => new { x = s.Latitude, y = s.Longitude, name = s.Name }));

            return Json(result);
        }
    }
}