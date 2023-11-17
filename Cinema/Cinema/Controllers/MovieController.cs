using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext context;

        public MovieController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var movies = context.Movies
            .Include(m => m.Genre)
            .Include(m => m.MovieActors)
            .Include(m => m.Tickets)
            .ToList();

            return View(movies);
        }

        public IActionResult Add()
        {
            ViewBag.Genres = context.Genres.ToList();
            ViewBag.Actors = context.MovieActors.Include
                (ma => ma.Actor).ToList();
            ViewBag.Tickets = context.Tickets.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
