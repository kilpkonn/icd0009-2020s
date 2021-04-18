using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Helpers;
using WebApplication.Models.GasRefill;

namespace WebApplication.Controllers
{
    /// <inheritdoc />
    [Authorize]
    public class GasRefillController : Controller
    {
        private readonly IAppBll _bll;

        /// <inheritdoc />
        public GasRefillController(IAppBll bll)
        {
            _bll = bll;
        }

        // GET: GasRefill
        /// <summary>
        /// Gas refill index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _bll.GasRefills.GetAllAsync(User.GetUserId()));
        }

        // GET: GasRefill/Details/5
        /// <summary>
        /// Gas rtefill details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _bll.GasRefills
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (gasRefill == null)
            {
                return NotFound();
            }

            return View(gasRefill);
        }

        // GET: GasRefill/Create
        /// <summary>
        /// Create gas refill
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            var vm = new CreateEditViewModel()
            {
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // POST: GasRefill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Create gas refill
        /// </summary>
        /// <param name="ceVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEditViewModel ceVm)
        {
            var gasRefill = ceVm.GasRefill!;
            if (ModelState.IsValid)
            {
                await _bll.GasRefills.AddAsync(gasRefill, User.GetUserId());
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var vm = new CreateEditViewModel()
            {
                GasRefill = gasRefill,
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // GET: GasRefill/Edit/5
        /// <summary>
        /// Edit gas refill
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _bll.GasRefills.FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (gasRefill == null)
            {
                return NotFound();
            }

            var vm = new CreateEditViewModel()
            {
                GasRefill = gasRefill,
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // POST: GasRefill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Post edit gas refill
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ceVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CreateEditViewModel ceVm)
        {
            var gasRefill = ceVm.GasRefill;
            if (id != gasRefill?.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.GasRefills.UpdateAsync(gasRefill, User.GetUserId());
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GasRefillExists(gasRefill!.Id))
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
                GasRefill = gasRefill,
                Cars = new SelectList(await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!), "Id", "Id")
            };
            return View(vm);
        }

        // GET: GasRefill/Delete/5
        /// <summary>
        /// Delete gas refill
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasRefill = await _bll.GasRefills
                .FirstOrDefaultAsync((Guid) id, User.GetUserId());
            if (gasRefill == null)
            {
                return NotFound();
            }

            return View(gasRefill);
        }

        // POST: GasRefill/Delete/5
        /// <summary>
        /// Post delete gas refill
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.GasRefills.RemoveAsync(id, User.GetUserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GasRefillExists(Guid id)
        {
            return await _bll.GasRefills.ExistsAsync(id, User.GetUserId());
        }
    }
}