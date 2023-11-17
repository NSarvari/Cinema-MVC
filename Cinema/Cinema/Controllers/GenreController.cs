using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext context;

        public GenreController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var genres = context.Genres
            .Include(m => m.Movies).ToList();

            return View(genres);
        }

        public IActionResult Add()
        {
            ViewBag.Movies = context.Movies.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre genre)
        {
            context.Genres.Add(genre);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
