﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartDormitary.Areas.Administration.Models;
using SmartDormitary.Services.Contracts;

namespace SmartDormitary.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;
        private readonly ISensorsService sensorsService;

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
    }
}