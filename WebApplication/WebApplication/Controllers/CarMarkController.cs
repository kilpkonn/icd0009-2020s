using System;
using System.Threading.Tasks;
using CarApp.BLL.App;
using BLL.App.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;

namespace WebApplication.Controllers
{
    /// <inheritdoc />
    [Authorize(Roles = "Admin")]
    public class CarMarkController : Controller
    {
        private readonly IAppBll _bll;

        /// <inheritdoc />
        public CarMarkController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: CarMark
        /// <summary>
        /// Car mark index
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _bll.CarMarks.GetAllAsync(null));
        }

        // GET: CarMark/Details/5
        /// <summary>
        /// Car mark details
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

            var carMark = await _bll.CarMarks
                .FirstOrDefaultAsync((Guid) id, null);
            if (carMark == null)
            {
                return NotFound();
            }

            return View(carMark);
        }

        // GET: CarMark/Create
        /// <summary>
        /// Create car mark
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarMark/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Post create car mark
        /// </summary>
        /// <param name="carMark"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarMark carMark)
        {
            if (ModelState.IsValid)
            {
                await _bll.CarMarks.AddAsync(carMark, User.GetUserId());
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(carMark);
        }

        // GET: CarMark/Edit/5
        /// <summary>
        /// Edit car mark
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Post edit car mark
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carMark"></param>
        /// <returns></returns>
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
                    await _bll.CarMarks.UpdateAsync(carMark, User.GetUserId());
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
        /// <summary>
        /// Delete car mark
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Post delete car mark
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.CarMarks.RemoveAsync(id, null);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarMarkExists(Guid id)
        {
            return await _bll.CarMarks.ExistsAsync(id, null);
        }
    }
}