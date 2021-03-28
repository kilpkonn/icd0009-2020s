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
using WebApplication.Models.CarType;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarTypeController : Controller
    {
        private readonly IAppBll _bll;

        public CarTypeController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: CarType
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _bll.CarTypes.GetAllAsync(null));
        }

        // GET: CarType/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _bll.CarTypes
                .FirstOrDefaultAsync((Guid) id, null);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }

        // GET: CarType/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CreateEditViewModel()
            {
                CarModels = new SelectList(await _bll.CarModels.GetAllAsync(null), "Id", "Name")
            };
            return View(vm);
        }

        // POST: CarType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel ceVm)
        {
            var carType = ceVm.CarType;
            if (ModelState.IsValid)
            {
                _bll.CarTypes.Add(carType!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var vm = new CreateEditViewModel()
            {
                CarType = carType,
                CarModels = new SelectList(await _bll.CarModels.GetAllAsync(null), "Id", "Name")
            };
            return View(vm);
        }

        // GET: CarType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _bll.CarTypes.FirstOrDefaultAsync((Guid) id, null);
            if (carType == null)
            {
                return NotFound();
            }

            var vm = new CreateEditViewModel()
            {
                CarType = carType,
                CarModels = new SelectList(await _bll.CarModels.GetAllAsync(null), "Id", "Name")
            };
            return View(vm);
        }

        // POST: CarType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateEditViewModel ceVm)
        {
            var carType = ceVm.CarType;
            if (id != (carType?.Id ?? null))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var toUpdate = await _bll.CarTypes.FirstOrDefaultAsync(id, null);
                    toUpdate!.Name = carType!.Name;
                    toUpdate.CarModelId = carType.CarModelId;
                    toUpdate.UpdatedAt = DateTime.Now;
                    toUpdate.UpdatedBy = (Guid) User.GetUserId()!;
                    _bll.CarTypes.Update(toUpdate, null);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarTypeExists(carType!.Id))
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
                CarType = carType,
                CarModels = new SelectList(await _bll.CarModels.GetAllAsync(null), "Id", "Name")
            };
            return View(vm);
        }

        // GET: CarType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carType = await _bll.CarTypes
                .FirstOrDefaultAsync((Guid) id, null);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }

        // POST: CarType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carType = await _bll.CarTypes.FirstOrDefaultAsync(id, null);
            if (carType != null)
            {
                _bll.CarTypes.Remove(carType, null);
                await _bll.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarTypeExists(Guid id)
        {
            return await _bll.CarTypes.ExistsAsync(id, null);
        }
    }
}