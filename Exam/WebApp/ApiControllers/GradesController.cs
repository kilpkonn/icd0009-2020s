using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public GradesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Grades
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.Grade>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGrades()
        {
            return await _context.Grades.ToListAsync();
        }

        // GET: api/Grades/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.Grade), StatusCodes.Status200OK)]

        public async Task<ActionResult<Grade>> GetGrade(Guid id)
        {
            var grade = await _context.Grades.FindAsync(id);

            if (grade == null)
            {
                return NotFound();
            }

            return grade;
        }

        // PUT: api/Grades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.Grade), StatusCodes.Status200OK)]

        public async Task<IActionResult> PutGrade(Guid id, PublicApi.DTO.Grade grade)
        {
            if (id != grade.Id)
            {
                return BadRequest();
            }

            Grade entity = new()
            {
                Id = grade.Id,
                GradeType = grade.GradeType,
                Value = grade.Value
            };
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
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

        // POST: api/Grades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.Grade), StatusCodes.Status200OK)]

        public async Task<ActionResult<Grade>> PostGrade(PublicApi.DTO.Grade grade)
        {
            Grade entity = new()
            {
                Id = new Guid(),
                GradeType = grade.GradeType,
                Value = grade.Value
            };
            _context.Grades.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrade", new { id = entity.Id }, grade);
        }

        // DELETE: api/Grades/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(Guid id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradeExists(Guid id)
        {
            return _context.Grades.Any(e => e.Id == id);
        }
    }
}
