using NLayerApp.DAL.Repositories;
using NLayerApp.DAL.Interfaces;
using NLayerApp.DAL.Entities;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.DTO;
using System.Collections.Generic;
using System.Linq;

namespace NLayerApp.BLL.Services {
	public class AfishaService :IAfishaService {
		private IUnitOfWork uof = null;

		public AfishaService() {
			uof = new EFUnitOfWork();
		}

		public IEnumerable<PlayDTO> BookTicket(int ticketId) {
			var ticket = uof.Tickets.GetAll().Single(x => x.Id == ticketId);

			if(ticket.TotalCount > ticket.BookedCount + ticket.BoughtCount) {
				ticket.BookedCount++;
				uof.Tickets.Update(ticket);
				uof.Save();
			}

			return PlaysToPlaysDTO(uof.Plays.GetAll());
		}

		public IEnumerable<PlayDTO> BuyTicket(int ticketId) {
			var ticket = uof.Tickets.GetAll().Single(x => x.Id == ticketId);

			if(ticket.TotalCount > ticket.BookedCount + ticket.BoughtCount) {
				ticket.BoughtCount++;
				uof.Tickets.Update(ticket);
				uof.Save();
			}

			return PlaysToPlaysDTO(uof.Plays.GetAll());
		}

		public IEnumerable<PlayDTO> GetAfishaPlays(string filterString) {
			IEnumerable<Play> plays;
			
			if(filterString?.Length > 0) {
				plays = uof.Plays.GetAll().Where(
					pl => pl.Name.Contains(filterString)
					|| pl.Author.Contains(filterString)
					|| pl.Genre.Contains(filterString)
					|| pl.DateTime.ToString().Contains(filterString));
			} else {
				plays = uof.Plays.GetAll();
			}

			return PlaysToPlaysDTO(plays);
		}

		public IEnumerable<PlayDTO> MakeBookedAsBought(int ticketId) {
			var ticket = uof.Tickets.GetAll().Single(x => x.Id == ticketId);

			if(ticket.BoughtCount < ticket.TotalCount) {
				ticket.BoughtCount++;
				ticket.BookedCount--;
				uof.Tickets.Update(ticket);
				uof.Save();
			}

			return PlaysToPlaysDTO(uof.Plays.GetAll());
		}

		public void UpdateAfishaPlays(IEnumerable<PlayDTO> playDTOs) {

			foreach(var newPlay in PlaysDTOToPlays(playDTOs)) {
				if(newPlay.Name == null || newPlay.Genre == null ||
					newPlay.Author == null || newPlay.DateTime == null) {
					throw new System.ArgumentException("Property can't be null");
				}
			}

			foreach(var ticket in uof.Tickets.GetAll()) {
				uof.Tickets.Delete(ticket.Id);
			}
			foreach(var play in uof.Plays.GetAll()) {
				uof.Plays.Delete(play.Id);
			}

			foreach(var newPlay in PlaysDTOToPlays(playDTOs)) {
				uof.Plays.Create(newPlay);
			}

			uof.Save();
		}

		private IEnumerable<PlayDTO> PlaysToPlaysDTO(IEnumerable<Play> plays) {
			var playDTOs = new List<PlayDTO>();
			plays.ToList().ForEach(play => {
				var ticketDTOs = new List<TicketDTO>();
				play.Tickets?.ToList().ForEach(ticket => ticketDTOs.Add(new TicketDTO{
					Id = ticket.Id,
					BookedCount = ticket.BookedCount,
					BoughtCount = ticket.BoughtCount,
					Price = ticket.Price,
					TotalCount = ticket.TotalCount
				}));

				playDTOs.Add(new PlayDTO {
					Id = play.Id,
					Author = play.Author,
					DateTime = play.DateTime,
					Genre = play.Genre,
					Name = play.Name,
					TicketDTOs = ticketDTOs
				});
			});
			return playDTOs;
		}

		private IEnumerable<Play> PlaysDTOToPlays(IEnumerable<PlayDTO> playDTOs) {
			var plays = new List<Play>();
			playDTOs.ToList().ForEach(play => {
				var tickets = new List<Ticket>();
				play.TicketDTOs?.ToList().ForEach(ticket => tickets.Add(new Ticket {
					Id = ticket.Id,
					BookedCount = ticket.BookedCount,
					BoughtCount = ticket.BoughtCount,
					Price = ticket.Price,
					TotalCount = ticket.TotalCount
				}));

				plays.Add(new Play {
					Id = play.Id,
					Author = play.Author,
					DateTime = play.DateTime,
					Genre = play.Genre,
					Name = play.Name,
					Tickets = tickets
				});
			});
			return plays;
		}
	}
}
