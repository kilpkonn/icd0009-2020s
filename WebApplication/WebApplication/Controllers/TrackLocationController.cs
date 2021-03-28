using System;
using System.Threading.Tasks;
using CarApp.BLL.App;
using CarApp.DAL.App;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;
using WebApplication.Models.TrackLocation;

namespace WebApplication.Controllers
{
    [Authorize]
    public class TrackLocationController : Controller
    {
        private readonly IAppBll _bll;

        public TrackLocationController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: TrackLocation
        public async Task<IActionResult> Index()
        {
            return View(await _bll.TrackLocations.GetAllAsync(User.GetUserId()));
        }

        // GET: TrackLocation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackLocation = await _bll.TrackLocations
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
            var vm = new CreateEditViewModel()
            {
                Tracks = new SelectList(await _bll.Tracks.GetAllAsync(User.GetUserId()), "Id", "Id")
            };
            return View(vm);
        }

        // POST: TrackLocation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel ceVm)
        {
            var trackLocation = ceVm.TrackLocation;
            if (ModelState.IsValid)
            {
                _bll.TrackLocations.Add(trackLocation!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var vm = new CreateEditViewModel()
            {
                TrackLocation = trackLocation,
                Tracks = new SelectList(await _bll.Tracks.GetAllAsync(User.GetUserId()), "Id", "Id")
            };
            return View(vm);
        }

        // GET: TrackLocation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackLocation = await _bll.TrackLocations.FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (trackLocation == null)
            {
                return NotFound();
            }

            var vm = new CreateEditViewModel()
            {
                TrackLocation = trackLocation,
                Tracks = new SelectList(await _bll.Tracks.GetAllAsync(User.GetUserId()), "Id", "Id")
            };
            return View(vm);
        }

        // POST: TrackLocation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateEditViewModel ceVm)
        {
            var trackLocation = ceVm.TrackLocation;
            if (id != (trackLocation?.Id ?? null))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var toUpdate = await _bll.TrackLocations.FirstOrDefaultAsync(id, User.GetUserId());
                    toUpdate!.Accuracy = trackLocation!.Accuracy;
                    toUpdate.Elevation = trackLocation.Elevation;
                    toUpdate.Lat = trackLocation.Lat;
                    toUpdate.Lng = trackLocation.Lng;
                    toUpdate.Rpm = trackLocation.Rpm;
                    toUpdate.Speed = trackLocation.Speed;
                    toUpdate.ElevationAccuracy = trackLocation.ElevationAccuracy;
                    toUpdate.TrackId = trackLocation.TrackId;
                    _bll.TrackLocations.Update(toUpdate, User.GetUserId());
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TrackLocationExists(trackLocation!.Id))
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

            var vm = new CreateEditViewModel()
            {
                TrackLocation = trackLocation,
                Tracks = new SelectList(await _bll.Tracks.GetAllAsync(User.GetUserId()), "Id", "Id")
            };
            return View(vm);
        }

        // GET: TrackLocation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trackLocation = await _bll.TrackLocations
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
            var trackLocation = await _bll.TrackLocations.FirstOrDefaultAsync(id, User.GetUserId());
            if (trackLocation != null)
            {
                _bll.TrackLocations.Remove(trackLocation, User.GetUserId());
                await _bll.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TrackLocationExists(Guid id)
        {
            return await _bll.TrackLocations.ExistsAsync(id, User.GetUserId());
        }
    }
}