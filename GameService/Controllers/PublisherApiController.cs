using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataModel;
using GameService.Data;

namespace GameService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherApiController : ControllerBase
    {
        private readonly GameContext _context;

        public PublisherApiController(GameContext context)
        {
            _context = context;
        }

        // GET: api/PublisherApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherModel>>> GetPublishers()
        {
          if (_context.Publishers == null)
          {
              return NotFound();
          }
            return await _context.Publishers.ToListAsync();
        }

        // GET: api/PublisherApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherModel>> GetPublisherModel(int id)
        {
          if (_context.Publishers == null)
          {
              return NotFound();
          }
            var publisherModel = await _context.Publishers.FindAsync(id);

            if (publisherModel == null)
            {
                return NotFound();
            }

            return publisherModel;
        }

        // PUT: api/PublisherApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisherModel(int id, PublisherModel publisherModel)
        {
            if (id != publisherModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(publisherModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherModelExists(id))
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

        // POST: api/PublisherApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PublisherModel>> PostPublisherModel(PublisherModel publisherModel)
        {
          if (_context.Publishers == null)
          {
              return Problem("Entity set 'GameContext.Publishers'  is null.");
          }
            _context.Publishers.Add(publisherModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublisherModel", new { id = publisherModel.Id }, publisherModel);
        }

        // DELETE: api/PublisherApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisherModel(int id)
        {
            if (_context.Publishers == null)
            {
                return NotFound();
            }
            var publisherModel = await _context.Publishers.FindAsync(id);
            if (publisherModel == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisherModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublisherModelExists(int id)
        {
            return (_context.Publishers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
