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
    public class TrackLocationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrackLocationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TrackLocation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackLocation>>> GetTrackLocations()
        {
            return await _context.TrackLocations.ToListAsync();
        }

        // GET: api/TrackLocation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackLocation>> GetTrackLocation(Guid id)
        {
            var trackLocation = await _context.TrackLocations.FindAsync(id);

            if (trackLocation == null)
            {
                return NotFound();
            }

            return trackLocation;
        }

        // PUT: api/TrackLocation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrackLocation(Guid id, TrackLocation trackLocation)
        {
            if (id != trackLocation.Id)
            {
                return BadRequest();
            }

            _context.Entry(trackLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackLocationExists(id))
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

        // POST: api/TrackLocation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrackLocation>> PostTrackLocation(TrackLocation trackLocation)
        {
            _context.TrackLocations.Add(trackLocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrackLocation", new { id = trackLocation.Id }, trackLocation);
        }

        // DELETE: api/TrackLocation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackLocation(Guid id)
        {
            var trackLocation = await _context.TrackLocations.FindAsync(id);
            if (trackLocation == null)
            {
                return NotFound();
            }

            _context.TrackLocations.Remove(trackLocation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrackLocationExists(Guid id)
        {
            return _context.TrackLocations.Any(e => e.Id == id);
        }
    }
}
