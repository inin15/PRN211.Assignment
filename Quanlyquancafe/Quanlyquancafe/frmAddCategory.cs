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
    public partial class frmAddCategory : Form
    {
        public frmAddCategory()
        {
            InitializeComponent();
        }

        private void frmAddCategory_Load(object sender, EventArgs e)
        {
            load();
        }
        //Hàm load thông tin
        private void load()
        {
            try
            {
                DBContext context  = new DBContext();
                DataTable table = context.loadCategory();
                dgvResult.DataSource = table;
            }
            catch
            {
                MessageBox.Show("Không thể tải dữ liệu","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        //Thêm món mới
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtName.Text != "")
                {
                    DBContext context=new DBContext();
                    context.AddCate(txtName.Text);
                    MessageBox.Show("Thêm món thành công!","Đã thêm",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtName.ResetText();
                    load();
                }
                else
                {
                    MessageBox.Show("Tên món mới bị trống!", "Thiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch
            {
                MessageBox.Show("Thêm món không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //Khi click chọn thông tin món sẽ được hiển thị trên datagritview
        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int t = dgvResult.CurrentCell.RowIndex;
            lblText.Text = dgvResult.Rows[t].Cells[0].Value.ToString();
        }
        //Xoá
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Bạn có chắc chắn muốn xoá không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes){
                    //Nếu nhấn yes
                    DBContext context = new DBContext();
                    context.DelCate(lblText.Text);
                    MessageBox.Show("Xoá thành công!","Đã xoá",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    load();
                }//Nếu nhấn No
            }
            catch
            {
                MessageBox.Show("Không thể xoá danh mục này!", " Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //chỉnh sửa
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                DBContext context = new DBContext();
                context.UpdateCate(txtName.Text,lblText.Text);
                MessageBox.Show("Sửa thành công!", "Đã sửa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load();
                txtName.ResetText();
            }
            catch
            {
                MessageBox.Show("Không thể chỉnh sửa danh mục này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
