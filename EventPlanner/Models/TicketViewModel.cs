using EventPlanner.Models;

public class TicketViewModel
{
	public int EventId { get; set; }
	public Event Event { get; set; }
	public int TicketCount { get; set; } 
	public string ParticipantName { get; set; }
	public string Email { get; set; }
}
