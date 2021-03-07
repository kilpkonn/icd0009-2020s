using System;
using System.Threading.Tasks;
using CarApp.DAL.App;
using Domain.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Controllers
{
    public class CarModelController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CarModelController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CarModel
        public async Task<IActionResult> Index()
        {
            return View(await _uow.CarModels.GetAllAsync());
        }

        // GET: CarModel/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _uow.CarModels
                .FirstOrDefaultAsync((Guid) id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: CarModel/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CarTypeId"] = new SelectList(await _uow.CarTypes.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: CarModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ReleaseDate,CarTypeId")]
            CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                carModel.Id = Guid.NewGuid();
                _uow.CarModels.Add(carModel);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CarTypeId"] = new SelectList(await _uow.CarTypes.GetAllAsync(), "Id", "Name", carModel.CarTypeId);
            return View(carModel);
        }

        // GET: CarModel/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _uow.CarModels.FirstOrDefaultAsync((Guid) id);
            if (carModel == null)
            {
                return NotFound();
            }

            ViewData["CarTypeId"] = new SelectList(await _uow.CarTypes.GetAllAsync(), "Id", "Name", carModel.CarTypeId);
            return View(carModel);
        }

        // POST: CarModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ReleaseDate,CarTypeId")]
            CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.CarModels.Update(carModel);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarModelExists(carModel.Id))
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

            ViewData["CarTypeId"] = new SelectList(await _uow.CarTypes.GetAllAsync(), "Id", "Name", carModel.CarTypeId);
            return View(carModel);
        }

        // GET: CarModel/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _uow.CarModels
                .FirstOrDefaultAsync((Guid) id);
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
            var carModel = await _uow.CarModels.FirstOrDefaultAsync(id);
            if (carModel != null)
            {
                _uow.CarModels.Remove(carModel);
                await _uow.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarModelExists(Guid id)
        {
            return await _uow.CarModels.ExistsAsync(id);
        }
    }
}