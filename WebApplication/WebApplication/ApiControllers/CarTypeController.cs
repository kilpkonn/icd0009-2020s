using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using WebApplication.Helpers;

namespace WebApplication.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarTypeController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarTypeMapper _mapper;

        public CarTypeController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarTypeMapper(mapper);
        }

        // GET: api/CarType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarType>>> GetCarTypes()
        {
            return (await _bll.CarTypes.GetAllAsync(null))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/CarType/5
        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarType(Guid id, CarType carType)
        {
            if (id != carType.Id)
            {
                return BadRequest();
            }

            _bll.CarTypes.Update(_mapper.Map(carType)!, User.GetUserId());

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
        [HttpPost]
        public async Task<ActionResult<CarType>> PostCarType(CarType carType)
        {
            carType = _mapper.Map(_bll.CarTypes.Add(_mapper.Map(carType)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarType", new { id = carType.Id }, carType);
        }

        // DELETE: api/CarType/5
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
