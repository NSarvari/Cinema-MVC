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

        //Update Genre
        public IActionResult Edit(int id)
        {
            var genre = context.Genres
                .Include(m => m.Movies)
                .FirstOrDefault(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            ViewBag.Movies = context.Movies.ToList();

            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                context.Genres.Update(genre);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var genre = context.Genres.Find(id);

            if (genre == null)
            {
                return NotFound();
            }

            context.Genres.Remove(genre);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
