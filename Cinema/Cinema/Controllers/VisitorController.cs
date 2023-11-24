using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Controllers
{
    public class VisitorController : Controller
    {
        private readonly ApplicationDbContext context;

        public VisitorController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var visitors = context.Visitors
           .Include(t => t.Tickets)
           .ToList();

            return View(visitors);
        }

        //Add Visitor
        public IActionResult Add()
        {
            ViewBag.Visitors = context.Visitors.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(Visitor visitor)
        {
            context.Visitors.Add(visitor);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Update Visitor
        public IActionResult Edit(int id)
        {
            var visitor = context.Visitors
                .Include(m => m.Tickets)
                .FirstOrDefault(m => m.Id == id);
            if (visitor == null)
            {
                return NotFound();
            }

            ViewBag.Tickets = context.Tickets.ToList();

            return View(visitor);
        }

        [HttpPost]
        public IActionResult Edit(Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                context.Visitors.Update(visitor);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visitor);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var visitor = context.Visitors.Find(id);

            if (visitor == null)
            {
                return NotFound();
            }

            context.Visitors.Remove(visitor);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
