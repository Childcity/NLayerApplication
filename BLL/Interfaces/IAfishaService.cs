using System;
using System.Collections.Generic;
using NLayerApp.BLL.DTO;

namespace NLayerApp.BLL.Interfaces {
	public interface IAfishaService {
		IEnumerable<PlayDTO> GetAfishaPlays(string filterString);
		void UpdateAfishaPlays(IEnumerable<PlayDTO> plays);
		IEnumerable<PlayDTO> BuyTicket(int ticketId);
		IEnumerable<PlayDTO> BookTicket(int ticketId);
		IEnumerable<PlayDTO> MakeBookedAsBought(int ticketId);
	}
}
