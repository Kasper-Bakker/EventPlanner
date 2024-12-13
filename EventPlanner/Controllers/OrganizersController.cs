using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Data;
using EventPlanner.Models;
using System.Threading.Tasks;
using System.Linq;

namespace EventPlanner.Controllers
{
	public class OrganizersController : Controller
	{
		private readonly Database _context;

		public OrganizersController(Database context)
		{
			_context = context;
		}

		// GET: Organizers
		public async Task<IActionResult> Index()
		{
			var organizers = await _context.Organizers.ToListAsync();
			return View(organizers);
		}

		// GET: Organizers/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return NotFound();

			var organizer = await _context.Organizers
				.FirstOrDefaultAsync(o => o.Id == id);

			if (organizer == null) return NotFound();

			return View(organizer);
		}

		// GET: Organizers/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Organizers/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Organizer organizer)
		{
			if (ModelState.IsValid)
			{
				_context.Organizers.Add(organizer);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(organizer);
		}

		// GET: Organizers/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();

			var organizer = await _context.Organizers.FindAsync(id);
			if (organizer == null) return NotFound();

			return View(organizer);
		}

		// POST: Organizers/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Organizer organizer)
		{
			if (id != organizer.Id) return NotFound();

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(organizer);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!OrganizerExists(organizer.Id)) return NotFound();
					else throw;
				}

				return RedirectToAction(nameof(Index));
			}

			return View(organizer);
		}

		// GET: Organizers/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();

			var organizer = await _context.Organizers
				.FirstOrDefaultAsync(o => o.Id == id);

			if (organizer == null) return NotFound();

			return View(organizer);
		}

		// POST: Organizers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var organizer = await _context.Organizers.FindAsync(id);
			if (organizer != null)
			{
				_context.Organizers.Remove(organizer);
				await _context.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Index));
		}

		private bool OrganizerExists(int id)
		{
			return _context.Organizers.Any(o => o.Id == id);
		}
	}
}
