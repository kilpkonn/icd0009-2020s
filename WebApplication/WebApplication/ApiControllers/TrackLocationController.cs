using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using WebApplication.Helpers;

namespace WebApplication.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackLocationController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly TrackLocationMapper _mapper;

        public TrackLocationController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new TrackLocationMapper(mapper);
        }

        // GET: api/TrackLocation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackLocation>>> GetTrackLocations()
        {
            return (await _bll.TrackLocations.GetAllAsync(User.GetUserId()))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/TrackLocation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackLocation>> GetTrackLocation(Guid id)
        {
            var trackLocation = await _bll.TrackLocations.FirstOrDefaultAsync(id, User.GetUserId());

            if (trackLocation == null)
            {
                return NotFound();
            }

            return _mapper.Map(trackLocation)!;
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

            await _bll.TrackLocations.UpdateAsync(_mapper.Map(trackLocation)!, User.GetUserId());
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.TrackLocations.ExistsAsync(id, User.GetUserId()))
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
            trackLocation = _mapper.Map(await _bll.TrackLocations.AddAsync(_mapper.Map(trackLocation)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTrackLocation", new { id = trackLocation.Id }, trackLocation);
        }

        // DELETE: api/TrackLocation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackLocation(Guid id)
        {
            var trackLocation = await _bll.TrackLocations.FirstOrDefaultAsync(id, User.GetUserId());
            if (trackLocation == null)
            {
                return NotFound();
            }

            _bll.TrackLocations.Remove(trackLocation, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
