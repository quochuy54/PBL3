using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAL;
namespace QuanLyThietBiPhongHocHongVaTinhTrangXuLy
{
    public partial class FRoom : Form
    {
        public FRoom(string zoneid)
        {
            InitializeComponent();
            setCBBSort();
            setCBBSearch();
            cbbTimkiem.Text = zoneid;
            if (zoneid != "")
            {
                dataGridView_RoomShow.DataSource = BUS_RoomData.Instance.BUS_RoomShowByIDZone(zoneid);
            }
            else
            {
                dataGridView_RoomShow.DataSource = BUS_RoomData.Instance.BUS_RoomShow();
            }
        }
        private void setCBBSort()
        {
            cbbSort.Items.AddRange(new string[]
                {
                    "roomId",
                    "zoneId",
                    "roomFunction"
                });
        }
        private void setCBBSearch()
        {
            foreach (var item in BUS_MainData.Instance.BUS_ZONE())
            {
                cbbTimkiem.Items.Add(item.zoneId.ToString());
            }

        }
        public void ShowSearch(string zoneId)
        {
            dataGridView_RoomShow.DataSource = BUS_RoomData.Instance.BUS_RoomShowByIDZone(zoneId);
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            dataGridView_RoomShow.DataSource = BUS_RoomData.Instance.BUS_RoomShow();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            FRoomEdit f = new FRoomEdit("","Add");
            f.refresh += new FRoomEdit.refreshData(ShowSearch);
            f.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string roomid = dataGridView_RoomShow.CurrentRow.Cells["roomID"].Value.ToString();
            FRoomEdit f = new FRoomEdit(roomid, "Edit");
            f.refresh += new FRoomEdit.refreshData(ShowSearch);
            f.Show();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string roomid = dataGridView_RoomShow.CurrentRow.Cells["roomID"].Value.ToString();
            BUS_RoomData.Instance.BUS_DELETEROOM(roomid);
            MessageBox.Show("Xóa thành công!");
            dataGridView_RoomShow.DataSource = BUS_RoomData.Instance.BUS_RoomShow();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            dataGridView_RoomShow.DataSource = BUS_RoomData.Instance.BUS_Sort(cbbSort.SelectedItem.ToString());
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            ShowSearch(cbbTimkiem.Text);
        }

        private void btnShowEquipments_Click(object sender, EventArgs e)
        {
            if (dataGridView_RoomShow.SelectedRows.Count == 1)
            {
                string roomId = dataGridView_RoomShow.CurrentRow.Cells["roomID"].Value.ToString();
                FEquipment f = new FEquipment(roomId);
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng!");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
