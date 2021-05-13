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
    /// Car Mark controller for managing car marks
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarMarkController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarMarkMapper _mapper;
        private readonly NewCarMarkMapper _newCarMarkMapper;

        /// <summary>
        /// Car Mark Controller
        /// </summary>
        /// <param name="bll">BLL</param>
        /// <param name="mapper">DTO Mapper</param>
        public CarMarkController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarMarkMapper(mapper);
            _newCarMarkMapper = new NewCarMarkMapper(mapper);
        }

        // GET: api/CarMark
        /// <summary>
        /// Get all car marks 
        /// </summary>
        /// <returns>Car marks</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarMark>), StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CarMark>>> GetCarMarks()
        {
            return (await _bll.CarMarks.GetAllAsync(null))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/CarMark/5
        /// <summary>
        /// Get Detailed data about car mark 
        /// </summary>
        /// <param name="id">Car mark id</param>
        /// <returns>Car mark</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarMark>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CarMark>> GetCarMark(Guid id)
        {
            var carMark = await _bll.CarMarks.FirstOrDefaultAsync(id, null);

            if (carMark == null)
            {
                return NotFound();
            }

            return _mapper.Map(carMark)!;
        }

        // PUT: api/CarMark/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update car mark
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="carMark">Car mark</param>
        /// <returns>Http response</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarMark(Guid id, CarMark carMark)
        {
            if (id != carMark.Id)
            {
                return BadRequest();
            }

            await _bll.CarMarks.UpdateAsync(_mapper.Map(carMark)!, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.CarMarks.ExistsAsync(id, User.GetUserId()))
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

        // POST: api/CarMark
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add new car mark
        /// </summary>
        /// <param name="carMark">Car mark to add</param>
        /// <returns>Car mark</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarMark>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<CarMark>> PostCarMark(NewCarMark carMark)
        {
            var res = _mapper.Map(await _bll.CarMarks.AddAsync(_newCarMarkMapper.Map(carMark)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarMark", new {id = res.Id}, res);
        }

        // DELETE: api/CarMark/5
        /// <summary>
        /// Delete car mark
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>No content</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarMark(Guid id)
        {
            var carMark = await _bll.CarMarks.FirstOrDefaultAsync(id, User.GetUserId());
            if (carMark == null)
            {
                return NotFound();
            }

            _bll.CarMarks.Remove(carMark, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}