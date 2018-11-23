using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartDormitary.Models.SensorViewModels;
using SmartDormitary.Services.Contracts;

namespace SmartDormitary.Controllers
{
    public class SensorController : Controller
    {
        private readonly ISensorTypesService sensorTypesService;
        private readonly ISensorsService sensorsService;

        public SensorController(ISensorTypesService sensorTypesService, ISensorsService sensorsService)
        {
            this.sensorTypesService = sensorTypesService;
            this.sensorsService = sensorsService;
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
            var model = await this.sensorTypesService.GetSensorTypesByIdAsync(Id);
            var sensor = new SensorViewModel(new SensorTypeViewModel(model));
            return View(sensor);
        }

        [HttpPost]
        public IActionResult Register(SensorViewModel model)
        {
            //Todo it is passig Id!?
            return View();
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
    }
}