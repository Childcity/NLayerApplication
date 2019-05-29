using NLayerApp.DAL.Repositories;
using NLayerApp.DAL.Interfaces;
using NLayerApp.DAL.Entities;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.DTO;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NLayerApp.BLL.Services {

	public enum TicketOperation {
		Buy,
		Book,
		MakeBookAsBought
	}

	public class AfishaServiceAdapter :IAfishaServiceAdapter<TicketOperation> {
		private IAfishaService afishaSvc = new AfishaService();

		public IEnumerable<PlayDTO> GetAfishaPlays(string filterString) => afishaSvc.GetAfishaPlays(filterString);

		public void UpdateAfishaPlays(IEnumerable<PlayDTO> plays) => afishaSvc.UpdateAfishaPlays(plays);

		public IEnumerable<PlayDTO> OperateOnTicket(TicketOperation operation, int ticketId) {
			switch(operation) {
				case TicketOperation.Buy:
					return afishaSvc.BuyTicket(ticketId);
				case TicketOperation.Book:
					return afishaSvc.BookTicket(ticketId);
				case TicketOperation.MakeBookAsBought:
					return afishaSvc.MakeBookedAsBought(ticketId);
				default:
					throw new ArgumentException("Invalid operation type: " + operation);
			}
		}
	}
}