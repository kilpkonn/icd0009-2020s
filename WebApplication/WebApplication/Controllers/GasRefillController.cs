using System;
using System.Threading.Tasks;
using CarApp.BLL.App;
using CarApp.DAL.App;
using Domain.App;
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
        public async Task<IActionResult> Create(CreateEditViewModel ceVm)
        {
            var gasRefill = ceVm.GasRefill;
            if (ModelState.IsValid)
            {
                _bll.GasRefills.Add(gasRefill!);
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
        public async Task<IActionResult> Edit(Guid id, CreateEditViewModel ceVm)
        {
            var gasRefill = ceVm.GasRefill;
            if (id != (gasRefill?.Id ?? null))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var toUpdate = await _bll.GasRefills.FirstOrDefaultAsync(id, User.GetUserId());
                    toUpdate!.Cost = gasRefill!.Cost;
                    toUpdate.Timestamp = gasRefill.Timestamp;
                    toUpdate.AmountRefilled = gasRefill.AmountRefilled;
                    toUpdate.AppUserId = (Guid) User.GetUserId()!;
                    _bll.GasRefills.Update(toUpdate, User.GetUserId());
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
            var gasRefill = await _bll.GasRefills.FirstOrDefaultAsync(id, User.GetUserId());
            if (gasRefill != null)
            {
                _bll.GasRefills.Remove(gasRefill, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GasRefillExists(Guid id)
        {
            return await _bll.GasRefills.ExistsAsync(id, User.GetUserId());
        }
    }
}
