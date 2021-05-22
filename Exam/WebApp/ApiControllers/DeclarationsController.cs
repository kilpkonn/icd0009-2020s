using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <inheritdoc />
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
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
        public async Task<IActionResult> PutDeclaration(Guid id, Declaration declaration)
        {
            if (id != declaration.Id)
            {
                return BadRequest();
            }

            _context.Entry(declaration).State = EntityState.Modified;

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
        public async Task<ActionResult<Declaration>> PostDeclaration(Declaration declaration)
        {
            _context.Declarations.Add(declaration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeclaration", new { id = declaration.Id }, declaration);
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
