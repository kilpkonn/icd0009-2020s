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
using WebApplication.Models.CarModel;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarModelController : Controller
    {
        private readonly IAppBll _bll;

        public CarModelController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: CarModel
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _bll.CarModels.GetAllAsync(null));
        }

        // GET: CarModel/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _bll.CarModels
                .FirstOrDefaultAsync((Guid) id, null);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: CarModel/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CreateEditViewModel()
            {
                CarMarks = new SelectList(await _bll.CarMarks.GetAllAsync(null), "Id", "Name")
            };
            return View(vm);
        }

        // POST: CarModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel ceVm)
        {
            var carModel = ceVm.CarModel;
            if (ModelState.IsValid)
            {
                _bll.CarModels.Add(carModel!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var vm = new CreateEditViewModel()
            {
                CarMarks = new SelectList(await _bll.CarMarks.GetAllAsync(null), "Id", "Name")
            };
            return View(vm);
        }

        // GET: CarModel/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _bll.CarModels.FirstOrDefaultAsync((Guid) id, null);
            if (carModel == null)
            {
                return NotFound();
            }

            var vm = new CreateEditViewModel()
            {
                CarModel = carModel,
                CarMarks = new SelectList(await _bll.CarMarks.GetAllAsync(null), "Id", "Name")
            };
            return View(vm);
        }

        // POST: CarModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateEditViewModel ceVm)
        {
            var carModel = ceVm.CarModel;
            if (id != (carModel?.Id ?? null))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var toUpdate = await _bll.CarModels.FirstOrDefaultAsync(id, null);
                    toUpdate!.Name = carModel!.Name;
                    toUpdate.CarMarkId = carModel.CarMarkId;
                    toUpdate.ReleaseDate = carModel.ReleaseDate;
                    toUpdate.UpdatedAt = DateTime.Now;
                    toUpdate.UpdatedBy = (Guid) User.GetUserId()!;
                    _bll.CarModels.Update(toUpdate, null);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarModelExists(carModel!.Id))
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
                CarModel = carModel,
                CarMarks = new SelectList(await _bll.CarMarks.GetAllAsync(null), "Id", "Name")
            };
            return View(vm);
        }

        // GET: CarModel/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _bll.CarModels
                .FirstOrDefaultAsync((Guid) id, null);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: CarModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carModel = await _bll.CarModels.FirstOrDefaultAsync(id, null);
            if (carModel != null)
            {
                _bll.CarModels.Remove(carModel, null);
                await _bll.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarModelExists(Guid id)
        {
            return await _bll.CarModels.ExistsAsync(id, null);
        }
    }
}