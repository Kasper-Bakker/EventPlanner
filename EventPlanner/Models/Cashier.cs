using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
	public class Cashier
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		public List<Ticket> Tickets { get; set; } = new();
	}
}
