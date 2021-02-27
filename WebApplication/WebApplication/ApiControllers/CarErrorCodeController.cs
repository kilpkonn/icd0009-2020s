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
    public class CarErrorCodeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarErrorCodeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CarErrorCode
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarErrorCode>>> GetCarErrorCodes()
        {
            return await _context.CarErrorCodes.ToListAsync();
        }

        // GET: api/CarErrorCode/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarErrorCode>> GetCarErrorCode(Guid id)
        {
            var carErrorCode = await _context.CarErrorCodes.FindAsync(id);

            if (carErrorCode == null)
            {
                return NotFound();
            }

            return carErrorCode;
        }

        // PUT: api/CarErrorCode/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarErrorCode(Guid id, CarErrorCode carErrorCode)
        {
            if (id != carErrorCode.Id)
            {
                return BadRequest();
            }

            _context.Entry(carErrorCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarErrorCodeExists(id))
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

        // POST: api/CarErrorCode
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarErrorCode>> PostCarErrorCode(CarErrorCode carErrorCode)
        {
            _context.CarErrorCodes.Add(carErrorCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarErrorCode", new { id = carErrorCode.Id }, carErrorCode);
        }

        // DELETE: api/CarErrorCode/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarErrorCode(Guid id)
        {
            var carErrorCode = await _context.CarErrorCodes.FindAsync(id);
            if (carErrorCode == null)
            {
                return NotFound();
            }

            _context.CarErrorCodes.Remove(carErrorCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarErrorCodeExists(Guid id)
        {
            return _context.CarErrorCodes.Any(e => e.Id == id);
        }
    }
}
