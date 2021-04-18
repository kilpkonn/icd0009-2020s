using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;

namespace WebApplication.Controllers
{
    /// <inheritdoc />
    [Authorize(Roles = "Admin")]
    public class CarAccessTypeController : Controller
    {
        private readonly IAppBll _bll;

        /// <inheritdoc />
        public CarAccessTypeController(IAppBll bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Car access type index
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        // GET: CarAccessType
        public async Task<IActionResult> Index()
        {
            return View(await _bll.CarAccessTypes.GetAllAsync(null));
        }

        /// <summary>
        /// Get car access type details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
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
        /// <summary>
        /// Get create car access
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarAccessType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Post create car access type
        /// </summary>
        /// <param name="carAccessType"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarAccessType carAccessType)
        {
            if (ModelState.IsValid)
            {
                await _bll.CarAccessTypes.AddAsync(carAccessType, User.GetUserId());
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(carAccessType);
        }

        // GET: CarAccessType/Edit/5
        /// <summary>
        /// Edit car access type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Post edit car access type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carAccessType"></param>
        /// <returns></returns>
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
                    await _bll.CarAccessTypes.UpdateAsync(carAccessType, null);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CarAccessTypeExists(carAccessType.Id))
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
        /// <summary>
        /// Delete car access type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Post delete car access type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.CarAccessTypes.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarAccessTypeExists(Guid id)
        {
            return await _bll.CarAccessTypes.ExistsAsync(id, null);
        }
    }
}