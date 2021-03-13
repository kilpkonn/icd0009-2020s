using System;
using System.Threading.Tasks;
using CarApp.DAL.App;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;

namespace WebApplication.Controllers
{
    [Authorize]
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
            return View(await _uow.CarAccesses.GetAllAsync(User.GetUserId()));
        }

        // GET: CarAccess/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var carAccess = await _uow.CarAccesses.FirstOrDefaultAsync((Guid) id!, User.GetUserId());
            if (carAccess == null)
            {
                return NotFound();
            }

            return View(carAccess);
        }

        // GET: CarAccess/Create
        public async Task<IActionResult> Create()
        {
            var userId = User.GetUserId();
            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(userId), "Id", "Id");
            ViewData["CarAccessTypeId"] = new SelectList(await _uow.CarAccessTypes.GetAllAsync(userId), "Id", "Name");
            return View();
        }

        // POST: CarAccess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarAccess carAccess)
        {
            if (ModelState.IsValid)
            {
                carAccess.Id = Guid.NewGuid();
                carAccess.AppUserId = (Guid) User.GetUserId()!;
                _uow.CarAccesses.Add(carAccess);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var userId = (Guid) User!.GetUserId()!;
            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(userId), "Id", "Id", carAccess.CarId);
            ViewData["CarAccessTypeId"] =
                new SelectList(await _uow.CarAccessTypes.GetAllAsync(userId), "Id", "Name", carAccess.CarAccessTypeId);
            return View(carAccess);
        }

        // GET: CarAccess/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = (Guid) User!.GetUserId()!;
            var carAccess = await _uow.CarAccesses.FirstOrDefaultAsync((Guid) id, userId);
            if (carAccess == null)
            {
                return NotFound();
            }

            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(userId), "Id", "Id", carAccess.CarId);
            ViewData["CarAccessTypeId"] =
                new SelectList(await _uow.CarAccessTypes.GetAllAsync(userId), "Id", "Name", carAccess.CarAccessTypeId);
            return View(carAccess);
        }

        // POST: CarAccess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CarAccess carAccess)
        {
            if (id != carAccess.Id)
            {
                return NotFound();
            }
            var userId = User.GetUserId();
            if (ModelState.IsValid)
            {
                try
                {
                    _uow.CarAccesses.Update(carAccess, userId);
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

            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAllAsync(userId), "Id", "Id", carAccess.CarId);
            ViewData["CarAccessTypeId"] =
                new SelectList(await _uow.CarAccessTypes.GetAllAsync(userId), "Id", "Name", carAccess.CarAccessTypeId);
            return View(carAccess);
        }

        // GET: CarAccess/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccess = await _uow.CarAccesses.FirstOrDefaultAsync((Guid) id, User.GetUserId());
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
            var userId = User.GetUserId();
            var carAccess = await _uow.CarAccesses.FirstOrDefaultAsync(id, userId);
            if (carAccess != null)
            {
                _uow.CarAccesses.Remove(carAccess, User.GetUserId());
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarAccessExists(Guid id)
        {
            return await _uow.CarAccesses.ExistsAsync(id, User.GetUserId());
        }
    }
}