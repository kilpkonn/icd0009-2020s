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
    public class TrackController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly TrackMapper _mapper;

        public TrackController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new TrackMapper(mapper);
        }

        // GET: api/Track
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
        {
            return (await _bll.Tracks.GetAllAsync(User.GetUserId()))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/Track/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Track>> GetTrack(Guid id)
        {
            var track = await _bll.Tracks.FirstOrDefaultAsync(id, User.GetUserId());

            if (track == null)
            {
                return NotFound();
            }

            return _mapper.Map(track)!;
        }

        // PUT: api/Track/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrack(Guid id, Track track)
        {
            if (id != track.Id)
            {
                return BadRequest();
            }

            _bll.Tracks.Update(_mapper.Map(track)!, User.GetUserId());
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Tracks.ExistsAsync(id, User.GetUserId()))
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

        // POST: api/Track
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Track>> PostTrack(Track track)
        {
            track = _mapper.Map(_bll.Tracks.Add(_mapper.Map(track)!))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTrack", new { id = track.Id }, track);
        }

        // DELETE: api/Track/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrack(Guid id)
        {
            var track = await _bll.Tracks.FirstOrDefaultAsync(id, User.GetUserId());
            if (track == null)
            {
                return NotFound();
            }

            _bll.Tracks.Remove(track, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
