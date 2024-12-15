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
		public async Task<IActionResult> Reserve(Ticket ticket, string ParticipantName, string Email)
		{
			var ev = await _context.Events
				.Include(e => e.Tickets)
				.FirstOrDefaultAsync(e => e.Id == ticket.EventId);

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

			ticket.ParticipantId = participant.Id;
			ticket.Status = "Niet betaald";

			ticket.ConfirmationNumber = GenerateConfirmationNumber();

			ev.Tickets.Add(ticket);
			await _context.SaveChangesAsync();

			return RedirectToAction("Confirmation", new { ticketId = ticket.Id });
		}

		private string GenerateConfirmationNumber()
		{
			Random rand = new Random();
			int randomNumber = rand.Next(1000, 9999);  
			return $"#{randomNumber}";
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

		// GET: Tickets/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var ticket = await _context.Tickets
				.Include(t => t.Event)
				.Include(t => t.Participant)
				.FirstOrDefaultAsync(t => t.Id == id);

			if (ticket == null)
			{
				return NotFound();
			}

			return View(ticket);
		}

		// POST: Tickets/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var ticket = await _context.Tickets.FindAsync(id);
			if (ticket != null)
			{
				_context.Tickets.Remove(ticket);  
				await _context.SaveChangesAsync(); 
			}

			return RedirectToAction(nameof(Ticketadmin));  
		}

		public IActionResult Confirmation(int ticketId)
		{
			var ticket = _context.Tickets
				.Include(t => t.Event)
				.Include(t => t.Participant)
				.FirstOrDefault(t => t.Id == ticketId);

			if (ticket == null)
			{
				return NotFound();
			}

			return View(ticket);
		}


	}
}
