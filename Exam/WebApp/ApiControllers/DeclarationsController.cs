using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO;
using Declaration = Domain.Declaration;

namespace WebApp.ApiControllers
{
    /// <inheritdoc />
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class DeclarationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public DeclarationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Declarations
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PublicApi.DTO.Declaration>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Declaration>>> GetDeclarations()
        {
            return await _context.Declarations.ToListAsync();
        }

        // GET: api/Declarations/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.Declaration), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Declaration>> GetDeclaration(Guid id)
        {
            var declaration = await _context.Declarations.FindAsync(id);

            if (declaration == null)
            {
                return NotFound();
            }

            return declaration;
        }

        // PUT: api/Declarations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="declaration"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PublicApi.DTO.Declaration), StatusCodes.Status200OK)]
        public async Task<IActionResult> PutDeclaration(Guid id, PublicApi.DTO.NewDeclaration declaration)
        {
            if (id != declaration.Id)
            {
                return BadRequest();
            }

            Declaration entity = new()
            {
                Id = declaration.Id,
                AppUserId = (Guid) declaration.AppUserId!,
                GradeId = declaration.GradeId,
                DeclarationStatus = declaration.DeclarationStatus,
                SubjectId = declaration.SubjectId,
            };

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeclarationExists(id))
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

        // POST: api/Declarations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// 
        /// </summary>
        /// <param name="declaration"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PublicApi.DTO.Declaration), StatusCodes.Status200OK)]
        public async Task<ActionResult<Declaration>> PostDeclaration(NewDeclaration declaration)
        {
            Declaration entity = new()
            {
                AppUserId = (Guid) User.GetUserId()!,
                DeclarationStatus = declaration.DeclarationStatus,
                GradeId = declaration.GradeId,
                SubjectId = declaration.SubjectId,
            };

            _context.Declarations.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeclaration", new {id = declaration.Id}, declaration);
        }

        // DELETE: api/Declarations/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeclaration(Guid id)
        {
            var declaration = await _context.Declarations.FindAsync(id);
            if (declaration == null)
            {
                return NotFound();
            }

            _context.Declarations.Remove(declaration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeclarationExists(Guid id)
        {
            return _context.Declarations.Any(e => e.Id == id);
        }
    }
}