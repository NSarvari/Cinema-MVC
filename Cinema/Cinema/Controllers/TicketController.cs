using Cinema.Data;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext context;

        public TicketController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var tickets = context.Tickets
           .Include(t => t.Movie)
           .Include(t => t.Visitor)
           .Include(t => t.Place)
           .ToList();

            return View(tickets);
        }

        public IActionResult Add()
        {
            ViewBag.Movies = context.Movies.ToList();
            ViewBag.Visitors = context.Visitors.ToList();
            ViewBag.Places = context.Places.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(Ticket ticket)
        {
            context.Tickets.Add(ticket);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Update Ticket
        public IActionResult Edit(int id)
        {
            var ticket = context.Tickets
                .Include(m => m.Movie)
                .Include(m => m.Visitor)
                .Include(m => m.Place)
                .FirstOrDefault(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            ViewBag.Movies = context.Movies.ToList();
            ViewBag.Visitors = context.Visitors.ToList();
            ViewBag.Places = context.Places.ToList();

            return View(ticket);
        }

        [HttpPost]
        public IActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                context.Tickets.Update(ticket);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var ticket = context.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound();
            }

            context.Tickets.Remove(ticket);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
