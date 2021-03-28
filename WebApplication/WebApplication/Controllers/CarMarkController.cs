using System;
using System.Threading.Tasks;
using CarApp.BLL.App;
using CarApp.DAL.App;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CarMarkController : Controller
    {
        private readonly IAppBll _bll;

        public CarMarkController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: CarMark
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _bll.CarMarks.GetAllAsync(null));
        }

        // GET: CarMark/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMark = await _bll.CarMarks
                .FirstOrDefaultAsync((Guid) id, null);
            if (carMark == null)
            {
                return NotFound();
            }

            return View(carMark);
        }

        // GET: CarMark/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarMark/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarMark carMark)
        {
            if (ModelState.IsValid)
            {
                _bll.CarMarks.Add(carMark);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(carMark);
        }

        // GET: CarMark/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMark = await _bll.CarMarks.FirstOrDefaultAsync((Guid) id, null);
            if (carMark == null)
            {
                return NotFound();
            }

            return View(carMark);
        }

        // POST: CarMark/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CarMark carMark)
        {
            if (id != carMark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.CarMarks.Update(carMark, null);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarMarkExists(carMark.Id))
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

            return View(carMark);
        }

        // GET: CarMark/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carMark = await _bll.CarMarks
                .FirstOrDefaultAsync((Guid) id, null);
            if (carMark == null)
            {
                return NotFound();
            }

            return View(carMark);
        }

        // POST: CarMark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carMark = await _bll.CarMarks.FirstOrDefaultAsync(id, null);
            if (carMark != null)
            {
                _bll.CarMarks.Remove(carMark, null);
                await _bll.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarMarkExists(Guid id)
        {
            return await _bll.CarMarks.ExistsAsync(id, null);
        }
    }
}