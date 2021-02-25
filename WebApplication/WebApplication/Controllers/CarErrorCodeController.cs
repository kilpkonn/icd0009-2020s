using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using Domain;

namespace WebApplication.Controllers
{
    public class CarErrorCodeController : Controller
    {
        private readonly AppDbContext _context;

        public CarErrorCodeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CarErrorCode
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarErrorCodes.ToListAsync());
        }

        // GET: CarErrorCode/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carErrorCode = await _context.CarErrorCodes
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,CanId,CanData,DetectedAt")] CarErrorCode carErrorCode)
        {
            if (ModelState.IsValid)
            {
                carErrorCode.Id = Guid.NewGuid();
                _context.Add(carErrorCode);
                await _context.SaveChangesAsync();
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

            var carErrorCode = await _context.CarErrorCodes.FindAsync(id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CanId,CanData,DetectedAt")] CarErrorCode carErrorCode)
        {
            if (id != carErrorCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carErrorCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarErrorCodeExists(carErrorCode.Id))
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

            var carErrorCode = await _context.CarErrorCodes
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var carErrorCode = await _context.CarErrorCodes.FindAsync(id);
            _context.CarErrorCodes.Remove(carErrorCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarErrorCodeExists(Guid id)
        {
            return _context.CarErrorCodes.Any(e => e.Id == id);
        }
    }
}
