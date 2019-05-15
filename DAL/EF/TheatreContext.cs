using System;
using System.Data.Entity;
using NLayerApp.DAL.Entities;

namespace NLayerApp.DAL.EF {
	public class TheatreContext :DbContext {
		public static string CreationDateTime = DateTime.Now.ToString();

		public DbSet<Play> Plays { get; set; }
		public DbSet<Ticket> Tickets { get; set; }

		private TheatreContext(string connectionString)
			: base(connectionString) { }

		private TheatreContext()
			: this("DefaultConnection") {
			Console.WriteLine($"Singleton ctor {DateTime.Now.TimeOfDay}");
		}

		public static TheatreContext GetInstance() {
			Console.WriteLine($"GetInstance {DateTime.Now.TimeOfDay}");
			return Nested.instance;
		}

		// Lazy-Singleton realization
		private class Nested {
			static Nested() { }
			internal static readonly TheatreContext instance = new TheatreContext();
		}
	}

}
