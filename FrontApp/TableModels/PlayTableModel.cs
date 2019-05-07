using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NLayerApp.FrontApp.TableModels {
	class PlayTableModel {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }
		public string Genre { get; set; }
		public string DateTime { get; set; }

		public static DataGridViewColumn[] Columns {
			get {
				DataGridViewTextBoxColumn idCol = new DataGridViewTextBoxColumn();
				idCol.HeaderText = "№";
				idCol.MinimumWidth = 30;
				idCol.ReadOnly = true;
				idCol.CellTemplate = new DataGridViewTextBoxCell();

				DataGridViewTextBoxColumn nameCol = new DataGridViewTextBoxColumn();
				nameCol.HeaderText = "Name";
				nameCol.CellTemplate = new DataGridViewTextBoxCell();

				DataGridViewTextBoxColumn authCol = new DataGridViewTextBoxColumn();
				authCol.HeaderText = "Author";
				authCol.CellTemplate = new DataGridViewTextBoxCell();

				DataGridViewTextBoxColumn genCol = new DataGridViewTextBoxColumn();
				genCol.HeaderText = "Genre";
				genCol.CellTemplate = new DataGridViewTextBoxCell();

				DataGridViewTextBoxColumn dateTimeCol = new DataGridViewTextBoxColumn();
				dateTimeCol.HeaderText = "DateTime";
				dateTimeCol.CellTemplate = new DataGridViewTextBoxCell();


				DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
				col.Width = 0;
				col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
				return new DataGridViewColumn[] { idCol, nameCol, authCol, genCol, dateTimeCol, col };
			}
		}

		public PlayTableModel(int id, string name, string auth, string gen, DateTime dateTime) {
			Id = id;
			Name = name;
			Author = auth;
			Genre = gen;
			DateTime = dateTime.ToString("%D%M%Y");
		}
	}
}
