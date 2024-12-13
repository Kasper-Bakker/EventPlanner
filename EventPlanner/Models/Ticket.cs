using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
	public class Ticket
	{
		public int Id { get; set; }
		[Required]
		public string Status { get; set; } = "Niet betaald";
		[Display(Name = "Evenement")]
		public int EventId { get; set; }
		[Display(Name = "Evenement")]
		public Event Event { get; set; }
		[Display(Name = "Deelenemer")]
		public int ParticipantId { get; set; }
		[Display(Name = "Deelenemer")]
		public Participant Participant { get; set; }

	}
}
