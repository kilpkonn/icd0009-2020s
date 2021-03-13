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
using Microsoft.AspNetCore.Authorization;
using WebApplication.Helpers;

namespace WebApplication.Controllers
{
    [Authorize]
    public class CarErrorCodeController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CarErrorCodeController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CarErrorCode
        public async Task<IActionResult> Index()
        {
            return View(await _uow.CarErrorCodes.GetAllAsync(User.GetUserId()));
        }

        // GET: CarErrorCode/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carErrorCode = await _uow.CarErrorCodes
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (carErrorCode == null)
            {
                return NotFound();
            }

            return View(carErrorCode);
        }

        // GET: CarErrorCode/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarErrorCode/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarErrorCode carErrorCode)
        {
            if (ModelState.IsValid)
            {
                _uow.CarErrorCodes.Add(carErrorCode);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(carErrorCode);
        }

        // GET: CarErrorCode/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carErrorCode = await _uow.CarErrorCodes.FirstOrDefaultAsync((Guid) id, null);
            if (carErrorCode == null)
            {
                return NotFound();
            }

            return View(carErrorCode);
        }

        // POST: CarErrorCode/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CarErrorCode carErrorCode)
        {
            if (id != carErrorCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.CarErrorCodes.Update(carErrorCode, null);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarErrorCodeExists(carErrorCode.Id))
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

            return View(carErrorCode);
        }

        // GET: CarErrorCode/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carErrorCode = await _uow.CarErrorCodes
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (carErrorCode == null)
            {
                return NotFound();
            }

            return View(carErrorCode);
        }

        // POST: CarErrorCode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carErrorCode = await _uow.CarErrorCodes.FirstOrDefaultAsync(id, User.GetUserId());
            if (carErrorCode != null)
            {
                _uow.CarErrorCodes.Remove(carErrorCode, User.GetUserId());
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarErrorCodeExists(Guid id)
        {
            return await _uow.CarErrorCodes.ExistsAsync(id, User.GetUserId());
        }
    }
}