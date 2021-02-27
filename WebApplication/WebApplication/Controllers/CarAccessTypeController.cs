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
    public class CarAccessTypeController : Controller
    {
        private readonly AppDbContext _context;

        public CarAccessTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CarAccessType
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarAccessTypes.ToListAsync());
        }

        // GET: CarAccessType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccessType = await _context.CarAccessTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carAccessType == null)
            {
                return NotFound();
            }

            return View(carAccessType);
        }

        // GET: CarAccessType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarAccessType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AccessLevel")] CarAccessType carAccessType)
        {
            if (ModelState.IsValid)
            {
                carAccessType.Id = Guid.NewGuid();
                _context.Add(carAccessType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carAccessType);
        }

        // GET: CarAccessType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccessType = await _context.CarAccessTypes.FindAsync(id);
            if (carAccessType == null)
            {
                return NotFound();
            }
            return View(carAccessType);
        }

        // POST: CarAccessType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,AccessLevel")] CarAccessType carAccessType)
        {
            if (id != carAccessType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carAccessType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarAccessTypeExists(carAccessType.Id))
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
            return View(carAccessType);
        }

        // GET: CarAccessType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccessType = await _context.CarAccessTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carAccessType == null)
            {
                return NotFound();
            }

            return View(carAccessType);
        }

        // POST: CarAccessType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carAccessType = await _context.CarAccessTypes.FindAsync(id);
            _context.CarAccessTypes.Remove(carAccessType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarAccessTypeExists(Guid id)
        {
            return _context.CarAccessTypes.Any(e => e.Id == id);
        }
    }
}
