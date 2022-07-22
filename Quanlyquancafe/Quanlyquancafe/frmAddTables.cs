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
    public partial class frmAddTables : Form
    {
        public frmAddTables()
        {
            InitializeComponent();
        }

        private void frmAddTables_Load(object sender, EventArgs e)
        {
            loadTable();
        }
        //Hàm load thông tin 
        private void loadTable()
        {
            try
            {
                DBContext context = new DBContext();
                DataTable table = context.loadTableF();
                dgvResult.DataSource = table;
            }
            catch
            {
                MessageBox.Show("Không thể tải bàn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Click vào bảng
        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int t = dgvResult.CurrentCell.RowIndex;
            lblText.Text = dgvResult.Rows[t].Cells[0].Value.ToString();
        }
        // Thêm bàn
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                if(name != "")
                {
                    DBContext context = new DBContext();
                    context.AddTable(name);
                    loadTable();
                    MessageBox.Show("Đã thêm bàn thành công!", "Đã thêm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tên bàn trống kìa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Không thể thêm bàn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //Xoá bàn
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Bạn có chắc chắn muốn xoá không?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.Yes)
                {
                    //Nếu nhấn yes
                    DBContext context = new DBContext();
                    context.DelTable(lblText.Text);
                    loadTable();
                    MessageBox.Show("Đã xoá bàn thành công", "Đã xoá", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Nếu nhấn No
            }
            catch
            {
                MessageBox.Show("Không thể xoá bàn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Sửa bàn
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "")
                {
                    DBContext context = new DBContext();
                    context.UpdateTable(txtName.Text, lblText.Text);
                    loadTable();
                    MessageBox.Show("Đã sửa bàn thành công!", "Đã sửa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.ResetText();
                }
                else
                {
                    MessageBox.Show("Tên bàn trống kìa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Không thể sửa bàn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
