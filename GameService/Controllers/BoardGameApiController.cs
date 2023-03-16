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
    public class BoardGameApiController : ControllerBase
    {
        private readonly GameContext _context;

        public BoardGameApiController(GameContext context)
        {
            _context = context;
        }

        // GET: api/BoardGameApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardGameModel>>> GetBoardGames()
        {
          if (_context.BoardGames == null)
          {
              return NotFound();
          }
            return await _context.BoardGames.ToListAsync();
        }

        // GET: api/BoardGameApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardGameModel>> GetBoardGameModel(int id)
        {
          if (_context.BoardGames == null)
          {
              return NotFound();
          }
            var boardGameModel = await _context.BoardGames.FindAsync(id);

            if (boardGameModel == null)
            {
                return NotFound();
            }

            return boardGameModel;
        }

        // PUT: api/BoardGameApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoardGameModel(int id, BoardGameModel boardGameModel)
        {
            if (id != boardGameModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(boardGameModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardGameModelExists(id))
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

        // POST: api/BoardGameApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoardGameModel>> PostBoardGameModel(BoardGameModel boardGameModel)
        {
          if (_context.BoardGames == null)
          {
              return Problem("Entity set 'GameContext.BoardGames'  is null.");
          }
            _context.BoardGames.Add(boardGameModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoardGameModel", new { id = boardGameModel.Id }, boardGameModel);
        }

        // DELETE: api/BoardGameApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoardGameModel(int id)
        {
            if (_context.BoardGames == null)
            {
                return NotFound();
            }
            var boardGameModel = await _context.BoardGames.FindAsync(id);
            if (boardGameModel == null)
            {
                return NotFound();
            }

            _context.BoardGames.Remove(boardGameModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardGameModelExists(int id)
        {
            return (_context.BoardGames?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
