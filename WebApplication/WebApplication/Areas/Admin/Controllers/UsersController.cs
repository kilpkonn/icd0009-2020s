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
    /// <inheritdoc />
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        /// <inheritdoc />
        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Admin/Users
        /// <summary>
        /// Get Users
        /// </summary>
        /// <returns>Users</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        // GET: Admin/Users/Details/5
        /// <summary>
        /// Get Detailed user data
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User data</returns>
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
        /// <summary>
        /// Create user
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="postVm">user data</param>
        /// <returns></returns>
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
        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
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
        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="postVm">user data</param>
        /// <returns></returns>
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
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
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
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
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