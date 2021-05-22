using System;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    /// <inheritdoc />
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        /// <inheritdoc />
        public RolesController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: Admin/Roles
        /// <summary>
        /// Roles index
        /// </summary>
        /// <returns>Roles list</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        // GET: Admin/Roles/Details/5
        /// <summary>
        /// Roles details view
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Role</returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _roleManager.FindByIdAsync(id.ToString());
            if (appRole == null)
            {
                return NotFound();
            }

            return View(appRole);
        }

        // GET: Admin/Roles/Create
        /// <summary>
        /// Create new role
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create new role
        /// </summary>
        /// <param name="appRole">data</param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                appRole.ConcurrencyStamp = Guid.NewGuid().ToString();
                await _roleManager.CreateAsync(appRole);
                return RedirectToAction(nameof(Index));
            }
            return View(appRole);
        }

        // GET: Admin/Roles/Edit/5
        /// <summary>
        /// Edit role
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _roleManager.FindByIdAsync(id.ToString());
            if (appRole == null)
            {
                return NotFound();
            }
            return View(appRole);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit role
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="appRole">Role</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AppRole appRole)
        {
            if (id != appRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id.ToString());
                    role!.Name = appRole.Name;
                    role.NormalizedName = appRole.NormalizedName;
                    role.ConcurrencyStamp = Guid.NewGuid().ToString();
                    await _roleManager.UpdateAsync(role);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppRoleExists(appRole.Id))
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
            return View(appRole);
        }

        // GET: Admin/Roles/Delete/5
        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _roleManager.FindByIdAsync(id.ToString());
            if (appRole == null)
            {
                return NotFound();
            }

            return View(appRole);
        }

        // POST: Admin/Roles/Delete/5
        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var appRole = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(appRole);
            return RedirectToAction(nameof(Index));
        }

        private bool AppRoleExists(Guid id)
        {
            return _roleManager.Roles.AsNoTracking().Any(e => e.Id == id);
        }
    }
}
