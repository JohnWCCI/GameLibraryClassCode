using GameLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Controllers
{
    public class PublisherController : Controller
    {
        private readonly GameContext _context;
        public PublisherController(GameContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var list = _context.Publishers
                .Include(b => b.BoardGames)
                .ToList();
            return View(list);
        }

        public ActionResult Details(int Id)
        {
            var list = _context.Publishers
               .Where(b => b.Id == Id)
               .Include(b => b.BoardGames)
               .FirstOrDefault();
            return View(list);
        }
    }
}
