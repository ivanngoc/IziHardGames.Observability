using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi0;

namespace WebApi0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityGen1Controller : ControllerBase
    {
        private readonly SimpleDbContext _context;

        public EntityGen1Controller(SimpleDbContext context)
        {
            _context = context;
        }
        [HttpPost("InitDb")]
        public async Task<IActionResult> GetEntityGen1()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
            return Ok();
        }

        // GET: api/EntityGen1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntityGen1>>> GetEntityGen1s()
        {
            return await _context.EntityGen1s.ToListAsync();
        }

        // GET: api/EntityGen1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntityGen1>> GetEntityGen1(Guid id)
        {
            var entityGen1 = await _context.EntityGen1s.FindAsync(id);

            if (entityGen1 == null)
            {
                return NotFound();
            }

            return entityGen1;
        }

        // PUT: api/EntityGen1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntityGen1(Guid id, EntityGen1 entityGen1)
        {
            if (id != entityGen1.guid)
            {
                return BadRequest();
            }

            _context.Entry(entityGen1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityGen1Exists(id))
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

        // POST: api/EntityGen1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EntityGen1>> PostEntityGen1(EntityGen1 entityGen1)
        {
            entityGen1.guid = Guid.NewGuid();
            _context.EntityGen1s.Add(entityGen1);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntityGen1", new { id = entityGen1.guid }, entityGen1);
        }

        // DELETE: api/EntityGen1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntityGen1(Guid id)
        {
            var entityGen1 = await _context.EntityGen1s.FindAsync(id);
            if (entityGen1 == null)
            {
                return NotFound();
            }

            _context.EntityGen1s.Remove(entityGen1);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntityGen1Exists(Guid id)
        {
            return _context.EntityGen1s.Any(e => e.guid == id);
        }
    }
}
