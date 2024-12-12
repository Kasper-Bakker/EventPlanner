using Microsoft.AspNetCore.Mvc;
using EventPlanner.Data;
using EventPlanner.Models;

namespace EventPlanner.Controllers
{
	public class OrganizersController : Controller
	{
		private readonly Database _context;

		public OrganizersController(Database context)
		{
			_context = context;
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
				return RedirectToAction("Index", "Events");
			}

			return View(organizer);
		}
	}
}
