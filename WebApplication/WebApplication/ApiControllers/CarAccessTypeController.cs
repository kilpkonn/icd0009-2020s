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
    public class CarAccessTypeController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarAccessTypeMapper _mapper;

        public CarAccessTypeController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarAccessTypeMapper(mapper);
        }

        // GET: api/CarAccessType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAccessType>>> GetCarAccessTypes()
        {
            return (await _bll.CarAccessTypes.GetAllAsync(null))
                .Select(x => _mapper.Map(x)).ToList()!;
        }

        // GET: api/CarAccessType/5
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarAccessType(Guid id, CarAccessType carAccessType)
        {
            if (id != carAccessType.Id)
            {
                return BadRequest();
            }
            
            _bll.CarAccessTypes.Update(_mapper.Map(carAccessType)!, User.GetUserId());

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
        [HttpPost]
        public async Task<ActionResult<CarAccessType>> PostCarAccessType(CarAccessType carAccessType)
        {
            carAccessType = _mapper.Map(_bll.CarAccessTypes.Add(_mapper.Map(carAccessType)!))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarAccessType", new { id = carAccessType.Id }, carAccessType);
        }

        // DELETE: api/CarAccessType/5
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
