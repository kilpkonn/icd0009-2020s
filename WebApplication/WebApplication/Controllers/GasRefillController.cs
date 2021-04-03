using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;
using WebApplication.Models.GasRefill;

namespace WebApplication.Controllers
{
    [Authorize]
    public class GasRefillController : Controller
    {
        private readonly IAppBll _bll;

        public GasRefillController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: GasRefill
        public async Task<IActionResult> Index()
        {
            return View(await _bll.GasRefills.GetAllAsync(User.GetUserId()));
        }

        // GET: GasRefill/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _bll.GasRefills
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (gasRefill == null)
            {
                return NotFound();
            }

            return View(gasRefill);
        }

        // GET: GasRefill/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CreateEditViewModel()
            {
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // POST: GasRefill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GasRefill gasRefill)
        {
            if (ModelState.IsValid)
            {
                await _bll.GasRefills.AddAsync(gasRefill, User.GetUserId());
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var vm = new CreateEditViewModel()
            {
                GasRefill = gasRefill,
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // GET: GasRefill/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _bll.GasRefills.FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (gasRefill == null)
            {
                return NotFound();
            }

            var vm = new CreateEditViewModel()
            {
                GasRefill = gasRefill,
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // POST: GasRefill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, GasRefill gasRefill)
        {
            if (id != gasRefill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.GasRefills.UpdateAsync(gasRefill, User.GetUserId());
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GasRefillExists(gasRefill!.Id))
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

            var vm = new CreateEditViewModel()
            {
                GasRefill = gasRefill,
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // GET: GasRefill/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _bll.GasRefills
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (gasRefill == null)
            {
                return NotFound();
            }

            return View(gasRefill);
        }

        // POST: GasRefill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GasRefills.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GasRefillExists(Guid id)
        {
            return await _bll.GasRefills.ExistsAsync(id, User.GetUserId());
        }
    }
}