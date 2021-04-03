using System;
using System.Threading.Tasks;
using CarApp.BLL.App;
using BLL.App.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;
using WebApplication.Models.CarAccess;

namespace WebApplication.Controllers
{
    [Authorize]
    public class CarAccessController : Controller
    {
        private readonly IAppBll _bll;

        public CarAccessController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: CarAccess
        public async Task<IActionResult> Index()
        {
            return View(await _bll.CarAccesses.GetAllAsync(User.GetUserId()));
        }

        // GET: CarAccess/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var carAccess = await _bll.CarAccesses.FirstOrDefaultAsync((Guid) id!, User.GetUserId());
            if (carAccess == null)
            {
                return NotFound();
            }

            return View(carAccess);
        }

        // GET: CarAccess/Create
        public async Task<IActionResult> Create()
        {
            var userId = User.GetUserId();
            var vm = new CreateEditViewModel()
            {
                CarOptions = new SelectList(await _bll.Cars.GetAllAsync(userId), "Id", "Id"),
                CarAccessTypeOptions = new SelectList(await _bll.CarAccessTypes.GetAllAsync(userId), "Id", "Name")
            };
            return View(vm);
        }

        // POST: CarAccess/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel ceVm)
        {
            if (ModelState.IsValid)
            {
                _bll.CarAccesses.Add(ceVm.CarAccess!, User.GetUserId());
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var userId = (Guid) User!.GetUserId()!;
            var vm = new CreateEditViewModel()
            {
                CarOptions = new SelectList(await _bll.Cars.GetAllAsync(userId), "Id", "Id"),
                CarAccessTypeOptions = new SelectList(await _bll.CarAccessTypes.GetAllAsync(userId), "Id", "Name")
            };
            return View(vm);
        }

        // GET: CarAccess/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = (Guid) User!.GetUserId()!;
            var carAccess = await _bll.CarAccesses.FirstOrDefaultAsync((Guid) id, userId);
            if (carAccess == null)
            {
                return NotFound();
            }

            var vm = new CreateEditViewModel()
            {
                CarAccess = carAccess,
                CarOptions = new SelectList(await _bll.Cars.GetAllAsync(userId), "Id", "Id"),
                CarAccessTypeOptions = new SelectList(await _bll.CarAccessTypes.GetAllAsync(userId), "Id", "Name")
            };
            return View(vm);
        }

        // POST: CarAccess/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CarAccess carAccess)
        {
            if (id != carAccess.Id)
            {
                return NotFound();
            }
            var userId = User.GetUserId();
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.CarAccesses.Update(carAccess, userId);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarAccessExists(carAccess.Id))
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
                CarOptions = new SelectList(await _bll.Cars.GetAllAsync(userId), "Id", "Id"),
                CarAccessTypeOptions = new SelectList(await _bll.CarAccessTypes.GetAllAsync(userId), "Id", "Name")
            };
            return View(vm);
        }

        // GET: CarAccess/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccess = await _bll.CarAccesses.FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (carAccess == null)
            {
                return NotFound();
            }

            return View(carAccess);
        }

        // POST: CarAccess/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userId = User.GetUserId();
            var carAccess = await _bll.CarAccesses.FirstOrDefaultAsync(id, userId);
            if (carAccess != null)
            {
                _bll.CarAccesses.Remove(carAccess, User.GetUserId());
                await _bll.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarAccessExists(Guid id)
        {
            return await _bll.CarAccesses.ExistsAsync(id, User.GetUserId());
        }
    }
}