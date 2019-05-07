using NLayerApp.DAL.EF;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using System;
using NLayerApp.DAL.Repositories.SQLite;

namespace NLayerApp.DAL.Repositories {
	public class EFUnitOfWork :IUnitOfWork {
		private TheatreContext db;
		private PlayRepository playRepository;
		private TicketRepository ticketRepository;

		public IRepository<Play> Plays {
			get => playRepository ?? (playRepository = new PlayRepository(db));
		}

		public IRepository<Ticket> Tickets {
			get => ticketRepository ?? (ticketRepository = new TicketRepository(db));
		}

		public EFUnitOfWork() => db = new TheatreContext();

		public EFUnitOfWork(string connectionString) => db = new TheatreContext(connectionString);

		public void Save() {
			db.SaveChanges();
		}

		#region IDisposable Support
		private bool disposedValue = false; // Для определения избыточных вызовов

		protected virtual void Dispose(bool disposing) {
			if(!disposedValue) {
				if(disposing) {
					db.Dispose();
				}
				disposedValue = true;
			}
		}

		// Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
