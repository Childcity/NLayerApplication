using System.Collections.Generic;

namespace NLayerApp.DAL.Entities { 
	public class Ticket {
		public int Id { get; set; }
		public int Price { get; set; }
		public int TotalCount { get; set; }
		public int BoughtCount { get; set; }
		public int BookedCount { get; set; }

		public virtual ICollection<Play> Plays { get; set; }

		public Ticket() {
			Plays = new HashSet<Play>();
		}
	}
}
