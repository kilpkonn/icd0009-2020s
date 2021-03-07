using System;
using System.Threading.Tasks;
using CarApp.DAL.App;
using Domain.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Controllers
{
    public class CarAccessTypeController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CarAccessTypeController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: CarAccessType
        public async Task<IActionResult> Index()
        {
            return View(await _uow.CarAccessTypes.GetAllAsync());
        }

        // GET: CarAccessType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccessType = await _uow.CarAccessTypes
                .FirstOrDefaultAsync((Guid) id);
            if (carAccessType == null)
            {
                return NotFound();
            }

            return View(carAccessType);
        }

        // GET: CarAccessType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarAccessType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AccessLevel")] CarAccessType carAccessType)
        {
            if (ModelState.IsValid)
            {
                carAccessType.Id = Guid.NewGuid();
                _uow.CarAccessTypes.Add(carAccessType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carAccessType);
        }

        // GET: CarAccessType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccessType = await _uow.CarAccessTypes.FirstOrDefaultAsync((Guid) id);
            if (carAccessType == null)
            {
                return NotFound();
            }
            return View(carAccessType);
        }

        // POST: CarAccessType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,AccessLevel")] CarAccessType carAccessType)
        {
            if (id != carAccessType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.CarAccessTypes.Update(carAccessType);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await CarAccessTypeExists(carAccessType.Id))
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
            return View(carAccessType);
        }

        // GET: CarAccessType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccessType = await _uow.CarAccessTypes
                .FirstOrDefaultAsync((Guid) id);
            if (carAccessType == null)
            {
                return NotFound();
            }

            return View(carAccessType);
        }

        // POST: CarAccessType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carAccessType = await _uow.CarAccessTypes.FirstOrDefaultAsync(id);
            if (carAccessType != null)
            {
                _uow.CarAccessTypes.Remove(carAccessType);
                await _uow.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarAccessTypeExists(Guid id)
        {
            return await _uow.CarAccessTypes.ExistsAsync(id);
        }
    }
}
