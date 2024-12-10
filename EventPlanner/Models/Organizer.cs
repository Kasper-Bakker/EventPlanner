using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
	public class Organizer
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		public List<Event> Events { get; set; } = new();
	}
}