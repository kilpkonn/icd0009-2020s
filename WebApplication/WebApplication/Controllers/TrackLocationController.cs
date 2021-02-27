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
    public class TrackLocationController : Controller
    {
        private readonly AppDbContext _context;

        public TrackLocationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TrackLocation
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TrackLocations.Include(t => t.Track);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TrackLocation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackLocation = await _context.TrackLocations
                .Include(t => t.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trackLocation == null)
            {
                return NotFound();
            }

            return View(trackLocation);
        }

        // GET: TrackLocation/Create
        public IActionResult Create()
        {
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id");
            return View();
        }

        // POST: TrackLocation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Lat,Lng,Elevation,Accuracy,ElevationAccuracy,Speed,Rpm,TrackId")] TrackLocation trackLocation)
        {
            if (ModelState.IsValid)
            {
                trackLocation.Id = Guid.NewGuid();
                _context.Add(trackLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", trackLocation.TrackId);
            return View(trackLocation);
        }

        // GET: TrackLocation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackLocation = await _context.TrackLocations.FindAsync(id);
            if (trackLocation == null)
            {
                return NotFound();
            }
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", trackLocation.TrackId);
            return View(trackLocation);
        }

        // POST: TrackLocation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Lat,Lng,Elevation,Accuracy,ElevationAccuracy,Speed,Rpm,TrackId")] TrackLocation trackLocation)
        {
            if (id != trackLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trackLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackLocationExists(trackLocation.Id))
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
            ViewData["TrackId"] = new SelectList(_context.Tracks, "Id", "Id", trackLocation.TrackId);
            return View(trackLocation);
        }

        // GET: TrackLocation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackLocation = await _context.TrackLocations
                .Include(t => t.Track)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trackLocation == null)
            {
                return NotFound();
            }

            return View(trackLocation);
        }

        // POST: TrackLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var trackLocation = await _context.TrackLocations.FindAsync(id);
            _context.TrackLocations.Remove(trackLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackLocationExists(Guid id)
        {
            return _context.TrackLocations.Any(e => e.Id == id);
        }
    }
}
