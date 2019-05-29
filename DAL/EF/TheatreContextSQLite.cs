using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace NLayerApp.DAL.EF {
	public class TheatreContextSQLite :TheatreContext {
		public static string dbPath = ConfigurationManager.AppSettings["DbPath"];

		private TheatreContextSQLite(string connectionString)
			: base(connectionString) { }

		private TheatreContextSQLite()
			: this("DefaultConnection") { }

		public static TheatreContextSQLite GetInstance() {
			return Nested.instance;
		}

		protected override void CreateDbIfNotExists() {
			if(! File.Exists(dbPath)) {
				SQLiteConnection.CreateFile(dbPath);
			}
		}

		protected override void CreateTablesIfNotExists() {
			SQLiteFactory factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");
			using(SQLiteConnection connection = (SQLiteConnection)factory.CreateConnection()) {
				connection.ConnectionString = "Data Source = " + dbPath;
				connection.Open();

				using(SQLiteCommand command = new SQLiteCommand(connection)) {
					command.CommandType = CommandType.Text;
					command.CommandText = 
						@"CREATE TABLE IF NOT EXISTS `Plays` (
							`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
							`Name`	text NOT NULL,
							`Author`	text NOT NULL,
							`Genre`	text NOT NULL,
							`DateTime`	datetime NOT NULL
						);";
					command.ExecuteNonQuery();
					
					command.CommandText =
						@"CREATE TABLE IF NOT EXISTS `Tickets` (
							`Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
							`Price`	INTEGER NOT NULL DEFAULT 0,
							`TotalCount`	INTEGER NOT NULL DEFAULT 0,
							`BoughtCount`	INTEGER NOT NULL DEFAULT 0,
							`BookedCount`	INTEGER NOT NULL DEFAULT 0
						);";
					command.ExecuteNonQuery();

					command.CommandText =
						@"CREATE TABLE IF NOT EXISTS `TicketPlays` (
							`Play_Id`	INTEGER,
							`Ticket_Id`	INTEGER,
							FOREIGN KEY(`Ticket_Id`) REFERENCES `Tickets`(`Id`),
							FOREIGN KEY(`Play_Id`) REFERENCES `Plays`(`Id`)
						);";
					command.ExecuteNonQuery();
				}
			}
		}

		// Lazy-Singleton realization
		private class Nested {
			static Nested() { }
			internal static readonly TheatreContextSQLite instance = new TheatreContextSQLite();
		}
	}

}
