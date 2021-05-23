using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using Extensions;
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
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects(Guid? semesterId = null)
        {
            return await _context.Subjects
                .Where(x => semesterId == null || x.SemesterId == semesterId)
                .ToListAsync();
        }

        // GET: api/Subjects/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.Subject), StatusCodes.Status200OK)]

        public async Task<ActionResult<PublicApi.DTO.Subject>> GetSubject(Guid id)
        {
            var subject = await _context.Subjects
                .Include(x => x.Homeworks)
                .ThenInclude(x => x.Submissions)
                .ThenInclude(x => x.Grade)
                .Include(x => x.Declarations)
                .ThenInclude(x => x.Grade)
                .FirstOrDefaultAsync(x => x.Id ==id);

            if (subject == null)
            {
                return NotFound();
            }

            return new PublicApi.DTO.Subject()
            {
                Id = subject.Id,
                SemesterId = subject.SemesterId,
                Title = subject.Title,
                Description = subject.Description,
                Declarations = subject.Declarations!.Select(x => new PublicApi.DTO.Declaration()
                {
                    Id = x.Id,
                    SubjectId = x.SubjectId,
                    AppUserId = x.AppUserId,
                    GradeId = x.GradeId,
                    Grade = x.Grade != null ? new PublicApi.DTO.Grade()
                    {
                        GradeType = x.Grade!.GradeType,
                    } : null,
                    DeclarationStatus = x.DeclarationStatus,
                }).ToList(),
                Homeworks = subject.Homeworks!.Select(x => new PublicApi.DTO.Homework()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    SubjectId = x.SubjectId,
                    Submissions = x.Submissions!.Select(s => new PublicApi.DTO.Submission()
                    {
                        Id = s.Id,
                        HomeworkId = s.HomeworkId,
                        GradeId = s.GradeId,
                        Grade = s.Grade != null ? new PublicApi.DTO.Grade()
                        {
                            Value = s.Grade!.Value
                        } : null,
                    }).ToList()
                }).ToList()
            };
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

            Subject entity = _context.Subjects.Find(subject.Id);
            entity.Title = subject.Title;
            entity.Description = subject.Description;

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
            if (_context.Subjects.Where(x => x.SemesterId == subject.SemesterId)
                .FirstOrDefault(x => x.Title == subject.Title) != null)
            {
                return BadRequest();
            }
            
            Subject entity = new()
            {
                Id = new Guid(),
                Title = subject.Title,
                Description = subject.Description,
                SemesterId = subject.SemesterId
            };

            _context.Subjects.Add(entity);
            _context.LecturerSubjects.Add(new LecturerSubject()
            {
                AppUserId = (Guid) User.GetUserId()!,
                Subject = entity,
            });

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
