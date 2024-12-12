using Microsoft.AspNetCore.Mvc;
using EventPlanner.Data;
using EventPlanner.Models;

namespace EventPlanner.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly Database _context;

		public CategoriesController(Database context)
		{
			_context = context;
		}

		// GET: Categories/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Categories/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Category category)
		{
			if (ModelState.IsValid)
			{
				_context.Categories.Add(category);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Events"); // Terug naar de events index-pagina
			}

			return View(category);
		}
	}
}
