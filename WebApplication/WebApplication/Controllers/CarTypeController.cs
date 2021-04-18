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
    /// <inheritdoc />
    [Authorize(Roles = "Admin")]
    public class CarTypeController : Controller
    {
        private readonly IAppBll _bll;

        /// <inheritdoc />
        public CarTypeController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: CarType
        /// <summary>
        /// Car type index
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _bll.CarTypes.GetAllAsync(null));
        }

        // GET: CarType/Details/5
        /// <summary>
        /// Car type details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Create car type
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Post create car type
        /// </summary>
        /// <param name="ceVm"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get edit car type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Post edit car type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ceVm"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Delete car type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Post delete car type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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