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
    /// Track Location controller for managing track locations
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrackLocationController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly TrackLocationMapper _mapper;
        private readonly NewTrackLocationMapper _newTrackLocationMapper;

        /// <summary>
        /// Track Location Controller
        /// </summary>
        /// <param name="bll">BLL</param>
        /// <param name="mapper">DTO Mapper</param>
        public TrackLocationController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new TrackLocationMapper(mapper);
            _newTrackLocationMapper = new NewTrackLocationMapper(mapper);
        }

        // GET: api/TrackLocation
        /// <summary>
        /// Get all track locations for user
        /// </summary>
        /// <returns>Track Locations</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TrackLocation>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackLocation>>> GetTrackLocations()
        {
            return (await _bll.TrackLocations.GetAllAsync(User.GetUserId()))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/TrackLocation/5
        /// <summary>
        /// Get Detailed data about track location
        /// </summary>
        /// <param name="id">Track location id</param>
        /// <returns>Track location</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TrackLocation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Update track location
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="trackLocation">Track location</param>
        /// <returns>Http response</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Add new track location
        /// </summary>
        /// <param name="trackLocation">Track location to add</param>
        /// <returns>Track location</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TrackLocation>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<TrackLocation>> PostTrackLocation(NewTrackLocation trackLocation)
        {
            var res =
                _mapper.Map(await _bll.TrackLocations.AddAsync(_newTrackLocationMapper.Map(trackLocation)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTrackLocation", new {id = res.Id}, res);
        }

        // DELETE: api/TrackLocation/5
        /// <summary>
        /// Delete track location
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>No content</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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