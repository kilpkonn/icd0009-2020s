using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain;
using Extensions;
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
    public class SubmissionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public SubmissionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Submissions
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.Submission>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Submission>>> GetSubmissions()
        {
            return await _context.Submissions.ToListAsync();
        }

        // GET: api/Submissions/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.Submission), StatusCodes.Status200OK)]
        public async Task<ActionResult<Submission>> GetSubmission(Guid id)
        {
            var submission = await _context.Submissions.FindAsync(id);

            if (submission == null)
            {
                return NotFound();
            }

            return submission;
        }

        // PUT: api/Submissions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="submission"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.Submission), StatusCodes.Status200OK)]
        public async Task<IActionResult> PutSubmission(Guid id, PublicApi.DTO.NewSubmission submission)
        {
            if (id != submission.Id)
            {
                return BadRequest();
            }

            Submission entity = new()
            {
                Id = submission.Id,
                AppUserId = submission.AppUserId,
                HomeworkId = submission.HomeworkId,
                Value = submission.Value,
                GradeId = submission.GradeId
            };
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubmissionExists(id))
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

        // POST: api/Submissions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// 
        /// </summary>
        /// <param name="submission"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.Submission), StatusCodes.Status200OK)]
        public async Task<ActionResult<Submission>> PostSubmission(PublicApi.DTO.NewSubmission submission)
        {
            Submission entity = new()
            {
                Id = submission.Id,
                AppUserId = submission.AppUserId,
                HomeworkId = submission.HomeworkId,
                Value = submission.Value,
                GradeId = submission.GradeId
            };
            _context.Submissions.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubmission", new {id = submission.Id}, submission);
        }

        // DELETE: api/Submissions/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubmission(Guid id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null)
            {
                return NotFound();
            }

            _context.Submissions.Remove(submission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubmissionExists(Guid id)
        {
            return _context.Submissions.Any(e => e.Id == id);
        }
    }
}