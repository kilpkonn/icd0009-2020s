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
    public class CarTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CarType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarType>>> GetCarTypes()
        {
            return await _context.CarTypes.ToListAsync();
        }

        // GET: api/CarType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarType>> GetCarType(Guid id)
        {
            var carType = await _context.CarTypes.FindAsync(id);

            if (carType == null)
            {
                return NotFound();
            }

            return carType;
        }

        // PUT: api/CarType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarType(Guid id, CarType carType)
        {
            if (id != carType.Id)
            {
                return BadRequest();
            }

            _context.Entry(carType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarTypeExists(id))
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

        // POST: api/CarType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarType>> PostCarType(CarType carType)
        {
            _context.CarTypes.Add(carType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarType", new { id = carType.Id }, carType);
        }

        // DELETE: api/CarType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarType(Guid id)
        {
            var carType = await _context.CarTypes.FindAsync(id);
            if (carType == null)
            {
                return NotFound();
            }

            _context.CarTypes.Remove(carType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarTypeExists(Guid id)
        {
            return _context.CarTypes.Any(e => e.Id == id);
        }
    }
}
