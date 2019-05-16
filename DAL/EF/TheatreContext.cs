using System.Data.Entity;

namespace NLayerApp.DAL.EF {
	public abstract class TheatreContext :DbContext {

		public TheatreContext(string connectionString)
			: base(connectionString) {
			Initialize();
		}

		// Template Method
		public void Initialize() {
			CreateDbIfNotExists();
			CreateTablesIfNotExists();
		}

		protected abstract void CreateDbIfNotExists();

		protected abstract void CreateTablesIfNotExists();
	}

}
