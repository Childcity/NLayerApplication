using System;
using System.Collections.Generic;
using NLayerApp.BLL.DTO;

namespace NLayerApp.BLL.Interfaces {
	public interface IAfishaServiceAdapter<OperationType> {
		IEnumerable<PlayDTO> GetAfishaPlays(string filterString);
		void UpdateAfishaPlays(IEnumerable<PlayDTO> plays);
		IEnumerable<PlayDTO> OperateOnTicket(OperationType operation, int ticketId);
	}
}
