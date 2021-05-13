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
using PublicApi.DTO.v1.Mappers;
using WebApplication.Helpers;

namespace WebApplication.ApiControllers
{
    /// <summary>
    /// Car controller for managing cars 
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarMapper _mapper;
        private readonly NewCarMapper _newCarMapper;

        /// <summary>
        /// Car Controller
        /// </summary>
        /// <param name="bll">BLL</param>
        /// <param name="mapper">DTO Mapper</param>
        public CarController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarMapper(mapper);
            _newCarMapper = new NewCarMapper(mapper);
        }

        // GET: api/Car
        /// <summary>
        /// Get all cars for user 
        /// </summary>
        /// <returns>Cars</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Car>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Car>>> GetCars()
        {
            return (await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!))
                .Select(x => _mapper.Map(x)).ToList()!;
        }

        // GET: api/Car/5
        /// <summary>
        /// Get Detailed data about car
        /// </summary>
        /// <param name="id">Car id</param>
        /// <returns>Car</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.DTO.v1.Car>> GetCar(Guid id)
        {
            var car = await _bll.Cars.FirstOrDefaultAsync(id, User.GetUserId());

            if (car == null)
            {
                return NotFound();
            }

            return _mapper.Map(car)!;
        }

        // PUT: api/Car/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update car
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="car">Car</param>
        /// <returns>Http response</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, PublicApi.DTO.v1.Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            car = _mapper.Map(await _bll.Cars.UpdateAsync(_mapper.Map(car)!, User.GetUserId()))!;

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Cars.ExistsAsync(id, User.GetUserId()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(car);
        }

        // POST: api/Car
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add new car
        /// </summary>
        /// <param name="car">Car to add</param>
        /// <returns>Car access</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.v1.Car>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.Car>> PostCar(PublicApi.DTO.v1.NewCar car)
        {
            var res = _mapper.Map(await _bll.Cars.AddAsync(_newCarMapper.Map(car)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCar", new {id = res.Id}, res);
        }

        // DELETE: api/Car/5
        /// <summary>
        /// Delete car
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>No content</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            var car = await _bll.Cars.FirstOrDefaultAsync(id, User.GetUserId());
            if (car == null)
            {
                return NotFound();
            }

            _bll.Cars.Remove(car, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}