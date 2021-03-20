using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApplication.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarModelController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CarModel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarModel>>> GetCarModels()
        {
            return await _context.CarModels.ToListAsync();
        }

        // GET: api/CarModel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarModel>> GetCarModel(Guid id)
        {
            var carModel = await _context.CarModels.FindAsync(id);

            if (carModel == null)
            {
                return NotFound();
            }

            return carModel;
        }

        // PUT: api/CarModel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarModel(Guid id, CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(carModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CarModel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarModel>> PostCarModel(CarModel carModel)
        {
            _context.CarModels.Add(carModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarModel", new { id = carModel.Id }, carModel);
        }

        // DELETE: api/CarModel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarModel(Guid id)
        {
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }

            _context.CarModels.Remove(carModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarModelExists(Guid id)
        {
            return _context.CarModels.Any(e => e.Id == id);
        }
    }
}
