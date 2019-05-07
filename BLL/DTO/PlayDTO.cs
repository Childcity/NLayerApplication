using System;
using System.Collections.Generic;

namespace NLayerApp.BLL.DTO {
    public class PlayDTO
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }
		public string Genre { get; set; }
		public DateTime DateTime { get; set; }
		public ICollection<TicketDTO> TicketDTOs { get; set; }
	}
}
