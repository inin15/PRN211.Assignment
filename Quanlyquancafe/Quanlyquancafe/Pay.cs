using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlyquancafe
{
    public partial class Pay : Form
    {
        public Pay()
        {
            InitializeComponent();
        }
        public Pay(string nameT)
            : this()
        {
            txtNameTable.Text = nameT;
            loadDataForm();
            loadDataBill();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Load thông tin cho người dùng xem
        private void loadDataForm()
        {
            DBContext context = new DBContext();
            DataTable table = context.loadTableWhere(txtNameTable.Text);
            txtSTT.Text = table.Rows[0][1].ToString();
            txtTotal.Text = table.Rows[0][2].ToString();
        }
        private void loadDataBill()
        {
            try
            {
                //Don rac
                pnlBill.Controls.Clear();
                DBContext context = new DBContext();
                DataTable table = context.loadBillWhere(txtNameTable.Text);
                //Load thong tin cac mon trong bill 
                int y = 10;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Label lbl = new Label()
                    {
                        Name = "btnFB" + i,
                        //in ra man hinh tung mon nhu vay no moi dep :)
                        Text = (i + 1) + ".     " + table.Rows[i][2].ToString() + "  X  " + table.Rows[i][3].ToString(),
                        Width = pnlBill.Width - 20,
                        Height = 20,
                        Location = new Point(5, y)
                    };
                    y += 25;
                    pnlBill.Controls.Add(lbl);
                }
            }
            catch
            {
            }

        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            DialogResult ms = MessageBox.Show("Bạn có muốn thanh toán " + txtNameTable.Text + "\nTổng tiền: " + txtTotal.Text + " VNĐ", "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None);
            if (ms == DialogResult.Yes)
            {
                //Tih tien
                setTableNull();
                deleteBill();
                MessageBox.Show("Đã thanh toán " + txtNameTable.Text, "Xong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if (ms == DialogResult.No)
            {
                this.Close();
            }
        }
        //set ban ve rong
        public void setTableNull()
        {
            DBContext context = new DBContext();
            context.ClearTable(txtNameTable.Text);
        }

        //Xoa het bill trong ban
        public void deleteBill()
        {
            DBContext context = new DBContext();
            context.ClearBill(txtNameTable.Text);
        }

        private void Pay_Load(object sender, EventArgs e)
        {

        }
    }
}
