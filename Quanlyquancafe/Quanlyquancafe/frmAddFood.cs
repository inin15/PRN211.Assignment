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
    public partial class frmAddFood : Form
    {
        string oldname;
        public frmAddFood()
        {
            InitializeComponent();
        }

        private void frmAdFood_Load(object sender, EventArgs e)
        {
            load();
            setNameCategory();
        }
        //cap nhat category de chon
        private void setNameCategory()
        {
            DBContext context = new DBContext();
            DataTable table = context.loadCategory();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                cbbAddCate.Items.Add(table.Rows[i][0].ToString());
            }
        }

        //ham load thong tin
        private void load()
        {
            try
            {
                DBContext context = new DBContext();
                DataTable table = context.loadAllFood();
                dgvResult.DataSource = table;
            }
            catch
            {
                MessageBox.Show("Tải không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //reset
        private void clear()
        {
            txtAddName.ResetText();
            cbbAddCate.ResetText();
            nudAddPrice.Value = 1000;
            cbbAddCate.Focus();
        }

        //click table
        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvResult.Rows[e.RowIndex];
                    txtAddName.Text = row.Cells[0].Value.ToString();
                    oldname = row.Cells[0].Value.ToString();
                    cbbAddCate.Text = row.Cells[1].Value.ToString();
                    nudAddPrice.Value = Int32.Parse(row.Cells[2].Value.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Lỗi data!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //them mon
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtAddName.Text;
                string nameCate = cbbAddCate.Text;
                float price = Convert.ToInt64(nudAddPrice.Value);
                DBContext context = new DBContext();
                context.AddFood(nameCate, name, price);
                MessageBox.Show("Thêm thành công", "Đã thêm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
                load();
            }
            catch
            {
                MessageBox.Show("Thêm không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Xoa mon
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có chắc xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    //Nhan yes
                    string name = txtAddName.Text;
                    DBContext context = new DBContext();
                    context.DelFood(name);
                    MessageBox.Show("Xóa thành công!", "Đã xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    load();
                }
                //nhan no
            }
            catch
            {
                MessageBox.Show("Xóa không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Sua mon
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string newname = txtAddName.Text;
                string nameCate = cbbAddCate.Text;
                float price = Convert.ToInt32(nudAddPrice.Value);
                DBContext context = new DBContext();
                context.UpdateFood(nameCate, newname, price, oldname);
                MessageBox.Show("Sửa thành công!", "Đã sửa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
                load();
            }
            catch
            {
                MessageBox.Show("Sửa không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

