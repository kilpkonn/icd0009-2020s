using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using CarApp.BLL.App;
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
            var type = ceVm.CarType!;
            if (ModelState.IsValid)
            {
                await _bll.CarTypes.AddAsync(type, User.GetUserId());
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var vm = new CreateEditViewModel()
            {
                CarType = type,
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

            var carType = await _bll.CarTypes.FirstOrDefaultAsync((Guid) id, User.GetUserId());
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
            var type = ceVm.CarType;
            if (id != type?.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.CarTypes.UpdateAsync(type, null);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarTypeExists(type.Id))
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
                CarType = type,
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
            await _bll.CarTypes.RemoveAsync(id, null);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarTypeExists(Guid id)
        {
            return await _bll.CarTypes.ExistsAsync(id, null);
        }
    }
}