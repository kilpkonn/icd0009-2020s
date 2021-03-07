using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarApp.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using Domain.App;

namespace WebApplication.Controllers
{
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
            return View(await _uow.Cars.GetAllAsync());
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _uow.Cars.FirstOrDefaultAsync((Guid) id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CarModelId"] = new SelectList(await _uow.CarModels.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AddedAt,CarModelId")] Domain.App.Car car)
        {
            if (ModelState.IsValid)
            {
                car.Id = Guid.NewGuid();
                _uow.Cars.Add(car);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CarModelId"] = new SelectList(await _uow.CarModels.GetAllAsync(), "Id", "Name", car.CarModelId);
            return View(car);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _uow.Cars.FirstOrDefaultAsync((Guid) id);
            if (car == null)
            {
                return NotFound();
            }

            ViewData["CarModelId"] = new SelectList(await _uow.CarModels.GetAllAsync(), "Id", "Name", car.CarModelId);
            return View(car);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AddedAt,CarModelId")] Domain.App.Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Cars.Update(car);
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

            ViewData["CarModelId"] = new SelectList(await _uow.CarModels.GetAllAsync(), "Id", "Name", car.CarModelId);
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
                .FirstOrDefaultAsync((Guid) id);
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
            var car = await _uow.Cars.FirstOrDefaultAsync(id);
            if (car != null)
            {
                _uow.Cars.Remove(car);
                await _uow.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarExists(Guid id)
        {
            return await _uow.Cars.ExistsAsync(id);
        }
    }
}