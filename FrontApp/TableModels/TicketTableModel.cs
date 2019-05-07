using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NLayerApp.FrontApp.TableModels {
	class TicketTableModel {
		public static DataGridViewColumn[] Columns {
			get {
				DataGridViewTextBoxColumn idCol = new DataGridViewTextBoxColumn();
				idCol.HeaderText = "№";
				idCol.MinimumWidth = 30;
				idCol.ReadOnly = true;
				idCol.CellTemplate = new DataGridViewTextBoxCell();

				DataGridViewTextBoxColumn nameCol = new DataGridViewTextBoxColumn();
				nameCol.HeaderText = "Price";
				nameCol.CellTemplate = new DataGridViewTextBoxCell();

				DataGridViewTextBoxColumn authCol = new DataGridViewTextBoxColumn();
				authCol.HeaderText = "TotalCount";
				authCol.CellTemplate = new DataGridViewTextBoxCell();

				DataGridViewTextBoxColumn genCol = new DataGridViewTextBoxColumn();
				genCol.HeaderText = "Booked";
				genCol.ReadOnly = true;
				genCol.CellTemplate = new DataGridViewTextBoxCell();

				DataGridViewTextBoxColumn dateTimeCol = new DataGridViewTextBoxColumn();
				dateTimeCol.HeaderText = "Left";
				dateTimeCol.ReadOnly = true;
				dateTimeCol.CellTemplate = new DataGridViewTextBoxCell();

				DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
				col.Width = 0;
				col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
				return new DataGridViewColumn[] { idCol, nameCol, authCol, genCol, dateTimeCol, col};
			}
		}
	}
}
