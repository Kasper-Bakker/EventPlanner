using EventPlanner.Data;
using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Controllers
{
	public class TicketsController : Controller
	{
		private readonly Database _context;

		public TicketsController(Database context)
		{
			_context = context;
		}

		// GET: Tickets/Reserve/5
		public async Task<IActionResult> Reserve(int? eventId)
		{
			if (eventId == null) return NotFound();

			var ev = await _context.Events
				.Include(e => e.Tickets)
				.FirstOrDefaultAsync(e => e.Id == eventId);

			if (ev == null) return NotFound();

			if (ev.GetAvailableSeats() <= 0)
			{
				return BadRequest("No seats available.");
			}

			var ticket = new Ticket
			{
				EventId = ev.Id,
				Status = "Unpaid"
			};

			return View(ticket);
		}

		// POST: Tickets/Reserve
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Reserve(Ticket ticket)
		{
			if (ModelState.IsValid)
			{
				var ev = await _context.Events
					.Include(e => e.Tickets)
					.FirstOrDefaultAsync(e => e.Id == ticket.EventId);

				if (ev.GetAvailableSeats() > 0)
				{
					_context.Tickets.Add(ticket);
					await _context.SaveChangesAsync();
					return RedirectToAction("Index", "Events");
				}
				return BadRequest("No seats available.");
			}
			return View(ticket);
		}

		// GET: Tickets/MarkPaid/5
		public async Task<IActionResult> MarkPaid(int? id)
		{
			if (id == null) return NotFound();

			var ticket = await _context.Tickets
				.Include(t => t.Event)
				.Include(t => t.Participant)
				.FirstOrDefaultAsync(t => t.Id == id);

			if (ticket == null) return NotFound();

			ticket.Status = "Paid";
			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Events");
		}
	}
}
