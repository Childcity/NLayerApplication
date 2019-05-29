using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NLayerApp.DAL.Repositories.SQLite {
	class PlayRepository :IRepository<Play> {
		private TheatreContextSQLite db;

		public PlayRepository(TheatreContextSQLite dbContext) {
			db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			db.Plays.Load();
		}
		public IEnumerable<Play> GetAll() {
			return db.Plays;
		}

		public Play Get(int id) {
			return db.Plays.Find(id);
		}

		public void Create(Play play) {
			db.Plays.Add(play);
		}

		public void Update(Play play) {
			db.Entry(play).State = EntityState.Modified;
		}

		public IEnumerable<Play> Find(Func<Play, bool> predicate) {
			return db.Plays.Where(predicate);
		}

		public void Delete(int id) {
			Play play = db.Plays.Find(id);
			if(play != null)
				db.Plays.Remove(play);
		}

	}
}
