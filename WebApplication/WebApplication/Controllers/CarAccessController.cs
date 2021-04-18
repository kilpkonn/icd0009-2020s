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
    /// <inheritdoc />
    [Authorize]
    public class CarAccessController : Controller
    {
        private readonly IAppBll _bll;

        /// <inheritdoc />
        public CarAccessController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: CarAccess
        /// <summary>
        /// Car access index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _bll.CarAccesses.GetAllAsync(User.GetUserId()));
        }

        // GET: CarAccess/Details/5
        /// <summary>
        /// Get car access details
        /// </summary>
        /// <param name="id">Car id</param>
        /// <returns></returns>
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
        /// <summary>
        /// Get create car access
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Post create car access
        /// </summary>
        /// <param name="carAccess"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarAccess carAccess)
        {
            if (ModelState.IsValid)
            {
                await _bll.CarAccesses.AddAsync(carAccess, User.GetUserId());
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
        /// <summary>
        /// Get edit car access
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Post edit car
        /// </summary>
        /// <param name="id">Car access id</param>
        /// <param name="carAccess"></param>
        /// <returns></returns>
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
                    await _bll.CarAccesses.UpdateAsync(carAccess, userId);
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
        /// <summary>
        /// Delete car access
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Post delete car access
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.CarAccesses.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarAccessExists(Guid id)
        {
            return await _bll.CarAccesses.ExistsAsync(id, User.GetUserId());
        }
    }
}