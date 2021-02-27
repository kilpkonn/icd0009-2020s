using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using Domain.App;

namespace WebApplication.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarAccessController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarAccessController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CarAccess
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAccess>>> GetCarAccesses()
        {
            return await _context.CarAccesses.ToListAsync();
        }

        // GET: api/CarAccess/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarAccess>> GetCarAccess(Guid id)
        {
            var carAccess = await _context.CarAccesses.FindAsync(id);

            if (carAccess == null)
            {
                return NotFound();
            }

            return carAccess;
        }

        // PUT: api/CarAccess/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarAccess(Guid id, CarAccess carAccess)
        {
            if (id != carAccess.Id)
            {
                return BadRequest();
            }

            _context.Entry(carAccess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarAccessExists(id))
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

        // POST: api/CarAccess
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarAccess>> PostCarAccess(CarAccess carAccess)
        {
            _context.CarAccesses.Add(carAccess);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarAccess", new { id = carAccess.Id }, carAccess);
        }

        // DELETE: api/CarAccess/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarAccess(Guid id)
        {
            var carAccess = await _context.CarAccesses.FindAsync(id);
            if (carAccess == null)
            {
                return NotFound();
            }

            _context.CarAccesses.Remove(carAccess);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarAccessExists(Guid id)
        {
            return _context.CarAccesses.Any(e => e.Id == id);
        }
    }
}
