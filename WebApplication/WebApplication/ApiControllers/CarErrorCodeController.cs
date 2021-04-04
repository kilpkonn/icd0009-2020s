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
    public class CarErrorCodeController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarErrorCodeMapper _mapper;

        public CarErrorCodeController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarErrorCodeMapper(mapper);
        }

        // GET: api/CarErrorCode
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarErrorCode>>> GetCarErrorCodes()
        {
            return (await _bll.CarErrorCodes.GetAllAsync(User.GetUserId()))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/CarErrorCode/5
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
        [HttpPost]
        public async Task<ActionResult<CarErrorCode>> PostCarErrorCode(CarErrorCode carErrorCode)
        {
            carErrorCode = _mapper.Map(await _bll.CarErrorCodes.AddAsync(_mapper.Map(carErrorCode)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarErrorCode", new { id = carErrorCode.Id }, carErrorCode);
        }

        // DELETE: api/CarErrorCode/5
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
