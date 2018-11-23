using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SmartDormitary.Models;
using SmartDormitary.Models.SensorViewModels;
using SmartDormitary.Services.Contracts;
using SmartDormitory.API.DormitaryAPI;

namespace SmartDormitary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestClient restClient;
        private readonly ISensorTypesService sensorTypesService;
        private readonly ISensorsService sensorsService;

        public HomeController(IRestClient restClient, ISensorTypesService sensorTypesService, ISensorsService sensorsService)
        {
            this.restClient = restClient;
            this.sensorTypesService = sensorTypesService;
            this.sensorsService = sensorsService;
        }

        public async Task<IActionResult> Index()
        {
            var sensors = await this.sensorsService.GetAllPublicSensorsAsync();

            var result = sensors.Select(s => new SensorViewModel()
            {
                Name = s.Name,
                Description = s.Description,
                PullingInterval = s.RefreshTime,
                Url = s.SensorTypeId.ToString(),
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                Value = s.Value,
                IsPublic = s.IsPublic,
                MaxAcceptableValue = s.MaxAcceptableValue,
                MinAcceptableValue = s.MinAcceptableValue,
                TickOff = s.TickOff

            });

            return View("Index", result);
        }

        public async Task<IActionResult> About()
        {
            // Testing the sensor API.
            var test = new SensorsAPI(restClient);

            var response = test.GetAllSensors();
            var responseAsync = await test.GetAllSensorsAsync();
            var newResponse = await test.GetSensorAsync(response.First().SensorId);

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
