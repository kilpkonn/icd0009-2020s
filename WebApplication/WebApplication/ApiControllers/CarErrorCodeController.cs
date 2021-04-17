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
    /// Car controller for managing cars 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarErrorCodeController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarErrorCodeMapper _mapper;

        /// <summary>
        /// Car Controller
        /// </summary>
        /// <param name="bll">BLL</param>
        /// <param name="mapper">DTO Mapper</param>
        public CarErrorCodeController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarErrorCodeMapper(mapper);
        }

        // GET: api/CarErrorCode
        /// <summary>
        /// Get all car error codes for user 
        /// </summary>
        /// <returns>Car error codes</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarErrorCode>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarErrorCode>>> GetCarErrorCodes()
        {
            return (await _bll.CarErrorCodes.GetAllAsync(User.GetUserId()))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/CarErrorCode/5
        /// <summary>
        /// Get Detailed data about car error code 
        /// </summary>
        /// <param name="id">Car error code id</param>
        /// <returns>Car error code</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarErrorCode>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CarErrorCode>> GetCarErrorCode(Guid id)
        {
            var carErrorCode = await _bll.CarErrorCodes.FirstOrDefaultAsync(id, User.GetUserId());

            if (carErrorCode == null)
            {
                return NotFound();
            }

            return _mapper.Map(carErrorCode)!;
        }

        // PUT: api/CarErrorCode/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update car error code
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="carErrorCode">Car error code</param>
        /// <returns>Http response</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarErrorCode(Guid id, CarErrorCode carErrorCode)
        {
            if (id != carErrorCode.Id)
            {
                return BadRequest();
            }

            await _bll.CarErrorCodes.UpdateAsync(_mapper.Map(carErrorCode)!, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.CarErrorCodes.ExistsAsync(id, User.GetUserId()))
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

        // POST: api/CarErrorCode
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add new car error code access
        /// </summary>
        /// <param name="carErrorCode">Car error code to add</param>
        /// <returns>Car access</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarErrorCode>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<CarErrorCode>> PostCarErrorCode(CarErrorCode carErrorCode)
        {
            carErrorCode =
                _mapper.Map(await _bll.CarErrorCodes.AddAsync(_mapper.Map(carErrorCode)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarErrorCode", new {id = carErrorCode.Id}, carErrorCode);
        }

        // DELETE: api/CarErrorCode/5
        /// <summary>
        /// Delete car error code
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>No content</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarErrorCode(Guid id)
        {
            var carErrorCode = await _bll.CarErrorCodes.FirstOrDefaultAsync(id, User.GetUserId());
            if (carErrorCode == null)
            {
                return NotFound();
            }

            _bll.CarErrorCodes.Remove(carErrorCode, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}