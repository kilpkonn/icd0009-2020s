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
    /// Gas Refill controller for managing gas refills
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GasRefillController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly GasRefillMapper _mapper;

        /// <summary>
        /// Gas Refill Controller
        /// </summary>
        /// <param name="bll">BLL</param>
        /// <param name="mapper">DTO Mapper</param>
        public GasRefillController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new GasRefillMapper(mapper);
        }

        // GET: api/GasRefill
        /// <summary>
        /// Get all gas refills for user
        /// </summary>
        /// <returns>Gas refills</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<GasRefill>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GasRefill>>> GetGasRefills()
        {
            return (await _bll.GasRefills.GetAllAsync(User.GetUserId()))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/GasRefill/5
        /// <summary>
        /// Get Detailed data about gas refill
        /// </summary>
        /// <param name="id">Gas refill id</param>
        /// <returns>Gas refill</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<GasRefill>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GasRefill>> GetGasRefill(Guid id)
        {
            var gasRefill = await _bll.GasRefills.FirstOrDefaultAsync(id, User.GetUserId());

            if (gasRefill == null)
            {
                return NotFound();
            }

            return _mapper.Map(gasRefill)!;
        }

        // PUT: api/GasRefill/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update gas refill
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="gasRefill">Gas refill</param>
        /// <returns>Http response</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGasRefill(Guid id, GasRefill gasRefill)
        {
            if (id != gasRefill.Id)
            {
                return BadRequest();
            }

            await _bll.GasRefills.UpdateAsync(_mapper.Map(gasRefill)!, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.GasRefills.ExistsAsync(id, User.GetUserId()))
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
        /// <summary>
        /// Add new gas refill
        /// </summary>
        /// <param name="gasRefill">Gas refill to add</param>
        /// <returns>Gas refill</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<GasRefill>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<GasRefill>> PostGasRefill(GasRefill gasRefill)
        {
            await _bll.GasRefills.AddAsync(_mapper.Map(gasRefill)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetGasRefill", new {id = gasRefill.Id}, gasRefill);
        }

        // DELETE: api/GasRefill/5
        /// <summary>
        /// Delete gas refill
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>No content</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasRefill(Guid id)
        {
            var gasRefill = await _bll.GasRefills.FirstOrDefaultAsync(id, User.GetUserId());
            if (gasRefill == null)
            {
                return NotFound();
            }

            _bll.GasRefills.Remove(gasRefill, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}