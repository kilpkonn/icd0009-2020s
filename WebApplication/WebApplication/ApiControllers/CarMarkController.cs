using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using Domain;

namespace WebApplication.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarMarkController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarMarkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CarMark
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarMark>>> GetCarMarks()
        {
            return await _context.CarMarks.ToListAsync();
        }

        // GET: api/CarMark/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarMark>> GetCarMark(Guid id)
        {
            var carMark = await _context.CarMarks.FindAsync(id);

            if (carMark == null)
            {
                return NotFound();
            }

            return carMark;
        }

        // PUT: api/CarMark/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarMark(Guid id, CarMark carMark)
        {
            if (id != carMark.Id)
            {
                return BadRequest();
            }

            _context.Entry(carMark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarMarkExists(id))
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

        // POST: api/CarMark
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarMark>> PostCarMark(CarMark carMark)
        {
            _context.CarMarks.Add(carMark);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarMark", new { id = carMark.Id }, carMark);
        }

        // DELETE: api/CarMark/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarMark(Guid id)
        {
            var carMark = await _context.CarMarks.FindAsync(id);
            if (carMark == null)
            {
                return NotFound();
            }

            _context.CarMarks.Remove(carMark);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarMarkExists(Guid id)
        {
            return _context.CarMarks.Any(e => e.Id == id);
        }
    }
}
