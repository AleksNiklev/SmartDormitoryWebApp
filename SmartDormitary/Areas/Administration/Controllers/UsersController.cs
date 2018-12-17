using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartDormitary.Areas.Administration.Models;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;

namespace SmartDormitary.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public class UsersController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService, SignInManager<User> signInManager)
        {
            this.usersService = usersService;
            this.signInManager = signInManager;
        }

        [TempData] public string StatusMessage { get; set; }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await usersService.GetAllUsersAsync();
            var viewModel = users.Select(u => new UserViewModel(u));
            return View(viewModel);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var user = await usersService.GetUserByGuidAsync(id);

            if (user == null) return NotFound();

            var userModel = new UserViewModel(user);

            return View(userModel);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var user = await usersService.GetUserByGuidAsync(id);
            if (user == null) return NotFound();

            return View(new UserViewModel(user));
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await usersService.GetUserByGuidAsync(id);
                    user.UserName = userViewModel.Username;
                    user.Email = userViewModel.Email;

                    await usersService.UpdateUserAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await usersService.UserExistsAsync(id) == false)
                        return NotFound();
                    throw;
                }

                StatusMessage = $"Successfully edited {userViewModel.Username}'s account.";
                return RedirectToAction(nameof(Edit), new {id});
            }

            StatusMessage = "Error: Something went wrong...";
            return View();
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> DeleteUserSensors(Guid id)
        {
            await usersService.DeleteUserSensorsAsync(id);
            StatusMessage = "Successfully removed the sensors registered by this user.";
            return RedirectToAction(nameof(Edit), new {id});
        }

        public async Task<IActionResult> ToggleStaffRoleAsync(string id)
        {
            var user = await usersService.GetUserByGuidAsync(new Guid(id));

            var isStaff = await signInManager.UserManager.IsInRoleAsync(user, "Administrator");

            if (isStaff)
            {
                await signInManager.UserManager.RemoveFromRoleAsync(user, "Administrator");
                StatusMessage = $"Successfully removed {user.UserName}'s Admin Status";
            }
            else
            {
                await signInManager.UserManager.AddToRoleAsync(user, "Administrator");
                StatusMessage = $"Successfully granted {user.UserName} an Admin Status";
            }

            await signInManager.UserManager.UpdateSecurityStampAsync(user);

            return RedirectToAction(nameof(Edit), new {id});
        }
    }
}