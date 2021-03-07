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
    public class TrackLocationController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public TrackLocationController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: TrackLocation
        public async Task<IActionResult> Index()
        {
            return View(await _uow.TrackLocations.GetAllAsync());
        }

        // GET: TrackLocation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackLocation = await _uow.TrackLocations
                .FirstOrDefaultAsync((Guid) id);
            if (trackLocation == null)
            {
                return NotFound();
            }

            return View(trackLocation);
        }

        // GET: TrackLocation/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TrackId"] = new SelectList(await _uow.Tracks.GetAllAsync(), "Id", "Id");
            return View();
        }

        // POST: TrackLocation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Lat,Lng,Elevation,Accuracy,ElevationAccuracy,Speed,Rpm,TrackId")]
            TrackLocation trackLocation)
        {
            if (ModelState.IsValid)
            {
                trackLocation.Id = Guid.NewGuid();
                _uow.TrackLocations.Add(trackLocation);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["TrackId"] = new SelectList(await _uow.Tracks.GetAllAsync(), "Id", "Id", trackLocation.TrackId);
            return View(trackLocation);
        }

        // GET: TrackLocation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackLocation = await _uow.TrackLocations.FirstOrDefaultAsync((Guid) id);
            if (trackLocation == null)
            {
                return NotFound();
            }

            ViewData["TrackId"] = new SelectList(await _uow.Tracks.GetAllAsync(), "Id", "Id", trackLocation.TrackId);
            return View(trackLocation);
        }

        // POST: TrackLocation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Lat,Lng,Elevation,Accuracy,ElevationAccuracy,Speed,Rpm,TrackId")]
            TrackLocation trackLocation)
        {
            if (id != trackLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.TrackLocations.Update(trackLocation);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TrackLocationExists(trackLocation.Id))
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

            ViewData["TrackId"] = new SelectList(await _uow.Tracks.GetAllAsync(), "Id", "Id", trackLocation.TrackId);
            return View(trackLocation);
        }

        // GET: TrackLocation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackLocation = await _uow.TrackLocations
                .FirstOrDefaultAsync((Guid) id);
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
            var trackLocation = await _uow.TrackLocations.FirstOrDefaultAsync(id);
            if (trackLocation != null)
            {
                _uow.TrackLocations.Remove(trackLocation);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TrackLocationExists(Guid id)
        {
            return await _uow.TrackLocations.ExistsAsync(id);
        }
    }
}