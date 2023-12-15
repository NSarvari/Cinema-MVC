using Cinema.Data;
using Cinema.Models;
using Cinema.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IFileService fileService;

        public MovieController(ApplicationDbContext context,
            IFileService fileService)
        {
            this.context = context;
            this.fileService = fileService;
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
        [Authorize(Roles = "Admin")]
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
            if (movie.FileUpload != null)
            {
                var fileResult = fileService.SaveImage
                    (movie.FileUpload);
                if (fileResult.Item1==1)
                {
                    movie.Poster = fileResult.Item2;
                }
                else
                {
                    ModelState.AddModelError(
                        string.Empty,fileResult.Item2 );
                    return View(movie);
                }
            }
            context.Movies.Add(movie);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Update Movie
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
