using System;
using System.Threading.Tasks;
using CarApp.DAL.App;
using Domain.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Controllers
{
    public class GasRefillController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public GasRefillController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: GasRefill
        public async Task<IActionResult> Index()
        {
            return View(await _uow.GasRefills.GetAllAsync());
        }

        // GET: GasRefill/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _uow.GasRefills
                .FirstOrDefaultAsync((Guid) id);
            if (gasRefill == null)
            {
                return NotFound();
            }

            return View(gasRefill);
        }

        // GET: GasRefill/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(), "Id", "Id");
            return View();
        }

        // POST: GasRefill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AmountRefilled,Timestamp,Cost,CarId")] GasRefill gasRefill)
        {
            if (ModelState.IsValid)
            {
                gasRefill.Id = Guid.NewGuid();
                _uow.GasRefills.Add(gasRefill);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(), "Id", "Id", gasRefill.CarId);
            return View(gasRefill);
        }

        // GET: GasRefill/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _uow.GasRefills.FirstOrDefaultAsync((Guid) id);
            if (gasRefill == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(), "Id", "Id", gasRefill.CarId);
            return View(gasRefill);
        }

        // POST: GasRefill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AmountRefilled,Timestamp,Cost,CarId")] GasRefill gasRefill)
        {
            if (id != gasRefill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.GasRefills.Update(gasRefill);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GasRefillExists(gasRefill.Id))
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
            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(), "Id", "Id", gasRefill.CarId);
            return View(gasRefill);
        }

        // GET: GasRefill/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _uow.GasRefills
                .FirstOrDefaultAsync((Guid) id);
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
            var gasRefill = await _uow.GasRefills.FirstOrDefaultAsync(id);
            if (gasRefill != null)
            {
                _uow.GasRefills.Remove(gasRefill);
                await _uow.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GasRefillExists(Guid id)
        {
            return await _uow.GasRefills.ExistsAsync(id);
        }
    }
}
