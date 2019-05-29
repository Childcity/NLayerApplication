using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Interfaces;
using NLayerApp.BLL.Services;
using NLayerApp.FrontApp.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NLayerApp.FrontApp {
	public partial class Form1 :Form {
		AfishaServiceAdapter afishaSVC = new AfishaServiceAdapter();

		public Form1() {
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e) {
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			foreach(var col in PlayTableModel.Columns) {
				dataGridView1.Columns.Add(col);
			}

			dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			foreach(var col in TicketTableModel.Columns) {
				dataGridView2.Columns.Add(col);
			}

			updatePlays();
			dataGridView1.SelectionChanged += onDataGridView1_SelectionChanged;
			dataGridView2.SelectionChanged += onDataGridView2_SelectionChanged;
			dataGridView1.CellEndEdit += onDataGridView1_CellEndEdit;
			dataGridView2.CellEndEdit += onDataGridView1_CellEndEdit;
			//dataGridView1.RowsRemoved += onDataGridView1_CellEndEdit;
			//dataGridView2.RowsRemoved += onDataGridView1_CellEndEdit;
		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {
		}

		private void button1_Click(object sender, EventArgs e) {
			int selTicket = dataGridView2.CurrentCell.RowIndex;
			var plays = afishaSVC.OperateOnTicket(TicketOperation.Buy, int.Parse(dataGridView2[5, selTicket].Value.ToString()));
			updateTickets(plays.ElementAt(dataGridView1.CurrentCell.RowIndex));
		}

		private void button2_Click(object sender, EventArgs e) {
			int selTicket = dataGridView2.CurrentCell.RowIndex;
			var plays = afishaSVC.OperateOnTicket(TicketOperation.Book, int.Parse(dataGridView2[5, selTicket].Value.ToString()));
			updateTickets(plays.ElementAt(dataGridView1.CurrentCell.RowIndex));
		}

		private void button3_Click(object sender, EventArgs e) {
			int selTicket = dataGridView2.CurrentCell.RowIndex;
			var plays = afishaSVC.OperateOnTicket(TicketOperation.MakeBookAsBought, int.Parse(dataGridView2[5, selTicket].Value.ToString()));
			updateTickets(plays.ElementAt(dataGridView1.CurrentCell.RowIndex));
		}

		private void textBox1_TextChanged(object sender, EventArgs e) {
			var plays = afishaSVC.GetAfishaPlays(textBox1.Text);

			dataGridView1.Rows.Clear();
			for(int rowInx = 0; rowInx < plays.Count(); rowInx++) {
				dataGridView1.Rows.Add(
					rowInx + 1
					, plays.ElementAt(rowInx).Name
					, plays.ElementAt(rowInx).Author
					, plays.ElementAt(rowInx).Genre
					, plays.ElementAt(rowInx).DateTime
					, plays.ElementAt(rowInx).Id);
			}
		}

		private void onDataGridView2_SelectionChanged(object sender, EventArgs e) {
			var rowsCount = dataGridView2.SelectedRows.Count;
			if(rowsCount == 0 || rowsCount > 1) {
				panel1.Enabled = false;
				return;
			}

			var plays = afishaSVC.GetAfishaPlays(null);
			int selPlayIdx = dataGridView2.CurrentCell.RowIndex;

			if(plays.Count() <= selPlayIdx) {
				panel1.Enabled = false;
				return;
			}

			panel1.Enabled = true;
			
		}

		private void onDataGridView1_SelectionChanged(object sender, EventArgs e) {
			var rowsCount = dataGridView1.SelectedRows.Count;
			if(rowsCount == 0 || rowsCount > 1) {
				dataGridView2.Visible = false;
				return;
			}

			var plays = afishaSVC.GetAfishaPlays(null);
			int selPlayIdx = dataGridView1.CurrentCell.RowIndex;

			if(plays.Count() <= selPlayIdx) {
				dataGridView2.Visible = false;
				return;
			}

			dataGridView2.Visible = true;
			var selPlay = plays.ElementAt(selPlayIdx);
			updateTickets(selPlay);
		}

		private void onDataGridView1_CellEndEdit(object sender, EventArgs e) {
			var senderDataGrid = (DataGridView)sender;
			var oldPlays = afishaSVC.GetAfishaPlays(null);
			var newPlays = new List<PlayDTO>();
			try {
				for(int i = 0; i < dataGridView1.Rows.Count - 1; i++) {
					if(dataGridView1.Rows[i].Cells[1].Value == null ||
						dataGridView1.Rows[i].Cells[2].Value == null ||
						dataGridView1.Rows[i].Cells[3].Value == null ||
						dataGridView1.Rows[i].Cells[4].Value == null ) {
						return;
					}

					var newTickets = new List<TicketDTO>();

					if(senderDataGrid.Name == "dataGridView2" && i == dataGridView1.CurrentCell.RowIndex) {
						for(int j = 0; j < dataGridView2.Rows.Count - 1; j++) {
							if(dataGridView2.Rows[j].Cells[1].Value == null ||
								dataGridView2.Rows[j].Cells[2].Value == null) {
								return;
							}

							int bookedCount = 0;
							int left = int.Parse(dataGridView2.Rows[j].Cells[2].Value.ToString());

							if(dataGridView2.Rows[j].Cells[3].Value != null) {
								int.TryParse(dataGridView2.Rows[j].Cells[3].Value.ToString(), out bookedCount);
								int.TryParse(dataGridView2.Rows[j].Cells[4].Value.ToString(), out left);
							}

							newTickets.Add(new TicketDTO {
								//Id = int.Parse(dataGridView2.Rows[j].Cells[0].Value.ToString()),
								Price = int.Parse(dataGridView2.Rows[j].Cells[1].Value.ToString()),
								TotalCount = int.Parse(dataGridView2.Rows[j].Cells[2].Value.ToString()),
								BookedCount = bookedCount,
								Left = left
							});
						}
					} else {
						if(dataGridView1.Rows[i].Cells[5].Value != null)
							newTickets = oldPlays.Single(
								pl => pl.Id == int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString())
								).TicketDTOs.ToList();
					}


					newPlays.Add(new PlayDTO {
						//Id = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()),
						Name = dataGridView1.Rows[i].Cells[1].Value.ToString(),
						Author = dataGridView1.Rows[i].Cells[2].Value.ToString(),
						Genre = dataGridView1.Rows[i].Cells[3].Value.ToString(),
						DateTime = DateTime.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()),
						TicketDTOs = newTickets
					});
				}
			} catch(Exception ex) {
				MessageBox.Show(
					"Please, enter proper value [" + ex.Message +"]",
					"Warning",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				return;
			}
			afishaSVC.UpdateAfishaPlays(newPlays);
			BeginInvoke(new MethodInvoker(updatePlays));
		}

		private void updateTickets(PlayDTO selPlay) {
			dataGridView2.Rows.Clear();
			for(int rowInx = 0; rowInx < selPlay.TicketDTOs.Count(); rowInx++) {
				var selPlayTicket = selPlay.TicketDTOs.ElementAt(rowInx);
				dataGridView2.Rows.Add(
					rowInx + 1
					, selPlayTicket.Price
					, selPlayTicket.TotalCount
					, selPlayTicket.BookedCount
					, selPlayTicket.Left
					, selPlayTicket.Id
					);
			}
		}

		private void updatePlays() {
			var plays = afishaSVC.GetAfishaPlays(null);
			resetDataGridView();
			for(int rowInx = 0; rowInx < plays.Count(); rowInx++) {
				dataGridView1.Rows.Add(
					rowInx + 1
					, plays.ElementAt(rowInx).Name
					, plays.ElementAt(rowInx).Author
					, plays.ElementAt(rowInx).Genre
					, plays.ElementAt(rowInx).DateTime
					, plays.ElementAt(rowInx).Id
					);
			}
		}

		public void resetDataGridView() {

			dataGridView1.DataSource = null;
			dataGridView1.Rows.Clear();
		}

		private void groupBox1_Enter(object sender, EventArgs e) {

		}
	}
}
