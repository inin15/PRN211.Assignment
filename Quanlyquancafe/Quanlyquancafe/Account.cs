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
    public partial class Account : Form
    {
        string oldusername;
        public Account()
        {
            InitializeComponent();
        }

        private void Account_Load(object sender, EventArgs e)
        {
            Load();
        }
        private void Load()
        {
            DBContext context = new DBContext();
            DataTable table = context.loadAccount();
            dgvResult.DataSource = table;
        }
        private void clear()
        {
            txtUsername.ResetText();
            txtDisplayname.ResetText();
            txtPassword.ResetText();
            cbAdmin.Checked = false;
        }

        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvResult.Rows[e.RowIndex];
                txtUsername.Text = row.Cells[0].Value.ToString();
                oldusername = row.Cells[0].Value.ToString();
                txtDisplayname.Text = row.Cells[1].Value.ToString();
                txtPassword.Text = row.Cells[2].Value.ToString();
                if (row.Cells[3].Value.ToString() == "ADMIN")
                    cbAdmin.Checked = true;
                else if (row.Cells[3].Value.ToString() == "CASHIER")
                    cbAdmin.Checked = false;
            }
        }
        //thêm tài khoản
        private void btnAdd_Click(object sender, EventArgs e)
        { 
            try { 
                string username = txtUsername.Text;
                string displayname = txtDisplayname.Text;
                string password = txtPassword.Text;
                string type = "CÁHIER";
                if (cbAdmin.Checked == true)
                {
                    type = "ADMIN"; // admin
                }
                DBContext context = new DBContext();
                context.AddAccount(username, displayname, password, type);
                MessageBox.Show("Thêm thành công!\n Tài khoản" + displayname + "đã được thêm", "Đã thêm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Load();
                clear();
            }
            catch
            {
                MessageBox.Show("Không thêm được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Bạn có chắc chắn muốn xoá tài khoản"+oldusername+"không?","Xác nhận",MessageBoxButtons.OK,MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.Yes)
                {
                    //Nhấn yes
                    string name = txtUsername.Text;
                    DBContext context = new DBContext();
                    context.DelAccount(name);
                    MessageBox.Show("Xoá thành công: \n Tài khoản" + name + " đã được xoá", "Đã xoá", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Load();
                    clear();
                }  //Nhấn no  
            }
            catch
            {
                MessageBox.Show("Không xoá được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string newusername = txtUsername.Text;
                string newdisplayname = txtDisplayname.Text;
                string newpassword = txtPassword.Text;
                string type = "CÁHIER";
                if(cbAdmin.Checked == true)
                {
                    type = "ADMIN";
                }
                DBContext context = new DBContext();
                context.UpdateAccount(newusername, newdisplayname, newpassword, type, oldusername);
                MessageBox.Show("Chỉnh sửa thành công: \n Tài khoản" + oldusername + "đã chỉnh sửa.", "Đã sửa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Load();
                clear();
            }
            catch
            {
                MessageBox.Show("Không sửa được!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
