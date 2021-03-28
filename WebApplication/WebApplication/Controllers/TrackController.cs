using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarApp.BLL.App;
using CarApp.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using WebApplication.Helpers;
using WebApplication.Models.Track;

namespace WebApplication.Controllers
{
    [Authorize]
    public class TrackController : Controller
    {
        private readonly IAppBll _bll;

        public TrackController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: Track
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Tracks.GetAllAsync(User.GetUserId()));
        }

        // GET: Track/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _bll.Tracks
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
            var vm = new CreateEditViewModel()
            {
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // POST: Track/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel ceVm)
        {
            var track = ceVm.Track;
            if (ModelState.IsValid)
            {
                track!.AppUserId = (Guid) User.GetUserId()!;
                _bll.Tracks.Add(track);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var vm = new CreateEditViewModel()
            {
                Track = track,
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // GET: Track/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _bll.Tracks.FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (track == null)
            {
                return NotFound();
            }
            var vm = new CreateEditViewModel()
            {
                Track = track,
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // POST: Track/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateEditViewModel ceVm)
        {
            var track = ceVm.Track;
            if (id != (track?.Id ?? null))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var toUpdate = await _bll.Tracks.FirstOrDefaultAsync(id, User.GetUserId());
                    toUpdate!.CarId = track!.CarId;
                    toUpdate.Distance = track.Distance;
                    toUpdate.StartTimestamp = track.StartTimestamp;
                    toUpdate.EndTimestamp = track.EndTimestamp;
                    toUpdate.AppUserId = (Guid) User.GetUserId()!;
                    _bll.Tracks.Update(toUpdate, User.GetUserId());
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TrackExists(track!.Id))
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
                Track = track,
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // GET: Track/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _bll.Tracks
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
            var track = await _bll.Tracks.FirstOrDefaultAsync(id, User.GetUserId());
            if (track != null)
            {
                _bll.Tracks.Remove(track, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TrackExists(Guid id)
        {
            return await _bll.Tracks.ExistsAsync(id, User.GetUserId());
        }
    }
}
