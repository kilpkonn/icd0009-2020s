using System;
using System.Threading.Tasks;
using CarApp.DAL.App;
using Domain.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Controllers
{
    public class CarAccessController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CarAccessController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CarAccess
        public async Task<IActionResult> Index()
        {
            return View(await _uow.CarAccesses.GetAllAsync());
        }

        // GET: CarAccess/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccess = await _uow.CarAccesses.FirstOrDefaultAsync((Guid) id!);
            if (carAccess == null)
            {
                return NotFound();
            }

            return View(carAccess);
        }

        // GET: CarAccess/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(), "Id", "Id");
            ViewData["CarAccessTypeId"] = new SelectList(await _uow.CarAccessTypes.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: CarAccess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CarId,CarAccessTypeId")]
            CarAccess carAccess)
        {
            if (ModelState.IsValid)
            {
                carAccess.Id = Guid.NewGuid();
                _uow.CarAccesses.Add(carAccess);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(), "Id", "Id", carAccess.CarId);
            ViewData["CarAccessTypeId"] =
                new SelectList(await _uow.CarAccessTypes.GetAllAsync(), "Id", "Name", carAccess.CarAccessTypeId);
            return View(carAccess);
        }

        // GET: CarAccess/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccess = await _uow.CarAccesses.FirstOrDefaultAsync((Guid) id);
            if (carAccess == null)
            {
                return NotFound();
            }

            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(), "Id", "Id", carAccess.CarId);
            ViewData["CarAccessTypeId"] =
                new SelectList(await _uow.CarAccessTypes.GetAllAsync(), "Id", "Name", carAccess.CarAccessTypeId);
            return View(carAccess);
        }

        // POST: CarAccess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,CarId,CarAccessTypeId")]
            CarAccess carAccess)
        {
            if (id != carAccess.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.CarAccesses.Update(carAccess);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarAccessExists(carAccess.Id))
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

            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(), "Id", "Id", carAccess.CarId);
            ViewData["CarAccessTypeId"] =
                new SelectList(await _uow.CarAccessTypes.GetAllAsync(), "Id", "Name", carAccess.CarAccessTypeId);
            return View(carAccess);
        }

        // GET: CarAccess/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccess = await _uow.CarAccesses.FirstOrDefaultAsync((Guid) id);
            if (carAccess == null)
            {
                return NotFound();
            }

            return View(carAccess);
        }

        // POST: CarAccess/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carAccess = await _uow.CarAccesses.FirstOrDefaultAsync(id);
            if (carAccess != null)
            {
                _uow.CarAccesses.Remove(carAccess);
                await _uow.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarAccessExists(Guid id)
        {
            return await _uow.CarAccesses.ExistsAsync(id);
        }
    }
}