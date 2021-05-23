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
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SubjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.Subject>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            return await _context.Subjects.ToListAsync();
        }

        // GET: api/Subjects/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.Subject), StatusCodes.Status200OK)]

        public async Task<ActionResult<Subject>> GetSubject(Guid id)
        {
            var subject = await _context.Subjects
                .Include(x => x.Homeworks)
                .Include(x => x.Declarations)
                .FirstOrDefaultAsync(x => x.Id ==id);

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.Subject), StatusCodes.Status200OK)]

        public async Task<IActionResult> PutSubject(Guid id, PublicApi.DTO.NewSubject subject)
        {
            if (id != subject.Id)
            {
                return BadRequest();
            }

            Subject entity = new()
            {
                Id = subject.Id,
                Title = subject.Title,
                Description = subject.Description,
            };

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.Subject), StatusCodes.Status200OK)]

        public async Task<ActionResult<Subject>> PostSubject(PublicApi.DTO.NewSubject subject)
        {
            Subject entity = new()
            {
                Id = subject.Id,
                Title = subject.Title,
                Description = subject.Description,
            };
            _context.Subjects.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubject", new { id = entity.Id }, subject);
        }

        // DELETE: api/Subjects/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectExists(Guid id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}
