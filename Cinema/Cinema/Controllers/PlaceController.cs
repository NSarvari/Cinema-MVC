using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Controllers
{
    public class PlaceController : Controller
    {
        private readonly ApplicationDbContext context;

        public PlaceController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var place = context.Places
           .Include(t => t.Tickets)
           .ToList();

            return View(place);
        }

        //Add Place
        public IActionResult Add()
        {
            ViewBag.Tickets = context.Tickets.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(Place place)
        {
            context.Places.Add(place);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Update Place
        public IActionResult Edit(int id)
        {
            var place = context.Places
                .Include(m => m.Tickets)
                .FirstOrDefault(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            ViewBag.Tickets = context.Tickets.ToList();

            return View(place);
        }

        [HttpPost]
        public IActionResult Edit(Place place)
        {
            if (ModelState.IsValid)
            {
                context.Places.Update(place);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(place);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var place = context.Places.Find(id);

            if (place == null)
            {
                return NotFound();
            }

            context.Places.Remove(place);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
