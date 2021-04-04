using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarApp.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using WebApplication.Helpers;

namespace WebApplication.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarMarkController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarMarkMapper _mapper;

        public CarMarkController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarMarkMapper(mapper);
        }

        // GET: api/CarMark
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CarMark>>> GetCarMarks()
        {
            return (await _bll.CarMarks.GetAllAsync(null))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/CarMark/5
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
        [HttpPost]
        public async Task<ActionResult<CarMark>> PostCarMark(CarMark carMark)
        {
            carMark = _mapper.Map(await _bll.CarMarks.AddAsync(_mapper.Map(carMark)!, User.GetUserId()))!;  // TODO: Userid?
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarMark", new {id = carMark.Id}, carMark);
        }

        // DELETE: api/CarMark/5
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