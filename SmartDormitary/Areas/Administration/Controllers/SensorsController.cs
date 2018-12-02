using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;

namespace SmartDormitary.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public class SensorsController : Controller
    {
        private readonly IUsersService usersService;
        private readonly ISensorsService sensorsService;
        private readonly ISensorTypesService sensorTypesService;
        private readonly SmartDormitaryContext _context;

        [TempData] public string StatusMessage { get; set; }
        
        public SensorsController(IUsersService usersService, ISensorsService sensorsService, ISensorTypesService sensorTypesService, SmartDormitaryContext context)
        {
            this.usersService = usersService;
            this.sensorsService = sensorsService;
            this.sensorTypesService = sensorTypesService;
            _context = context;
        }

        // GET: Administration/Sensors
        public async Task<IActionResult> Index()
        {
            var smartDormitaryContext = _context.Sensors.Include(s => s.SensorType).Include(s => s.User);
            return View(await smartDormitaryContext.ToListAsync());
        }

        // GET: Administration/Sensors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensors
                .Include(s => s.SensorType)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sensor == null)
            {
                return NotFound();
            }

            return View(sensor);
        }

        // GET: Administration/Sensors/Create
        public IActionResult Create()
        {
            ViewData["SensorTypeId"] = new SelectList(_context.SensorTypes, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Administration/Sensors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RefreshTime,IsPublic,Timestamp,Latitude,Longitude,Value,MinAcceptableValue,MaxAcceptableValue,TickOff,SensorTypeId,UserId")] Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                sensor.Id = Guid.NewGuid();
                _context.Add(sensor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SensorTypeId"] = new SelectList(_context.SensorTypes, "Id", "Description", sensor.SensorTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", sensor.UserId);
            return View(sensor);
        }

        // GET: Administration/Sensors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await sensorsService.GetSensorByGuidAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }

            ViewData["SensorTypes"] = new SelectList(await sensorTypesService.GetAllSensorTypesAsync(), "Id", "Description", sensor.SensorTypeId);
            ViewData["Users"] = new SelectList(await usersService.GetAllUsersAsync(), "Id", "UserName", sensor.UserId);
            return View(sensor);
        }

        // POST: Administration/Sensors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,RefreshTime,IsPublic,Timestamp,Latitude,Longitude,Value,MinAcceptableValue,MaxAcceptableValue,TickOff,SensorTypeId,UserId")] Sensor sensor)
        {
            if (id != sensor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sensor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await sensorsService.SensorExists(sensor.Id) == false)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                this.StatusMessage = $"Successfully saved the changes.";
                return RedirectToAction(nameof(Edit), new {id = id});
            }
            ViewData["SensorTypes"] = new SelectList(await sensorTypesService.GetAllSensorTypesAsync(), "Id", "Description", sensor.SensorTypeId);
            ViewData["Users"] = new SelectList(await usersService.GetAllUsersAsync(), "Id", "UserName", sensor.UserId);
            return View(sensor);
        }

        // GET: Administration/Sensors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensors
                .Include(s => s.SensorType)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sensor == null)
            {
                return NotFound();
            }

            return View(sensor);
        }

        // POST: Administration/Sensors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
