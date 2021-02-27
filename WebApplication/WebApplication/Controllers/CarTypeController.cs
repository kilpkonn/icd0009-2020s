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
    public class CarTypeController : Controller
    {
        private readonly AppDbContext _context;

        public CarTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CarType
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CarTypes.Include(c => c.CarMark);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CarType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _context.CarTypes
                .Include(c => c.CarMark)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }

        // GET: CarType/Create
        public IActionResult Create()
        {
            ViewData["CarMarkId"] = new SelectList(_context.CarMarks, "Id", "Name");
            return View();
        }

        // POST: CarType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CarMarkId")] CarType carType)
        {
            if (ModelState.IsValid)
            {
                carType.Id = Guid.NewGuid();
                _context.Add(carType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarMarkId"] = new SelectList(_context.CarMarks, "Id", "Name", carType.CarMarkId);
            return View(carType);
        }

        // GET: CarType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _context.CarTypes.FindAsync(id);
            if (carType == null)
            {
                return NotFound();
            }
            ViewData["CarMarkId"] = new SelectList(_context.CarMarks, "Id", "Name", carType.CarMarkId);
            return View(carType);
        }

        // POST: CarType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CarMarkId")] CarType carType)
        {
            if (id != carType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarTypeExists(carType.Id))
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
            ViewData["CarMarkId"] = new SelectList(_context.CarMarks, "Id", "Name", carType.CarMarkId);
            return View(carType);
        }

        // GET: CarType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _context.CarTypes
                .Include(c => c.CarMark)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var carType = await _context.CarTypes.FindAsync(id);
            _context.CarTypes.Remove(carType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarTypeExists(Guid id)
        {
            return _context.CarTypes.Any(e => e.Id == id);
        }
    }
}
