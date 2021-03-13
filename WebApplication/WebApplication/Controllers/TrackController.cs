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
    public class TrackController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public TrackController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Track
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Tracks.GetAllAsync(User.GetUserId()));
        }

        // GET: Track/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _uow.Tracks
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // GET: Track/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id");
            return View();
        }

        // POST: Track/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Track track)
        {
            if (ModelState.IsValid)
            {
                track.AppUserId = (Guid) User.GetUserId()!;
                _uow.Tracks.Add(track);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id", track.CarId);
            return View(track);
        }

        // GET: Track/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _uow.Tracks.FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (track == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id", track.CarId);
            return View(track);
        }

        // POST: Track/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Track track)
        {
            if (id != track.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Tracks.Update(track, User.GetUserId());
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TrackExists(track.Id))
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
            ViewData["CarId"] = new SelectList(await _uow.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id", track.CarId);
            return View(track);
        }

        // GET: Track/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _uow.Tracks
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: Track/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var track = await _uow.Tracks.FirstOrDefaultAsync(id, User.GetUserId());
            if (track != null)
            {
                _uow.Tracks.Remove(track, User.GetUserId());
                await _uow.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TrackExists(Guid id)
        {
            return await _uow.Tracks.ExistsAsync(id, User.GetUserId());
        }
    }
}
