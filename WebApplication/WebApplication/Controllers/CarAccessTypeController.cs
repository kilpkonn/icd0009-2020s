using System;
using System.Threading.Tasks;
using CarApp.BLL.App;
using CarApp.DAL.App;
using Domain.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Controllers
{
    public class CarAccessTypeController : Controller
    {
        private readonly IAppBll _bll;

        public CarAccessTypeController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: CarAccessType
        public async Task<IActionResult> Index()
        {
            return View(await _bll.CarAccessTypes.GetAllAsync(null));
        }

        // GET: CarAccessType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carAccessType = await _bll.CarAccessTypes
                .FirstOrDefaultAsync((Guid) id, null);
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
        public async Task<IActionResult> Create(CarAccessType carAccessType)
        {
            if (ModelState.IsValid)
            {
                _bll.CarAccessTypes.Add(carAccessType);
                await _bll.SaveChangesAsync();
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

            var carAccessType = await _bll.CarAccessTypes.FirstOrDefaultAsync((Guid) id, null);
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
        public async Task<IActionResult> Edit(Guid id, CarAccessType carAccessType)
        {
            if (id != carAccessType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.CarAccessTypes.Update(carAccessType, null);
                    await _bll.SaveChangesAsync();
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

            var carAccessType = await _bll.CarAccessTypes
                .FirstOrDefaultAsync((Guid) id, null);
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
            var carAccessType = await _bll.CarAccessTypes.FirstOrDefaultAsync(id, null);
            if (carAccessType != null)
            {
                _bll.CarAccessTypes.Remove(carAccessType, null);
                await _bll.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarAccessTypeExists(Guid id)
        {
            return await _bll.CarAccessTypes.ExistsAsync(id, null);
        }
    }
}
