using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
	public class Ticket
	{
		public int Id { get; set; }
		[Required]
		public string Status { get; set; } = "Unpaid"; 

		public int EventId { get; set; }
		public Event Event { get; set; }

		public int ParticipantId { get; set; }
		public Participant Participant { get; set; }

		public int CashierId { get; set; }
		public Cashier Cashier { get; set; }
	}
}
