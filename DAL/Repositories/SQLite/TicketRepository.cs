using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NLayerApp.DAL.Repositories.SQLite {
	class TicketRepository :IRepository<Ticket> {
		private TheatreContextSQLite db;

		public TicketRepository(TheatreContextSQLite dbContext) {
			db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			db.Tickets.Load();
		}
		public IEnumerable<Ticket> GetAll() {
			return db.Tickets;
		}

		public Ticket Get(int id) {
			return db.Tickets.Find(id);
		}

		public void Create(Ticket ticket) {
			db.Tickets.Add(ticket);
		}

		public void Update(Ticket ticket) {
			db.Entry(ticket).State = EntityState.Modified;
		}

		public IEnumerable<Ticket> Find(Func<Ticket, bool> predicate) {
			return db.Tickets.Where(predicate);
		}

		public void Delete(int id) {
			Ticket ticket = db.Tickets.Find(id);
			if(ticket != null)
				db.Tickets.Remove(ticket);
		}

	}
}
