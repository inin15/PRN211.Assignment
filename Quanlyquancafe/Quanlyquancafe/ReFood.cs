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
    public partial class ReFood : Form
    {
        private DataTable datatable;
        private bool condition = true;
        public ReFood()
        {
            InitializeComponent();
        }
        public ReFood(string nameTableFrom)
                    : this()
        {
            loadDataTable();
            cbbTable.Text = nameTableFrom;
        }
        //Load len cho nguoi dung chon ban
        private void loadDataTable()
        {
            DBContext context = new DBContext();
            DataTable table = context.loadTableF();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                cbbTable.Items.Add(table.Rows[i][0].ToString());
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        //Thay doi datagirdview
        private void cbbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Dieu kien dung
                DBContext context = new DBContext();
                datatable = context.loadTableFoodBill(cbbTable.Text);
                txtTotal.Text = datatable.Rows[0][3].ToString();
                for (int i = 0; i < datatable.Rows.Count; i++)
                {
                    cbbFood.Items.Add(datatable.Rows[i][0].ToString());
                }
            }
            catch
            {
                condition = false;
            }
        }

        private void cbbFood_SelectedIndexChanged(object sender, EventArgs e)
        {
            nudCount.Value = Int16.Parse(datatable.Rows[cbbFood.SelectedIndex][1].ToString());
        }

        //Nhan nut va thuc hien giam mon
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (nudCountReF.Value <= nudCount.Value && condition == true && cbbFood.Text != "")
            {
                DialogResult ms = MessageBox.Show("Bạn có muốn bỏ " + nudCountReF.Text + " món " + cbbFood.Text + " ở bàn " + cbbTable.Text + " không?", "Xác nhận", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None);
                if (ms == DialogResult.Yes)
                {
                    //Bo mon
                    reFood();
                    MessageBox.Show("Đã bỏ món thành công!", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (ms == DialogResult.No)
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Không thể giảm món!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //sua bill
        private void reFood()
        {
            //sua bill
            int count = Int16.Parse((nudCount.Value - nudCountReF.Value).ToString());
            float total = float.Parse((float.Parse(txtTotal.Text) - float.Parse(datatable.Rows[cbbFood.SelectedIndex][2].ToString()) * (float)nudCountReF.Value).ToString());
            DBContext context = new DBContext();
            context.giammon(cbbTable.Text, cbbFood.Text, count, total);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
