using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SmartDormitary.Data.Models;
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
        private readonly UserManager<User> userManeger;

        public HomeController(IRestClient restClient, ISensorTypesService sensorTypesService, ISensorsService sensorsService, UserManager<User> userManeger)
        {
            this.restClient = restClient;
            this.sensorTypesService = sensorTypesService;
            this.sensorsService = sensorsService;
            this.userManeger = userManeger;
        }

        public async Task<IActionResult> Index()
        {
            var sensors = await this.sensorsService.GetAllPublicSensorsAsync();

            var result = sensors.Select(s => new SensorViewModel(s));

            return View("Index", result);
        }

        [Authorize(Roles = "Administrator, User")]
        public async Task<IActionResult> Sensors()
        {
            var user = this.userManeger.Users.Where(u => u.UserName == User.Identity.Name).First();
            var sensors = await this.sensorsService.GetUserSensorsAsync(user.Id);
            var result = sensors.Select(s => new SensorViewModel(s));

            return View("Sensors", result);
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
