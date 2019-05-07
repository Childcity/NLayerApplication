using NLayerApp.DAL.Entities;
using System;

namespace NLayerApp.DAL.Interfaces {
	public interface IUnitOfWork :IDisposable {
		IRepository<Play> Plays { get; }
		IRepository<Ticket> Tickets { get; }
		void Save();
	}
}