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
    public class CarMarkController : Controller
    {
        private readonly AppDbContext _context;

        public CarMarkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CarMark
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarMarks.ToListAsync());
        }

        // GET: CarMark/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMark = await _context.CarMarks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carMark == null)
            {
                return NotFound();
            }

            return View(carMark);
        }

        // GET: CarMark/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarMark/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CarMark carMark)
        {
            if (ModelState.IsValid)
            {
                carMark.Id = Guid.NewGuid();
                _context.Add(carMark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carMark);
        }

        // GET: CarMark/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMark = await _context.CarMarks.FindAsync(id);
            if (carMark == null)
            {
                return NotFound();
            }
            return View(carMark);
        }

        // POST: CarMark/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] CarMark carMark)
        {
            if (id != carMark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carMark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarMarkExists(carMark.Id))
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
            return View(carMark);
        }

        // GET: CarMark/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMark = await _context.CarMarks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carMark == null)
            {
                return NotFound();
            }

            return View(carMark);
        }

        // POST: CarMark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carMark = await _context.CarMarks.FindAsync(id);
            _context.CarMarks.Remove(carMark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarMarkExists(Guid id)
        {
            return _context.CarMarks.Any(e => e.Id == id);
        }
    }
}
