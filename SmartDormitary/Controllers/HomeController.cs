using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SmartDormitary.Models;
using SmartDormitary.Services.Contracts;
using SmartDormitory.API.DormitaryAPI;

namespace SmartDormitary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRestClient restClient;
        private readonly ISensorTypesService sensorTypesService;

        public HomeController(IRestClient restClient, ISensorTypesService sensorTypesService)
        {
            this.restClient = restClient;
            this.sensorTypesService = sensorTypesService;
        }

        public IActionResult Index()
        {
            RecurringJob.AddOrUpdate(
                () => sensorTypesService.SeedSensorTypesAsync(),
                Cron.MinuteInterval(1));

            return View();
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
