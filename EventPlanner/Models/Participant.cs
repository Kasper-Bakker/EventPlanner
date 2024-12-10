using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
	public class Participant
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required, EmailAddress]
		public string Email { get; set; }

		public List<Ticket> Tickets { get; set; } = new();
	}
}
