using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarApp.BLL.App;
using CarApp.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BLL.App.DTO;
using Microsoft.AspNetCore.Authorization;
using WebApplication.Helpers;
using WebApplication.Models.Car;

namespace WebApplication.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly IAppBll _bll;

        public CarController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!));
        }

        // GET: Car/Details/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel ceVm)
        {
            var car = ceVm.Car;
            if (ModelState.IsValid)
            {
                car!.AppUserId = (Guid) User.GetUserId()!;
                car!.UpdatedBy = (Guid) User.GetUserId()!;
                car!.CreatedBy = (Guid) User.GetUserId()!;
                _bll.Cars.Add(car);

                var carAccess = new CarAccess()
                {
                    Car = car,
                    AppUserId = (Guid) User.GetUserId()!,
                    CarAccessType = await _bll.CarAccessTypes.FindByNameAsync("Owner")
                };
                _bll.CarAccesses.Add(carAccess);
                
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateEditViewModel ceVm)
        {
            var car = ceVm.Car;
            if (id != (car?.Id ?? Guid.Empty))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var toChange = await _bll.Cars.FirstOrDefaultAsync(id, User.GetUserId());
                    toChange!.CarTypeId = car!.CarTypeId;
                    toChange!.UpdatedAt = DateTime.Now;
                    toChange!.UpdatedBy = (Guid) User.GetUserId()!;
                    _bll.Cars.Update(toChange, User.GetUserId());
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var car = await _bll.Cars.FirstOrDefaultAsync(id, User.GetUserId());
            if (car != null)
            {
                _bll.Cars.Remove(car, User.GetUserId());
                await _bll.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarExists(Guid id)
        {
            return await _bll.Cars.ExistsAsync(id, User.GetUserId());
        }
    }
}