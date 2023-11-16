using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;

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
            var movies = 
                context.Movies.ToList();

            return View(movies);
        }

        public IActionResult Add()
        {
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
