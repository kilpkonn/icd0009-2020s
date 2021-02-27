using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using Domain.App;

namespace WebApplication.Controllers
{
    public class CarAccessController : Controller
    {
        private readonly AppDbContext _context;

        public CarAccessController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CarAccess
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CarAccesses.Include(c => c.Car).Include(c => c.CarAccessType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CarAccess/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccess = await _context.CarAccesses
                .Include(c => c.Car)
                .Include(c => c.CarAccessType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carAccess == null)
            {
                return NotFound();
            }

            return View(carAccess);
        }

        // GET: CarAccess/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
            ViewData["CarAccessTypeId"] = new SelectList(_context.CarAccessTypes, "Id", "Name");
            return View();
        }

        // POST: CarAccess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CarId,CarAccessTypeId")] CarAccess carAccess)
        {
            if (ModelState.IsValid)
            {
                carAccess.Id = Guid.NewGuid();
                _context.Add(carAccess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carAccess.CarId);
            ViewData["CarAccessTypeId"] = new SelectList(_context.CarAccessTypes, "Id", "Name", carAccess.CarAccessTypeId);
            return View(carAccess);
        }

        // GET: CarAccess/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccess = await _context.CarAccesses.FindAsync(id);
            if (carAccess == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carAccess.CarId);
            ViewData["CarAccessTypeId"] = new SelectList(_context.CarAccessTypes, "Id", "Name", carAccess.CarAccessTypeId);
            return View(carAccess);
        }

        // POST: CarAccess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,CarId,CarAccessTypeId")] CarAccess carAccess)
        {
            if (id != carAccess.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carAccess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarAccessExists(carAccess.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carAccess.CarId);
            ViewData["CarAccessTypeId"] = new SelectList(_context.CarAccessTypes, "Id", "Name", carAccess.CarAccessTypeId);
            return View(carAccess);
        }

        // GET: CarAccess/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccess = await _context.CarAccesses
                .Include(c => c.Car)
                .Include(c => c.CarAccessType)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var carAccess = await _context.CarAccesses.FindAsync(id);
            _context.CarAccesses.Remove(carAccess);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarAccessExists(Guid id)
        {
            return _context.CarAccesses.Any(e => e.Id == id);
        }
    }
}
