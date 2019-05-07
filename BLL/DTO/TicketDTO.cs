namespace NLayerApp.BLL.DTO { 
	public class TicketDTO {
		public int Id { get; set; }
		public int Price { get; set; }
		public int TotalCount { get; set; }
		public int BoughtCount { get; set; }
		public int BookedCount { get; set; }
		public int Left {
			get => TotalCount - BookedCount - BoughtCount;
			set { BoughtCount = TotalCount - (value + BookedCount); }
		}
	}
}
