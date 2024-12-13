using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
	public class Event
	{
		public int Id { get; set; }
		[Required]

		[Display(Name = "Naam")]
		public string Name { get; set; }
		[Required]

		[Display(Name = "Locatie")]
		public string Location { get; set; }
		[Required]

		[Display(Name = "Datum")]
		public DateTime DateTime { get; set; }
		[Required]

		[Display(Name = "Prijs per kaartje")]
		public decimal Cost { get; set; }
		[Required]

		[Display(Name = "Max. aantal deelnemers")]
		public int MaxParticipants { get; set; }

		[Display(Name = "Beschrijving")]
		public string Description { get; set; }

		[Display(Name = "Foto")]
		public string Photo { get; set; } // Path to photo file

		// Relationships
		[Display(Name = "Organisator")]
		public int OrganizerId { get; set; }
		[Display(Name = "Organisator")]
		public Organizer? Organizer { get; set; }
		[Display(Name = "Categorie")]
		public int CategoryId { get; set; }
		[Display(Name = "Categorie")]
		public Category? Category { get; set; }

		public List<Ticket> Tickets { get; set; } = new();


		public int GetAvailableSeats()
		{
			return MaxParticipants - Tickets.Count;
		}
	}
}