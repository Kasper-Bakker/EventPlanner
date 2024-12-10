using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Data;
using EventPlanner.Models;

namespace EventPlanner.Controllers
{
    public class EventsController : Controller
    {
        private readonly Database _context;

        public EventsController(Database context)
        {
            _context = context;
        }

		// GET: Events
		public async Task<IActionResult> Index()
		{
			var events = await _context.Events
				.Include(e => e.Organizer)
				.Include(e => e.Category)
				.ToListAsync();
			return View(events);
		}

		// GET: Events/Create
		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
			ViewBag.Organizers = new SelectList(_context.Organizers, "Id", "Name");
			return View();
		}

		// POST: Events/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Event ev)
		{
			if (ModelState.IsValid)
			{
				_context.Add(ev);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
			ViewBag.Organizers = new SelectList(_context.Organizers, "Id", "Name");
			return View(ev);
		}

		// GET: Events/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return NotFound();

			var ev = await _context.Events
				.Include(e => e.Organizer)
				.Include(e => e.Category)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (ev == null) return NotFound();

			return View(ev);
		}
	}
}
