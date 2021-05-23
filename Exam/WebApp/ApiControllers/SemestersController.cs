using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <inheritdoc />
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class SemestersController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SemestersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Semesters
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.Semester>>> GetSemesters()
        {
            return await _context.Semesters
                .Select(x => new PublicApi.DTO.Semester()
                {
                    Id = x.Id,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                })
                .ToListAsync();
        }

        // // GET: api/Semesters/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Semester>> GetSemester(Guid id)
        // {
        //     var semester = await _context.Semesters.FindAsync(id);
        //
        //     if (semester == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return semester;
        // }
        //
        // // PUT: api/Semesters/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutSemester(Guid id, Semester semester)
        // {
        //     if (id != semester.Id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(semester).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!SemesterExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }
        //
        // // POST: api/Semesters
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Semester>> PostSemester(Semester semester)
        // {
        //     _context.Semesters.Add(semester);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetSemester", new { id = semester.Id }, semester);
        // }
        //
        // // DELETE: api/Semesters/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteSemester(Guid id)
        // {
        //     var semester = await _context.Semesters.FindAsync(id);
        //     if (semester == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Semesters.Remove(semester);
        //     await _context.SaveChangesAsync();
        //
        //     return NoContent();
        // }
        //
        // private bool SemesterExists(Guid id)
        // {
        //     return _context.Semesters.Any(e => e.Id == id);
        // }
    }
}
