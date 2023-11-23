using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Controllers
{
    public class MovieActorController : Controller
    {
        private readonly ApplicationDbContext context;

        public MovieActorController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var movieActor = context.MovieActors
            .Include(m => m.Movie)
            .Include(m => m.Actor)
            .ToList();

            return View(movieActor);
        }

        //Add MovieActor
        public IActionResult Add()
        {
            ViewBag.Movies = context.Movies.ToList();
            ViewBag.Actors = context.Actors.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(MovieActor movieActor)
        {
            context.MovieActors.Add(movieActor);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
