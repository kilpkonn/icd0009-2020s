using System;
using System.Threading.Tasks;
using BLL.App.DTO;
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
    /// <inheritdoc />
    [Authorize]
    public class CarErrorCodeController : Controller
    {
        private readonly IAppBll _bll;

        /// <inheritdoc />
        public CarErrorCodeController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: CarErrorCode
        /// <summary>
        /// Car error code index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _bll.CarErrorCodes.GetAllAsync(User.GetUserId()));
        }

        // GET: CarErrorCode/Details/5
        /// <summary>
        /// Car error code details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Create car error code
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Post create care error code
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarErrorCode errorCode)
        {
            if (ModelState.IsValid)
            {
                await _bll.CarErrorCodes.AddAsync(errorCode, User.GetUserId());
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var vm = new CreateEditViewModel()
            {
                CarErrorCode = errorCode,
                CarOptions = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id",
                    "CarType.Name")
            };

            return View(vm);
        }

        // GET: CarErrorCode/Edit/5
        /// <summary>
        /// Edit car error code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Post edit car error code
        /// </summary>
        /// <param name="id"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CarErrorCode errorCode)
        {
            if (id != errorCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.CarErrorCodes.UpdateAsync(errorCode, null);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarErrorCodeExists(errorCode.Id))
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
                CarErrorCode = errorCode,
                CarOptions = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id",
                    "CarType.Name")
            };

            return View(vm);
        }

        // GET: CarErrorCode/Delete/5
        /// <summary>
        /// Delete car error code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Post delete car error code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.CarErrorCodes.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarErrorCodeExists(Guid id)
        {
            return await _bll.CarErrorCodes.ExistsAsync(id, User.GetUserId());
        }
    }
}