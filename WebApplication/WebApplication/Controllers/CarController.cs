using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;
using WebApplication.Models.Car;

namespace WebApplication.Controllers
{
    /// <inheritdoc />
    [Authorize]
    public class CarController : Controller
    {
        private readonly IAppBll _bll;

        /// <inheritdoc />
        public CarController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: Car
        /// <summary>
        /// Car index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!));
        }

        // GET: Car/Details/5
        /// <summary>
        /// Get car details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _bll.Cars.FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        /// <summary>
        /// Create car
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            var vm = new CreateEditViewModel()
            {
                CarTypeOptions = new SelectList(await _bll.CarTypes.GetAllAsync(User.GetUserId()), "Id", "Name")
            };
            return View(vm);
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Post create car
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Car car)
        {
            if (ModelState.IsValid)
            {
                await _bll.Cars.AddAsync(car, User.GetUserId());
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var vm = new CreateEditViewModel()
            {
                CarTypeOptions = new SelectList(await _bll.CarTypes.GetAllAsync(User.GetUserId()), "Id", "Name")
            };
            return View(vm);
        }

        // GET: Car/Edit/5
        /// <summary>
        /// Get edit car
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _bll.Cars.FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (car == null)
            {
                return NotFound();
            }

            var vm = new CreateEditViewModel()
            {
                Car = car,
                CarTypeOptions = new SelectList(await _bll.CarTypes.GetAllAsync(User.GetUserId()), "Id", "Name")
            };
            return View(vm);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Post edit car
        /// </summary>
        /// <param name="id"></param>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.Cars.UpdateAsync(car, User.GetUserId());
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarExists(car!.Id))
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
                Car = car,
                CarTypeOptions = new SelectList(await _bll.CarTypes.GetAllAsync(User.GetUserId()), "Id", "Name")
            };
            return View(vm);
        }

        // GET: Car/Delete/5
        /// <summary>
        /// Delete car
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _bll.Cars
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Delete/5
        /// <summary>
        /// Post delete car
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Cars.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarExists(Guid id)
        {
            return await _bll.Cars.ExistsAsync(id, User.GetUserId());
        }
    }
}