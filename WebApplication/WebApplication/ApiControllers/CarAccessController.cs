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
    public class CarAccessController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarAccessMapper _mapper;

        public CarAccessController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarAccessMapper(mapper);
        }

        // GET: api/CarAccess
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAccess>>> GetCarAccesses()
        {
            return (await _bll.CarAccesses.GetAllAsync(User.GetUserId()))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/CarAccess/5
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarAccess(Guid id, CarAccess carAccess)
        {
            if (id != carAccess.Id)
            {
                return BadRequest();
            }

            _bll.CarAccesses.Update(_mapper.Map(carAccess)!, User.GetUserId());

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
        [HttpPost]
        public async Task<ActionResult<CarAccess>> PostCarAccess(CarAccess carAccess)
        {
            _bll.CarAccesses.Add(_mapper.Map(carAccess)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarAccess", new {id = carAccess.Id}, carAccess);
        }

        // DELETE: api/CarAccess/5
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