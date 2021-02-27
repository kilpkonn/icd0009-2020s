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
    public class GasRefillController : Controller
    {
        private readonly AppDbContext _context;

        public GasRefillController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GasRefill
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.GasRefills.Include(g => g.Car);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GasRefill/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _context.GasRefills
                .Include(g => g.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gasRefill == null)
            {
                return NotFound();
            }

            return View(gasRefill);
        }

        // GET: GasRefill/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
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
                _context.Add(gasRefill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", gasRefill.CarId);
            return View(gasRefill);
        }

        // GET: GasRefill/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _context.GasRefills.FindAsync(id);
            if (gasRefill == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", gasRefill.CarId);
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
                    _context.Update(gasRefill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GasRefillExists(gasRefill.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", gasRefill.CarId);
            return View(gasRefill);
        }

        // GET: GasRefill/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _context.GasRefills
                .Include(g => g.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var gasRefill = await _context.GasRefills.FindAsync(id);
            _context.GasRefills.Remove(gasRefill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GasRefillExists(Guid id)
        {
            return _context.GasRefills.Any(e => e.Id == id);
        }
    }
}
