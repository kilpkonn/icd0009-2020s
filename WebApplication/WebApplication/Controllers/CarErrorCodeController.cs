using System;
using System.Threading.Tasks;
using CarApp.BLL.App;
using CarApp.DAL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;
using WebApplication.Models.CarErrorCode;

namespace WebApplication.Controllers
{
    [Authorize]
    public class CarErrorCodeController : Controller
    {
        private readonly IAppBll _bll;

        public CarErrorCodeController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: CarErrorCode
        public async Task<IActionResult> Index()
        {
            return View(await _bll.CarErrorCodes.GetAllAsync(User.GetUserId()));
        }

        // GET: CarErrorCode/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carErrorCode = await _bll.CarErrorCodes
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (carErrorCode == null)
            {
                return NotFound();
            }

            return View(carErrorCode);
        }

        // GET: CarErrorCode/Create
        public async Task<IActionResult> Create()
        {
            var vm = new CreateEditViewModel()
            {
                CarOptions = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id",
                    "CarType.Name")
            };

            return View(vm);
        }

        // POST: CarErrorCode/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel ceVm)
        {
            var carErrorCode = ceVm.CarErrorCode;
            if (ModelState.IsValid)
            {
                carErrorCode!.CreatedBy = (Guid) User.GetUserId()!;
                carErrorCode!.UpdatedBy = (Guid) User.GetUserId()!;
                _bll.CarErrorCodes.Add(carErrorCode);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var vm = new CreateEditViewModel()
            {
                CarErrorCode = carErrorCode,
                CarOptions = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id",
                    "CarType.Name")
            };

            return View(vm);
        }

        // GET: CarErrorCode/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carErrorCode = await _bll.CarErrorCodes.FirstOrDefaultAsync((Guid) id, null);
            if (carErrorCode == null)
            {
                return NotFound();
            }

            var vm = new CreateEditViewModel()
            {
                CarErrorCode = carErrorCode,
                CarOptions = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id",
                    "CarType.Name")
            };

            return View(vm);
        }

        // POST: CarErrorCode/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateEditViewModel ceVm)
        {
            var carErrorCode = ceVm.CarErrorCode;
            if (id != (carErrorCode?.Id ?? null))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var toUpdate = await _bll.CarErrorCodes.FirstOrDefaultAsync(id, User.GetUserId());
                    toUpdate!.CarId = carErrorCode!.CarId;
                    toUpdate.CanData = carErrorCode.CanData;
                    toUpdate.UpdatedAt = DateTime.Now;
                    toUpdate.UpdatedBy = (Guid) User.GetUserId()!;
                    _bll.CarErrorCodes.Update(toUpdate, null);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarErrorCodeExists(carErrorCode!.Id))
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
                CarErrorCode = carErrorCode,
                CarOptions = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id",
                    "CarType.Name")
            };

            return View(vm);
        }

        // GET: CarErrorCode/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carErrorCode = await _bll.CarErrorCodes
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (carErrorCode == null)
            {
                return NotFound();
            }

            return View(carErrorCode);
        }

        // POST: CarErrorCode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carErrorCode = await _bll.CarErrorCodes.FirstOrDefaultAsync(id, User.GetUserId());
            if (carErrorCode != null)
            {
                _bll.CarErrorCodes.Remove(carErrorCode, User.GetUserId());
                await _bll.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarErrorCodeExists(Guid id)
        {
            return await _bll.CarErrorCodes.ExistsAsync(id, User.GetUserId());
        }
    }
}