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
    /// Car Access controller for managing car accesses
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarAccessController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarAccessMapper _mapper;

        /// <summary>
        /// Car Access Controller
        /// </summary>
        /// <param name="bll">BLL</param>
        /// <param name="mapper">DTO Mapper</param>
        public CarAccessController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarAccessMapper(mapper);
        }

        // GET: api/CarAccess
        /// <summary>
        /// Get All car accesses for user 
        /// </summary>
        /// <returns>Car accesses</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarAccess>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAccess>>> GetCarAccesses()
        {
            return (await _bll.CarAccesses.GetAllAsync(User.GetUserId()))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/CarAccess/5
        /// <summary>
        /// Get Detailed data about car access 
        /// </summary>
        /// <param name="id">Car access id</param>
        /// <returns>Car access</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarAccess>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CarAccess>> GetCarAccess(Guid id)
        {
            var carAccess = await _bll.CarAccesses.FirstOrDefaultAsync(id, User.GetUserId());

            if (carAccess == null)
            {
                return NotFound();
            }

            return _mapper.Map(carAccess)!;
        }

        // PUT: api/CarAccess/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Car Access
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="carAccess">Car access</param>
        /// <returns>Http response</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarAccess(Guid id, CarAccess carAccess)
        {
            if (id != carAccess.Id)
            {
                return BadRequest();
            }

            await _bll.CarAccesses.UpdateAsync(_mapper.Map(carAccess)!, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.CarAccesses.ExistsAsync(id, User.GetUserId()))
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
        /// <summary>
        /// Add new car access
        /// </summary>
        /// <param name="carAccess">Car access to add</param>
        /// <returns>Car access</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarAccess>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<CarAccess>> PostCarAccess(CarAccess carAccess)
        {
            await _bll.CarAccesses.AddAsync(_mapper.Map(carAccess)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarAccess", new {id = carAccess.Id}, carAccess);
        }

        // DELETE: api/CarAccess/5
        /// <summary>
        /// Delete car access
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>No content</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarAccess(Guid id)
        {
            var carAccess = await _bll.CarAccesses.FirstOrDefaultAsync(id, User.GetUserId());
            if (carAccess == null)
            {
                return NotFound();
            }

            _bll.CarAccesses.Remove(carAccess, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}