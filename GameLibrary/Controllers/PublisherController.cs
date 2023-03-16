using DataModel;
using GameLibrary.Data;
using GameLibrary.Models;
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
            var list = View(_context.Publishers
                                .Include(b => b.BoardGames)
                                .ToList());
            return View();
        }
        public ActionResult Create()
        {
            PublisherModel publisher = new PublisherModel();

            return View(publisher);
        }
        [HttpPost]
        public ActionResult Create(PublisherModel publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return Problem($"Publisher Id is null");
            }
            var publisher = _context.Publishers
                .Where(p => p.Id == id)
                .Include(p => p.BoardGames)
                .FirstOrDefault();
            if (publisher == null)
            {
                return Problem($"Publisher Id {id} was not found");
            }
            return View(publisher);
        }
        //the http get delete method doesn't delete the specified publisher, it returns a 
        //view of the game where you can submit the deletion
        [HttpPost]
        public ActionResult Delete(int? id, bool notUsed)
        {
            if (_context.Publishers == null)
            {
                return Problem("Entity set 'GameContext.Publisher is null");
            }
            var publisher = _context.Publishers.Find(id);
            if (publisher is not null) //ToDo: Added A Null Check to prevent errors
            {
                _context.Publishers.Remove(publisher);
                _context.SaveChanges();
            }
            else
            {
                return Problem($"Unable to delete Publisher id {id}");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            PublisherModel? publisher = _context.Publishers
                                    .Where(b => b.Id == id)
                                    .FirstOrDefault();
            if (publisher is not null)
            {

                return View(publisher);
            }

            return View();
        }
        [HttpPost]
        public ActionResult Edit(PublisherModel publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(publisher).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publisher);
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