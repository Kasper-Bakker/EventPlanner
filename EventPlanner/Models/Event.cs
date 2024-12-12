using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
	public class Event
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Location { get; set; }
		[Required]
		public DateTime DateTime { get; set; }
		[Required]
		public decimal Cost { get; set; }
		[Required]
		public int MaxParticipants { get; set; }

		public string Description { get; set; }
		public string Photo { get; set; } // Path to photo file

		// Relationships
		public int OrganizerId { get; set; }
		public Organizer? Organizer { get; set; }

		public int CategoryId { get; set; }
		public Category? Category { get; set; }

		public List<Ticket> Tickets { get; set; } = new();


		public int GetAvailableSeats()
		{
			return MaxParticipants - Tickets.Count;
		}
	}
}