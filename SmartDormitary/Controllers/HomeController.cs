using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SmartDormitary.Areas.Administration.Models;
using SmartDormitary.Data.Models;
using SmartDormitary.Models;
using SmartDormitary.Services.Contracts;
using SensorViewModel = SmartDormitary.Models.SensorViewModels.SensorViewModel;

namespace SmartDormitary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISensorsService sensorsService;
        private readonly ISensorTypesService sensorTypesService;
        private readonly UserManager<User> userManager;

        public HomeController( ISensorTypesService sensorTypesService,
            ISensorsService sensorsService, UserManager<User> userManager)
        {
            this.sensorTypesService = sensorTypesService;
            this.sensorsService = sensorsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var sensors = await sensorsService.GetAllPublicSensorsAsync();

            var result = sensors.Select(s => new SensorViewModel(s));

            TempData["userId"] = userManager.GetUserId(User);

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

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}