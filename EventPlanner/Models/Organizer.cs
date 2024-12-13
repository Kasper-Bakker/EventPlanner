using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
	public class Organizer
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "Naam")]
		public string Name { get; set; }
		[Display(Name = "Evenementen")]
		public List<Event> Events { get; set; } = new();
	}
}