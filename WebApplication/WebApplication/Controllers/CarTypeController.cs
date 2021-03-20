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

namespace WebApplication.Controllers
{
    [Authorize]
    public class CarTypeController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CarTypeController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CarType
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _uow.CarTypes.GetAllAsync(null));
        }

        // GET: CarType/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _uow.CarTypes
                .FirstOrDefaultAsync((Guid) id, null);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }

        // GET: CarType/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CarMarkId"] = new SelectList(await _uow.CarMarks.GetAllAsync(null), "Id", "Name");
            return View();
        }

        // POST: CarType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarType carType)
        {
            if (ModelState.IsValid)
            {
                _uow.CarTypes.Add(carType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CarMarkId"] = new SelectList(await _uow.CarMarks.GetAllAsync(null), "Id", "Name", carType.CarModelId);
            return View(carType);
        }

        // GET: CarType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _uow.CarTypes.FirstOrDefaultAsync((Guid) id, null);
            if (carType == null)
            {
                return NotFound();
            }

            ViewData["CarMarkId"] = new SelectList(await _uow.CarMarks.GetAllAsync(null), "Id", "Name", carType.CarModelId);
            return View(carType);
        }

        // POST: CarType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CarType carType)
        {
            if (id != carType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.CarTypes.Update(carType, null);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarTypeExists(carType.Id))
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

            ViewData["CarMarkId"] = new SelectList(await _uow.CarMarks.GetAllAsync(null), "Id", "Name", carType.CarModelId);
            return View(carType);
        }

        // GET: CarType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _uow.CarTypes
                .FirstOrDefaultAsync((Guid) id, null);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }

        // POST: CarType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carType = await _uow.CarTypes.FirstOrDefaultAsync(id, null);
            if (carType != null)
            {
                _uow.CarTypes.Remove(carType, null);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarTypeExists(Guid id)
        {
            return await _uow.CarTypes.ExistsAsync(id, null);
        }
    }
}