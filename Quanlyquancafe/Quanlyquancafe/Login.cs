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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        //Hàm buttom Exit
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult ms = MessageBox.Show("Bạn có muốn thoát không? ", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (ms == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        //Ham kiem tra Dang nhap
        private bool CheckLogin(string username, string password, string typeA)
        {
            DataProvider provider = new DataProvider();
            DataTable table = provider.loadAccount();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString() == username && table.Rows[i][2].ToString() == password && table.Rows[i][3].ToString() == typeA)
                {
                    name = table.Rows[i][1].ToString();
                    type = table.Rows[i][3].ToString();
                    MessageBox.Show("Xin chào " + name + " :)", "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            return false;
        }

    }
}
