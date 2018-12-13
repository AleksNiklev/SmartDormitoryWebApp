using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartDormitary.Areas.Administration.Models;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;
using SmartDormitory.API.DormitaryAPI;

namespace SmartDormitary.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public class SensorsController : Controller
    {
        private readonly ISensorsService sensorsService;
        private readonly ISensorTypesService sensorTypesService;
        private readonly ISensorsAPI sensorApi;
        private readonly UserManager<User> userManager;
        private readonly IUsersService usersService;

        public SensorsController(IUsersService usersService, ISensorsService sensorsService,
            ISensorTypesService sensorTypesService, ISensorsAPI sensorApi, UserManager<User> userManager)
        {
            this.usersService = usersService;
            this.sensorsService = sensorsService;
            this.sensorTypesService = sensorTypesService;
            this.sensorApi = sensorApi;
            this.userManager = userManager;
        }

        [TempData] public string StatusMessage { get; set; }

        // GET: Administration/Sensors
        public async Task<IActionResult> Index()
        {
            var sensorsInclude = await sensorsService.GetAllSensorsAsync();
            return View(sensorsInclude);
        }

        // GET: Administration/Sensors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var sensor = await sensorsService.GetSensorByGuidAsync(id);
            if (sensor == null) return NotFound();

            var sensorView = new SensorViewModel(sensor);
            return View(sensorView);
        }

        // GET: Administration/Sensors/Create
        public async Task<IActionResult> Create()
        {
            ViewData["SensorTypes"] =
                new SelectList(await sensorTypesService.GetAllSensorTypesAsync(), "Id", "Description");
            ViewData["Users"] = new SelectList(await usersService.GetAllUsersAsync(), "Id", "UserName");
            return View();
        }

        // POST: Administration/Sensors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SensorViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.MinAcceptableValue >= model.MaxAcceptableValue)
                {
                    StatusMessage = "Error: Max Acceptable value should be greater than Min Acceptable Value.";
                    return View(model);
                }

                var typeId = model.SensorTypeId;
                var sensorApi = await this.sensorApi.GetSensorAsync(typeId);

                var sensorData = new SensorData
                {
                    Value = sensorApi.Value,
                    Timestamp = sensorApi.Timestamp
                };

                var sensorDataEntity = await sensorsService.RegisterSensorDataAsync(sensorData);

                var sensor = new Sensor
                {
                    Id = new Guid(),
                    Name = model.Name,
                    Description = model.Description,
                    RefreshTime = model.RefreshTime,
                    Longitude = model.Longitude,
                    Latitude = model.Latitude,
                    IsPublic = model.IsPublic,
                    TickOff = model.TickOff,
                    MinAcceptableValue = model.MinAcceptableValue,
                    MaxAcceptableValue = model.MaxAcceptableValue,
                    SensorTypeId = typeId,
                    SensorDataId = sensorDataEntity.Entity.Id,
                    UserId = userManager.Users.First(u => u.UserName == User.Identity.Name).Id
                };

                await sensorsService.RegisterSensorAsync(sensor);
                return RedirectToAction(nameof(Details), new { id = sensor.Id });
            }

            ViewData["SensorTypes"] = new SelectList(await sensorTypesService.GetAllSensorTypesAsync(), "Id",
                "Description", model.SensorTypeId);
            ViewData["Users"] = new SelectList(await usersService.GetAllUsersAsync(), "Id", "UserName", model.UserId);
            StatusMessage = "Error: Something went wrong...";
            return View(model);
        }

        // GET: Administration/Sensors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var sensor = await sensorsService.GetSensorByGuidAsync(id);
            if (sensor == null) return NotFound();

            ViewData["SensorTypes"] = new SelectList(await sensorTypesService.GetAllSensorTypesAsync(), "Id",
                "Description", sensor.SensorTypeId);
            ViewData["Users"] = new SelectList(await usersService.GetAllUsersAsync(), "Id", "UserName", sensor.UserId);
            var sensorView = new SensorViewModel(sensor);
            return View(sensorView);
        }

        // POST: Administration/Sensors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SensorViewModel sensor)
        {
            if (id != sensor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var sensorModel = await sensorsService.GetSensorByGuidAsync(sensor.Id);
                    sensorModel.Name = sensor.Name;
                    sensorModel.Description = sensor.Description;
                    sensorModel.RefreshTime = sensor.RefreshTime;
                    sensorModel.MinAcceptableValue = sensor.MinAcceptableValue;
                    sensorModel.MaxAcceptableValue = sensor.MaxAcceptableValue;
                    sensorModel.Longitude = sensor.Longitude;
                    sensorModel.Latitude = sensor.Latitude;
                    sensorModel.IsPublic = sensor.IsPublic;
                    sensorModel.TickOff = sensor.TickOff;
                    sensorModel.SensorTypeId = sensor.SensorTypeId;
                    sensorModel.UserId = sensor.UserId;
                    
                    await sensorsService.UpdateSensorAsync(sensorModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await sensorsService.SensorExists(sensor.Id) == false)
                        return NotFound();
                    throw;
                }

                StatusMessage = "Successfully saved the changes.";
                return RedirectToAction(nameof(Edit), new { id });
            }

            ViewData["SensorTypes"] = new SelectList(await sensorTypesService.GetAllSensorTypesAsync(), "Id",
                "Description", sensor.SensorTypeId);
            ViewData["Users"] = new SelectList(await usersService.GetAllUsersAsync(), "Id", "UserName", sensor.UserId);
            StatusMessage = "Error: Something went wrong...";
            return View(sensor);
        }

        // GET: Administration/Sensors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var sensor = await sensorsService.GetSensorByGuidAsync(id);
            if (sensor == null) return NotFound();

            var sensorView = new SensorViewModel(sensor);
            return View(sensorView);
        }

        // POST: Administration/Sensors/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await sensorsService.DeleteSensorsAsync(id);
            StatusMessage = "Successfully deleted the sensor.";
            return RedirectToAction(nameof(Index));
        }
    }
}