using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO;

namespace WebApp.ApiControllers
{
    /// <inheritdoc />
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public StatsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Semesters
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SemesterStats>>> GetSemesters()
        {
            return (await _context.Semesters
                .Include(x => x.Subjects)
                .ThenInclude(x => x.Declarations)
                .ThenInclude(x => x.Grade)
                .Include(x => x.Subjects)
                .ThenInclude(x => x.Homeworks)
                .Include(x => x.Subjects)
                .ThenInclude(x => x.Grades)
                .ToListAsync()).Select(x => new SemesterStats()
            {
                Name = x.Name,
                AverageGrade = x.Subjects!.SelectMany(s => s.Declarations!).Any() ? x.Subjects!.SelectMany(s => s.Declarations!).Average(g => g.Grade?.Value ?? 0.0) : -1.0,
                FailedStudents = x.Subjects!.SelectMany(s => s.Declarations!).Count(d => d.Grade?.Value != null && d.Grade.Value < 0),
                GradedStudents = x.Subjects!.SelectMany(s => s.Declarations!).Count(d => d.Grade?.Value != null),
                Homeworks = x.Subjects!.SelectMany(s => s.Homeworks!).Count(),
                Students = x.Subjects!.SelectMany(s => s.Declarations!).Count(d => d.DeclarationStatus == EDeclarationStatus.Accepted),
                Subjects = x.Subjects!.Count,
            }).ToList();
        }
    }
}