using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarApp.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using WebApplication.Helpers;

namespace WebApplication.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CarController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Cars.GetAllAsync(User.GetUserId()));
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _uow.Cars.FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CarModelId"] = new SelectList(await _uow.CarModels.GetAllAsync(User.GetUserId()), "Id", "Name");
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Domain.App.Car car)
        {
            if (ModelState.IsValid)
            {
                car.AppUserId = (Guid) User.GetUserId()!;
                _uow.Cars.Add(car);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CarModelId"] = new SelectList(await _uow.CarTypes.GetAllAsync(User.GetUserId()), "Id", "Name", car.CarTypeId);
            return View(car);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _uow.Cars.FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (car == null)
            {
                return NotFound();
            }

            ViewData["CarModelId"] = new SelectList(await _uow.CarTypes.GetAllAsync(User.GetUserId()), "Id", "Name", car.CarTypeId);
            return View(car);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Domain.App.Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Cars.Update(car, User.GetUserId());
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarExists(car.Id))
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

            ViewData["CarModelId"] = new SelectList(await _uow.CarTypes.GetAllAsync(User.GetUserId()), "Id", "Name", car.CarTypeId);
            return View(car);
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _uow.Cars
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var car = await _uow.Cars.FirstOrDefaultAsync(id, User.GetUserId());
            if (car != null)
            {
                _uow.Cars.Remove(car, User.GetUserId());
                await _uow.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarExists(Guid id)
        {
            return await _uow.Cars.ExistsAsync(id, User.GetUserId());
        }
    }
}