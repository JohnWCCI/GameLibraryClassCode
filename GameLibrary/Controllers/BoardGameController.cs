using DataModel;
using GameLibrary.Data;
using DataModel; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Controllers
{
    public class BoardGameController : Controller
    {
        private readonly GameContext _context;
        public BoardGameController(GameContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            //ToDo add publisher to the BoardGameModel
            return View(_context.BoardGames
                .OrderBy(b => b.PublishersId) // order by Id so that all books by the same publisher will be together
                .Include(p => p.Publishers)
                .ToList());
        }
        public ActionResult Create()
        {
            BoardGameModel boardGame = new BoardGameModel();
            return View(boardGame);
        }
        [HttpPost]
        public ActionResult Create(BoardGameModel boardGame)
        {
            if (ModelState.IsValid)
            {
                //ToDo uses NewPublisher for the user to enter a publisher new
                boardGame.PublishersId = GetPublisher(boardGame.NewPublisher);
                _context.BoardGames.Add(boardGame);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boardGame);
        }
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return Problem($"BoardGame Id is null");
            }
            var game = _context.BoardGames.FirstOrDefault(
                g => g.Id == id);
            if (game == null)
            {
                return Problem($"BoardGame Id {id} was not found");
            }
            return View(game);
        }
        //the http get delete method doesn't delete the specified game, it returns a 
        //view of the game where you can submit the deletion
        [HttpPost]
        public ActionResult Delete(int? id, bool notUsed)
        {
            if (_context.BoardGames == null)
            {
                return Problem("Entity set 'GameContext.BoardGames is null");
            }
            var game = _context.BoardGames.Find(id);
            if (game is not null) //ToDo: Added A Null Check to prevent errors
            {
                _context.BoardGames.Remove(game);
                _context.SaveChanges();
            }
            else
            {
                return Problem($"Unable to delete BoardGames id {id}");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            BoardGameModel? game = _context.BoardGames
                                    .Include(p=>p.Publishers)
                                    .Where(b => b.Id == id)
                                    .FirstOrDefault();
            if (game is not null)
            {
                game.NewPublisher = game.Publisher;
                return View(game);
            }

            return View();
        }
        [HttpPost]
        public ActionResult Edit(BoardGameModel game)
        {
            if (ModelState.IsValid)
            {
                game.PublishersId = GetPublisher(game.NewPublisher);
                _context.Entry(game).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return Problem("Entity set 'GameContext.BoardGames is null");
            }
            var game = _context.BoardGames
                .Where(b => b.Id == id)
                .Include(b => b.Publishers)
                .FirstOrDefault();
            if (game is null)
            {
                return Problem($"Unable to find BoardGames id {id}");
            }
            return View(game);
        }

        //ToDo Gets the Publisher ID even if it has to add the publisher
        private int GetPublisher(string publisher)
        {
            PublisherModel? pub = null;

            pub = _context.Publishers
                   .Where(p => p.Name.ToLower() == publisher)
                   .FirstOrDefault();
            if (pub is null)
            {
                pub = new PublisherModel
                {
                    Name = publisher
                };
                _context.Add(pub);
                _context.SaveChanges();
            }
            return pub.Id;
        }

    }
}
