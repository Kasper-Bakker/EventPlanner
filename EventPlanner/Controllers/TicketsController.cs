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

		public async Task<IActionResult> Index()
		{
			var tickets = await _context.Tickets
				.Include(t => t.Event)
				.Include(t => t.Participant)
				.ToListAsync();

			return View(tickets);
		}

		// GET: Tickets/Reserve/5
		public async Task<IActionResult> Reserve(int? eventId)
		{
			if (eventId == null)
			{
				return NotFound();
			}

			var ev = await _context.Events
				.Include(e => e.Tickets)
				.FirstOrDefaultAsync(e => e.Id == eventId);

			if (ev == null)
			{
				return NotFound();
			}

			if (ev.GetAvailableSeats() <= 0)
			{
				return BadRequest("No seats available.");
			}

			var model = new Ticket
			{
				EventId = ev.Id,
				Event = ev,
			};

			return View(model);
		}






		// POST: Tickets/Reserve
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Reserve(int eventId, string ParticipantName, string Email, int TicketCount)
		{
			var ev = await _context.Events
				.Include(e => e.Tickets)
				.FirstOrDefaultAsync(e => e.Id == eventId);

			if (ev == null || ev.GetAvailableSeats() <= 0)
			{
				return BadRequest("Geen plaatsen beschikbaar.");
			}

			var participant = await _context.Participants
				.FirstOrDefaultAsync(p => p.Name == ParticipantName);

			if (participant == null)
			{
				participant = new Participant
				{
					Name = ParticipantName,
					Email = Email
				};
				_context.Participants.Add(participant);
				await _context.SaveChangesAsync();
			}

			for (int i = 0; i < TicketCount; i++)
			{
				var ticket = new Ticket
				{
					EventId = ev.Id,
					Status = "Niet betaald", 
					ParticipantId = participant.Id
				};

				_context.Tickets.Add(ticket);
			}

			await _context.SaveChangesAsync();

			return RedirectToAction("Index", "Events");
		}


		// GET: Tickets/MarkPaid/5
		[HttpPost]
		public async Task<IActionResult> MarkPaid(int id)
		{
			var ticket = await _context.Tickets
				.Include(t => t.Event)
				.Include(t => t.Participant)
				.FirstOrDefaultAsync(t => t.Id == id);

			if (ticket == null)
				return NotFound();

			ticket.Status = "Betaald";
			await _context.SaveChangesAsync();

			return RedirectToAction("Ticketadmin");
		}


		public async Task<IActionResult> Ticketadmin()
		{
			var tickets = await _context.Tickets
				.Include(t => t.Event)
				.Include(t => t.Participant)
				.ToListAsync();

			return View(tickets);
		}




	}
}
