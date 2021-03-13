using System;
using System.Threading.Tasks;
using CarApp.DAL.App;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;

namespace WebApplication.Controllers
{
    [Authorize]
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
            return View(await _uow.TrackLocations.GetAllAsync(User.GetUserId()));
        }

        // GET: TrackLocation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackLocation = await _uow.TrackLocations
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (trackLocation == null)
            {
                return NotFound();
            }

            return View(trackLocation);
        }

        // GET: TrackLocation/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TrackId"] = new SelectList(await _uow.Tracks.GetAllAsync(User.GetUserId()), "Id", "Id");
            return View();
        }

        // POST: TrackLocation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrackLocation trackLocation)
        {
            if (ModelState.IsValid)
            {
                _uow.TrackLocations.Add(trackLocation);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["TrackId"] = new SelectList(await _uow.Tracks.GetAllAsync(User.GetUserId()), "Id", "Id", trackLocation.TrackId);
            return View(trackLocation);
        }

        // GET: TrackLocation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackLocation = await _uow.TrackLocations.FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (trackLocation == null)
            {
                return NotFound();
            }

            ViewData["TrackId"] = new SelectList(await _uow.Tracks.GetAllAsync(User.GetUserId()), "Id", "Id", trackLocation.TrackId);
            return View(trackLocation);
        }

        // POST: TrackLocation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TrackLocation trackLocation)
        {
            if (id != trackLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.TrackLocations.Update(trackLocation, User.GetUserId());
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

            ViewData["TrackId"] = new SelectList(await _uow.Tracks.GetAllAsync(User.GetUserId()), "Id", "Id", trackLocation.TrackId);
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
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
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
            var trackLocation = await _uow.TrackLocations.FirstOrDefaultAsync(id, User.GetUserId());
            if (trackLocation != null)
            {
                _uow.TrackLocations.Remove(trackLocation, User.GetUserId());
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TrackLocationExists(Guid id)
        {
            return await _uow.TrackLocations.ExistsAsync(id, User.GetUserId());
        }
    }
}