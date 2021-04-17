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
    /// Car Access Type controller for managing car access types
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarAccessTypeController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarAccessTypeMapper _mapper;

        /// <summary>
        /// Car Access Type Controller
        /// </summary>
        /// <param name="bll">BLL</param>
        /// <param name="mapper">DTO Mapper</param>
        public CarAccessTypeController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarAccessTypeMapper(mapper);
        }

        // GET: api/CarAccessType
        /// <summary>
        /// Get all car access types 
        /// </summary>
        /// <returns>Car access types</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarAccessType>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAccessType>>> GetCarAccessTypes()
        {
            return (await _bll.CarAccessTypes.GetAllAsync(null))
                .Select(x => _mapper.Map(x)).ToList()!;
        }

        // GET: api/CarAccessType/5
        /// <summary>
        /// Get Detailed data about car access type
        /// </summary>
        /// <param name="id">Car access type id</param>
        /// <returns>Car access type</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarAccessType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CarAccessType>> GetCarAccessType(Guid id)
        {
            var carAccessType = await _bll.CarAccessTypes.FirstOrDefaultAsync(id, null);

            if (carAccessType == null)
            {
                return NotFound();
            }

            return _mapper.Map(carAccessType)!;
        }

        // PUT: api/CarAccessType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update car access type
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="carAccessType">Car access type</param>
        /// <returns>Http response</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarAccessType(Guid id, CarAccessType carAccessType)
        {
            if (id != carAccessType.Id)
            {
                return BadRequest();
            }

            await _bll.CarAccessTypes.UpdateAsync(_mapper.Map(carAccessType)!, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.CarAccesses.ExistsAsync(id, null))
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
        /// <summary>
        /// Add new car access type
        /// </summary>
        /// <param name="carAccessType">Car access type to add</param>
        /// <returns>Car access type</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarAccessType>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<CarAccessType>> PostCarAccessType(CarAccessType carAccessType)
        {
            carAccessType =
                _mapper.Map(await _bll.CarAccessTypes.AddAsync(_mapper.Map(carAccessType)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarAccessType", new {id = carAccessType.Id}, carAccessType);
        }

        // DELETE: api/CarAccessType/5
        /// <summary>
        /// Delete car access type
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>No content</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarAccessType(Guid id)
        {
            var carAccessType = await _bll.CarAccessTypes.FirstOrDefaultAsync(id, User.GetUserId());
            if (carAccessType == null)
            {
                return NotFound();
            }

            _bll.CarAccessTypes.Remove(carAccessType, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}