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
    public partial class frmMain : Form
    { //username va password
        private string username;
        private string password;
        //Bill
        string strBill;
        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(string user, string name, string pass, string type) : this()
        {
            username = user;
            password = pass;
           /* lblName.Text = name;
            if (type == "ADMIN")
            {
                lblName.ForeColor = ColorTranslator.FromHtml("red");
                tmiAdmin.Visible = true;
            }*/
        }

        

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
