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
    public class CarAccessTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarAccessTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CarAccessType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAccessType>>> GetCarAccessTypes()
        {
            return await _context.CarAccessTypes.ToListAsync();
        }

        // GET: api/CarAccessType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarAccessType>> GetCarAccessType(Guid id)
        {
            var carAccessType = await _context.CarAccessTypes.FindAsync(id);

            if (carAccessType == null)
            {
                return NotFound();
            }

            return carAccessType;
        }

        // PUT: api/CarAccessType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarAccessType(Guid id, CarAccessType carAccessType)
        {
            if (id != carAccessType.Id)
            {
                return BadRequest();
            }

            _context.Entry(carAccessType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarAccessTypeExists(id))
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

        // POST: api/CarAccessType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarAccessType>> PostCarAccessType(CarAccessType carAccessType)
        {
            _context.CarAccessTypes.Add(carAccessType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarAccessType", new { id = carAccessType.Id }, carAccessType);
        }

        // DELETE: api/CarAccessType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarAccessType(Guid id)
        {
            var carAccessType = await _context.CarAccessTypes.FindAsync(id);
            if (carAccessType == null)
            {
                return NotFound();
            }

            _context.CarAccessTypes.Remove(carAccessType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarAccessTypeExists(Guid id)
        {
            return _context.CarAccessTypes.Any(e => e.Id == id);
        }
    }
}
