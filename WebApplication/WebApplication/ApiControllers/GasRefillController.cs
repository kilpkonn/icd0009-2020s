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
    public class GasRefillController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GasRefillController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GasRefill
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GasRefill>>> GetGasRefills()
        {
            return await _context.GasRefills.ToListAsync();
        }

        // GET: api/GasRefill/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GasRefill>> GetGasRefill(Guid id)
        {
            var gasRefill = await _context.GasRefills.FindAsync(id);

            if (gasRefill == null)
            {
                return NotFound();
            }

            return gasRefill;
        }

        // PUT: api/GasRefill/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGasRefill(Guid id, GasRefill gasRefill)
        {
            if (id != gasRefill.Id)
            {
                return BadRequest();
            }

            _context.Entry(gasRefill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GasRefillExists(id))
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

        // POST: api/GasRefill
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GasRefill>> PostGasRefill(GasRefill gasRefill)
        {
            _context.GasRefills.Add(gasRefill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGasRefill", new { id = gasRefill.Id }, gasRefill);
        }

        // DELETE: api/GasRefill/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasRefill(Guid id)
        {
            var gasRefill = await _context.GasRefills.FindAsync(id);
            if (gasRefill == null)
            {
                return NotFound();
            }

            _context.GasRefills.Remove(gasRefill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GasRefillExists(Guid id)
        {
            return _context.GasRefills.Any(e => e.Id == id);
        }
    }
}
