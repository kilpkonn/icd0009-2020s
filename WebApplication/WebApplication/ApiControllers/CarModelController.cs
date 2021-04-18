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
    /// Car Model controller for managing car models
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarModelController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarModelMapper _mapper;

        /// <summary>
        /// Car Model Controller
        /// </summary>
        /// <param name="bll">BLL</param>
        /// <param name="mapper">DTO Mapper</param>
        public CarModelController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarModelMapper(mapper);
        }

        // GET: api/CarModel
        /// <summary>
        /// Get all car models 
        /// </summary>
        /// <returns>Car models</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarModel>), StatusCodes.Status200OK)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CarModel>>> GetCarModels()
        {
            return (await _bll.CarModels.GetAllAsync(null))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/CarModel/5
        /// <summary>
        /// Get Detailed data about car model 
        /// </summary>
        /// <param name="id">Car model id</param>
        /// <returns>Car model</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CarModel>> GetCarModel(Guid id)
        {
            var carModel = await _bll.CarModels.FirstOrDefaultAsync(id, null);

            if (carModel == null)
            {
                return NotFound();
            }

            return _mapper.Map(carModel)!;
        }

        // PUT: api/CarModel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update car model
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="carModel">Car model</param>
        /// <returns>Http response</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarModel(Guid id, CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return BadRequest();
            }

            await _bll.CarModels.UpdateAsync(_mapper.Map(carModel)!, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.CarModels.ExistsAsync(id, null))
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

        // POST: api/CarModel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add new car model
        /// </summary>
        /// <param name="carModel">Car model to add</param>
        /// <returns>Car model</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CarModel>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<CarModel>> PostCarModel(CarModel carModel)
        {
            carModel = _mapper.Map(await _bll.CarModels.AddAsync(_mapper.Map(carModel)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarModel", new {id = carModel.Id}, carModel);
        }

        // DELETE: api/CarModel/5
        /// <summary>
        /// Delete car model
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>No content</returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarModel(Guid id)
        {
            var carModel = await _bll.CarModels.FirstOrDefaultAsync(id, User.GetUserId());
            if (carModel == null)
            {
                return NotFound();
            }

            _bll.CarModels.Remove(carModel, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}