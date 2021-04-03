using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.Mappers;
using WebApplication.Helpers;

namespace WebApplication.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarMapper _mapper;

        public CarController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarMapper(mapper);
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Car>>> GetCars()
        {
            return (await _bll.Cars.GetAccessibleCarsForUser((Guid) User.GetUserId()!))
                .Select(x => _mapper.Map(x)).ToList()!;
        }

        // GET: api/Car/5
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, PublicApi.DTO.v1.Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _bll.Cars.Update(_mapper.Map(car)!, User.GetUserId());

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

            return NoContent();
        }

        // POST: api/Car
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.Car>> PostCar(PublicApi.DTO.v1.Car car)
        {
            car = _mapper.Map(_bll.Cars.Add(_mapper.Map(car)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Car/5
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
