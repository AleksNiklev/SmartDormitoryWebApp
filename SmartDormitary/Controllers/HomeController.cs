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
        private readonly ISensorsService sensorsService;
        private readonly ISensorTypesService sensorTypesService;
        private readonly UserManager<User> userManager;

        public HomeController(IRestClient restClient, ISensorTypesService sensorTypesService,
            ISensorsService sensorsService, UserManager<User> userManager)
        {
            this.restClient = restClient;
            this.sensorTypesService = sensorTypesService;
            this.sensorsService = sensorsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var sensors = await sensorsService.GetAllPublicSensorsAsync();

            var result = sensors.Select(s => new SensorViewModel(s));

            return View("Index", result);
        }

        [Authorize]
        public async Task<IActionResult> Sensors()
        {
            var user = userManager.Users.First(u => u.UserName == User.Identity.Name);
            var sensors = await sensorsService.GetUserSensorsAsync(user.Id);
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
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}