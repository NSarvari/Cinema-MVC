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

        //Add Movie
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

        //Update Movie
        public IActionResult Edit(int id)
        {
            var movie = context.Movies
                .Include (m => m.Genre)
                .Include(m => m.MovieActors)
                .Include(m => m.Tickets)
                .FirstOrDefault(m=>m.Id==id);
            if (movie == null)
            {
                return NotFound();
            }

            ViewBag.Genres = context.Genres.ToList();
            ViewBag.Actors = context.MovieActors.Include
                (ma => ma.Actor).ToList();
            ViewBag.Tickets = context.Tickets.ToList();
            
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
                context.Movies.Update(movie);
                context.SaveChanges();
                return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var movie = context.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            context.Movies.Remove(movie);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
