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
    public class GasRefillController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly GasRefillMapper _mapper;

        public GasRefillController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new GasRefillMapper(mapper);
        }

        // GET: api/GasRefill
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GasRefill>>> GetGasRefills()
        {
            return (await _bll.GasRefills.GetAllAsync(User.GetUserId()))
                .Select(x => _mapper.Map(x))
                .ToList()!;
        }

        // GET: api/GasRefill/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GasRefill>> GetGasRefill(Guid id)
        {
            var gasRefill = await _bll.GasRefills.FirstOrDefaultAsync(id, User.GetUserId());

            if (gasRefill == null)
            {
                return NotFound();
            }

            return _mapper.Map(gasRefill)!;
        }

        // PUT: api/GasRefill/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGasRefill(Guid id, GasRefill gasRefill)
        {
            if (id != gasRefill.Id)
            {
                return BadRequest();
            }

            _bll.GasRefills.Update(_mapper.Map(gasRefill)!, User.GetUserId());

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.GasRefills.ExistsAsync(id, User.GetUserId()))
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

        // POST: api/GasRefill
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GasRefill>> PostGasRefill(GasRefill gasRefill)
        {
            _bll.GasRefills.Add(_mapper.Map(gasRefill)!, User.GetUserId());
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetGasRefill", new { id = gasRefill.Id }, gasRefill);
        }

        // DELETE: api/GasRefill/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasRefill(Guid id)
        {
            var gasRefill = await _bll.GasRefills.FirstOrDefaultAsync(id, User.GetUserId());
            if (gasRefill == null)
            {
                return NotFound();
            }

            _bll.GasRefills.Remove(gasRefill, User.GetUserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
