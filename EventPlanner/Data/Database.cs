using System.Collections.Generic;
using System.Net.Sockets;
using EventPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Data
{
	public class Database : DbContext
	{
		public Database(DbContextOptions<Database> options) : base(options) { }

		public DbSet<Organizer> Organizers { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Participant> Participants { get; set; }
		public DbSet<Ticket> Tickets { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string connection = @"Data Source=.;Initial Catalog=Eventplanner;Integrated Security=true;TrustServerCertificate=True;";
			optionsBuilder.UseSqlServer(connection);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Event>().ToTable("Events");
			modelBuilder.Entity<Category>().ToTable("Categories");
			modelBuilder.Entity<Organizer>().ToTable("Organizers");
			modelBuilder.Entity<Participant>().ToTable("Participants");
			modelBuilder.Entity<Ticket>().ToTable("Tickets");
		}
	}

	
}
