using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarApp.BLL.App;
using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using WebApplication.Helpers;

namespace WebApplication.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly CarModelMapper _mapper;

        public CarModelController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new CarModelMapper(mapper);
        }

        // GET: api/CarModel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarModel>>> GetCarModels()
        {
            return (await _bll.CarModels.GetAllAsync(null))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/CarModel/5
        [HttpGet("{id}")]
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
        [HttpPost]
        public async Task<ActionResult<CarModel>> PostCarModel(CarModel carModel)
        {
            carModel = _mapper.Map(await _bll.CarModels.AddAsync(_mapper.Map(carModel)!, User.GetUserId()))!;
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCarModel", new { id = carModel.Id }, carModel);
        }

        // DELETE: api/CarModel/5
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
