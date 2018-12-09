using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartDormitary.Areas.Administration.Models;
using SmartDormitary.Services.Contracts;

namespace SmartDormitary.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public class HomeController : Controller
    {
        private readonly ISensorsService sensorsService;
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService, ISensorsService sensorsService)
        {
            this.usersService = usersService;
            this.sensorsService = sensorsService;
        }

        public async Task<IActionResult> Index()
        {
            var usersCount = await usersService.GetUsersCountAsync();
            var sensorCount = await sensorsService.GetSensorCountAsync();
            var usersList = await usersService.GetLastRegisteredUsersAsync();
            var sensorsList = await sensorsService.GetLastRegisteredSensorsAsync();

            var pageModel = new IndexPageViewModel
            {
                UsersList = usersList,
                SensorsList = sensorsList,
                SensorsCount = sensorCount,
                UsersCount = usersCount
            };

            return View("Index", pageModel);
        }

        public async Task<JsonResult> GetAllSensors()
        {
            var sensors = await sensorsService.GetAllSensorsAsync();
            var result =
                JsonConvert.SerializeObject(sensors.Select(s => new {x = s.Latitude, y = s.Longitude, name = s.Name}));

            return Json(result);
        }
    }
}