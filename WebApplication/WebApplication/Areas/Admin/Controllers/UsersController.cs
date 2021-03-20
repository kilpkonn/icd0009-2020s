using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Areas.Admin.Models;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _userManager.FindByIdAsync(id.ToString());
            if (appUser == null)
            {
                return NotFound();
            }

            UserViewModel vm = new()
            {
                User = appUser,
                Roles = await _roleManager.Roles.ToListAsync(),
                SelectedRoles = await _userManager.GetRolesAsync(appUser),
            };

            return View(vm);
        }

        // GET: Admin/Users/Create
        public async Task<IActionResult> Create()
        {
            UserViewModel vm = new()
            {
                Roles = await _roleManager.Roles.ToListAsync(),
                SelectedRoles = new List<string>(),
            };
            return View(vm);
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel postVm)
        {
            var appUser = postVm.User;
            if (ModelState.IsValid)
            {
                appUser!.Id = Guid.NewGuid();
                appUser.SecurityStamp = Guid.NewGuid().ToString();
                appUser.ConcurrencyStamp = Guid.NewGuid().ToString();
                await _userManager.CreateAsync(appUser);
                
                foreach (var role in postVm.SelectedRoles)
                {
                    if (!await _userManager.IsInRoleAsync(appUser, role))
                    {
                        await _userManager.AddToRoleAsync(appUser, role);
                    }
                }
                
                return RedirectToAction(nameof(Index));
            }
            
            UserViewModel vm = new()
            {
                User = appUser,
                Roles = await _roleManager.Roles.ToListAsync(),
                SelectedRoles = await _userManager.GetRolesAsync(appUser!),
            };

            return View(vm);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _userManager.FindByIdAsync(id.ToString());
            if (appUser == null)
            {
                return NotFound();
            }

            UserViewModel vm = new()
            {
                User = appUser,
                Roles = await _roleManager.Roles.ToListAsync(),
                SelectedRoles = await _userManager.GetRolesAsync(appUser),
            };

            return View(vm);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserViewModel postVm)
        {
            var appUser = postVm.User;
            if (id != (appUser?.Id ?? null))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(appUser!.Id.ToString());
                    user!.Email = appUser.Email;
                    user.UserName = appUser.UserName;
                    user.DisplayName = appUser.DisplayName;
                    user.LockoutEnabled = appUser.LockoutEnabled;
                    user.LockoutEnd = appUser.LockoutEnd;
                    user.NormalizedEmail = appUser.NormalizedEmail;
                    user.PasswordHash = appUser.PasswordHash;
                    user.PhoneNumber = appUser.PhoneNumber;
                    user.PhoneNumberConfirmed = appUser.PhoneNumberConfirmed;
                    user.NormalizedUserName = appUser.NormalizedUserName;
                    user.TwoFactorEnabled = appUser.TwoFactorEnabled;
                    
                    foreach (var role in postVm.SelectedRoles)
                    {
                        if (!await _userManager.IsInRoleAsync(appUser, role))
                        {
                            await _userManager.AddToRoleAsync(appUser, role);
                        }
                    }

                    foreach (var role in await _userManager.GetRolesAsync(appUser))
                    {
                        if (!postVm.SelectedRoles.Contains(role))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role);
                        }
                    }
                    
                    await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser!.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            
            UserViewModel vm = new()
            {
                User = appUser,
                Roles = await _roleManager.Roles.ToListAsync(),
                SelectedRoles = await _userManager.GetRolesAsync(appUser!),
            };

            return View(vm);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _userManager.FindByIdAsync(id.ToString());
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var appUser = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(appUser);
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(Guid id)
        {
            return _userManager.Users.AsNoTracking().Any(m => m.Id == id);
        }
    }
}