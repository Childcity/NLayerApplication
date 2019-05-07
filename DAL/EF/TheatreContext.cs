using System;
using System.Data.Entity;
using NLayerApp.DAL.Entities;

namespace NLayerApp.DAL.EF {
	public class TheatreContext :DbContext {
		public DbSet<Play> Plays { get; set; }
		public DbSet<Ticket> Tickets { get; set; }

		public TheatreContext(string connectionString)
			: base(connectionString) {
		}

		public TheatreContext()
			: this("DefaultConnection") { }

		//static TheatreContext() {
		//	Database.SetInitializer(new StoreDbInitializer());
		//}
	}

	//class StoreDbInitializer :CreateDatabaseIfNotExists<TheatreContext> {
	//	protected override void Seed(TheatreContext db) {
	//		db.Plays.Add(new Play { Name = "Natalka Poltavka", Author = "Mykola Lysenko", Genre = "Opera", DateTime = DateTime.Now });
	//		db.SaveChanges();
	//	}
	//}
}
