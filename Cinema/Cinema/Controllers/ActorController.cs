using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Controllers
{
    public class ActorController : Controller
    {
        private readonly ApplicationDbContext context;

        public ActorController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var actor = context.Actors
            .Include(m => m.MovieActors)
            .ThenInclude(m=>m.Movie)
            .ToList();

            return View(actor);
        }

        //Add Actor
        public IActionResult Add()
        {
            ViewBag.Movies = context.MovieActors.Include
                (ma => ma.Movie).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(Actor actor)
        {
            context.Actors.Add(actor);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Update Actor
        public IActionResult Edit(int id)
        {
            var actor = context.Actors
                .Include(m => m.MovieActors)
                .FirstOrDefault(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            ViewBag.Movies = context.MovieActors.Include
                (ma => ma.Movie).ToList();

            return View(actor);
        }

        [HttpPost]
        public IActionResult Edit(Actor actor)
        {
            if (ModelState.IsValid)
            {
                context.Actors.Update(actor);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actor);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var actor = context.Actors.Find(id);

            if (actor == null)
            {
                return NotFound();
            }

            context.Actors.Remove(actor);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
