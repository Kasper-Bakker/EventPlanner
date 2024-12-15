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

		// GET: Events/Edit/5
		// GET: Events/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();

			var ev = await _context.Events.FindAsync(id);
			if (ev == null) return NotFound();

			ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", ev.CategoryId);
			ViewBag.OrganizerId = new SelectList(_context.Organizers, "Id", "Name", ev.OrganizerId);
			return View(ev);
		}


		// POST: Events/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Event ev)
		{
			if (id != ev.Id) return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(ev);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!EventExists(ev.Id)) return NotFound();
					else throw;
				}
				return RedirectToAction(nameof(Index));
			}

			ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", ev.CategoryId);
			ViewBag.OrganizerId = new SelectList(_context.Organizers, "Id", "Name", ev.OrganizerId);
			return View(ev);
		}


		// GET: Events/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();

			var ev = await _context.Events
				.Include(e => e.Organizer)
				.Include(e => e.Category)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (ev == null) return NotFound();

			return View(ev);
		}

		// POST: Events/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var ev = await _context.Events.FindAsync(id);
			if (ev != null)
			{
				_context.Events.Remove(ev);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> EventAdmin()
		{
			var events = await _context.Events
				.Include(e => e.Category)
				.Include(e => e.Organizer)
				.ToListAsync();

			return View(events);
		}


		private bool EventExists(int id)
		{
			return _context.Events.Any(e => e.Id == id);
		}
	}
}
