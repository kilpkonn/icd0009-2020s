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
    /// Car Type controller for managing car types
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarTypeController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarTypeMapper _mapper;
        private readonly NewCarTypeMapper _newCarTypeMapper;

        /// <summary>
        /// Car Type Controller
        /// </summary>
        /// <param name="bll">BLL</param>
        /// <param name="mapper">DTO Mapper</param>
        public CarTypeController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarTypeMapper(mapper);
            _newCarTypeMapper = new NewCarTypeMapper(mapper);
        }

        // GET: api/CarType
        /// <summary>
        /// Get all car types
        /// </summary>
        /// <returns>Car types</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarType>), StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CarType>>> GetCarTypes()
        {
            return (await _bll.CarTypes.GetAllAsync(null))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/CarType/5
        /// <summary>
        /// Get Detailed data about car type 
        /// </summary>
        /// <param name="id">Car type id</param>
        /// <returns>Car type</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CarType>> GetCarType(Guid id)
        {
            var carType = await _bll.CarTypes.FirstOrDefaultAsync(id, null);

            if (carType == null)
            {
                return NotFound();
            }

            return _mapper.Map(carType)!;
        }

        // PUT: api/CarType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update car type
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="carType">Car type</param>
        /// <returns>Http response</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarType(Guid id, CarType carType)
        {
            if (id != carType.Id)
            {
                return BadRequest();
            }

            await _bll.CarTypes.UpdateAsync(_mapper.Map(carType)!, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.CarTypes.ExistsAsync(id, null))
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

        // POST: api/CarType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add new car access
        /// </summary>
        /// <param name="carType">Car type to add</param>
        /// <returns>Car type</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarType>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<CarType>> PostCarType(NewCarType carType)
        {
            var res = _mapper.Map(await _bll.CarTypes.AddAsync(_newCarTypeMapper.Map(carType)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarType", new {id = res.Id}, res);
        }

        // DELETE: api/CarType/5
        /// <summary>
        /// Delete car type
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>No content</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarType(Guid id)
        {
            var carType = await _bll.CarTypes.FirstOrDefaultAsync(id, User.GetUserId());
            if (carType == null)
            {
                return NotFound();
            }

            _bll.CarTypes.Remove(carType, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}