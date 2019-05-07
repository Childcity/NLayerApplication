using System;
using System.Collections.Generic;

namespace NLayerApp.DAL.Entities {
    public class Play
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }
		public string Genre { get; set; }
		public DateTime DateTime { get; set; }

		public virtual ICollection<Ticket> Tickets { get; set; }

		public Play() {
			Tickets = new HashSet<Ticket>();
		}

	}
}
