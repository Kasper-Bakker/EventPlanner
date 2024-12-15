using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Data; 
using EventPlanner.Models; 

namespace EventPlannerAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EventsController : ControllerBase
	{
		private readonly Database _context;

		public EventsController(Database context)
		{
			_context = context;
		}

		// GET: api/events
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
		{
			var events = await _context.Events.ToListAsync();
			return events;
		}

		// GET: api/events/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Event>> GetEvent(int id)
		{
			var eventItem = await _context.Events.FindAsync(id);

			if (eventItem == null)
			{
				return NotFound();
			}

			return eventItem;
		}
	}
}
