using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.StaticMaps;
using GoogleMapsApi.StaticMaps.Entities;
using GoogleMapsApi.StaticMaps.Enums;
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

            // Google Maps - Will move it to a service instead probably...
            var mapsService = new StaticMapsEngine();
            var sensorMarkers = new List<Marker>();
            foreach (var sensor in sensors)
            {
                if (!sensor.IsPublic) continue;

                sensorMarkers.Add(new Marker
                    {Locations = new List<ILocationString> {new Location(sensor.Latitude, sensor.Longitude)}});
            }

            var request = new StaticMapRequest(sensorMarkers, new ImageSize(800, 600))
            {
                // TODO: Move API KEY to some "safer" place.
                ApiKey = "AIzaSyDOc4hXPYpMR4Gos817M6Iz_5hUKrPE0k4",
                Center = new Location(42.6865786, 23.3335581),
                IsSSL = true,
                Zoom = 12,
                Style = new MapStyle {MapFeature = MapFeature.Road},
                MapType = MapType.Roadmap
            };
            var map = mapsService.GenerateStaticMapURL(request);

            TempData["StaticMapUri"] = map;

            return View("Index", result);
        }

        [Authorize(Roles = "Administrator, User")]
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