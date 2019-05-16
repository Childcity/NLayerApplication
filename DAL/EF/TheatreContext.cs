using NLayerApp.DAL.Entities;
using System.Data.Entity;

namespace NLayerApp.DAL.EF {
	public abstract class TheatreContext :DbContext {

		public DbSet<Play> Plays { get; set; }
		public DbSet<Ticket> Tickets { get; set; }

		public TheatreContext(string connectionString)
			: base(connectionString) {
			Initialize();
		}

		// Template Method
		public void Initialize() {
			CreateDbIfNotExists();
			CreateTablesIfNotExists();
		}

		protected abstract void CreateDbIfNotExists();

		protected abstract void CreateTablesIfNotExists();
	}

}
