using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using WebApplication.Helpers;

namespace WebApplication.ApiControllers
{
    /// <summary>
    /// Track controller for managing tracks
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrackController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly TrackMapper _mapper;

        /// <summary>
        /// Track Controller
        /// </summary>
        /// <param name="bll">BLL</param>
        /// <param name="mapper">DTO Mapper</param>
        public TrackController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new TrackMapper(mapper);
        }

        // GET: api/Track
        /// <summary>
        /// Get all tracks for user 
        /// </summary>
        /// <returns>Tracks</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Track>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
        {
            return (await _bll.Tracks.GetAllAsync(User.GetUserId()))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/Track/5
        /// <summary>
        /// Get Detailed data about track
        /// </summary>
        /// <param name="id">Track id</param>
        /// <returns>Track</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Track>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Update Track
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="track">Track</param>
        /// <returns>Http response</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrack(Guid id, Track track)
        {
            if (id != track.Id)
            {
                return BadRequest();
            }

            await _bll.Tracks.UpdateAsync(_mapper.Map(track)!, User.GetUserId());

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
        /// <summary>
        /// Add new track
        /// </summary>
        /// <param name="track">Track to add</param>
        /// <returns>Track</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Track>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<Track>> PostTrack(Track track)
        {
            track = _mapper.Map(await _bll.Tracks.AddAsync(_mapper.Map(track)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTrack", new {id = track.Id}, track);
        }

        // DELETE: api/Track/5
        /// <summary>
        /// Delete track
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>No content</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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