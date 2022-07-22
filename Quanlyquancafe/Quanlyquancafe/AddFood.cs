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
    public partial class AddFood : Form
    {
        bool isFood = false;
        public AddFood()
        {
            InitializeComponent();
        }
        public AddFood(string nameT, string nameF,string sttT):this()
        {
            loadFood();
            //Chọn giá trị gợi ý
            txtBan.Text = nameT;
            cbbFood.Text = nameF;
            txtSTT.Text = sttT;

        }
        //Hàm load thông tin
        private void loadFood()
        {
            DBContext context = new DBContext();
            DataTable table = context.loadAllFood();
            for(int i = 0; i<table.Rows.Count; i++)
            {
                cbbFood.Items.Add(table.Rows[i][0].ToString());

            }

        }
        //àm mở bàn
        public void openTable()
        {
            DBContext context = new DBContext();
            context.ResetTable(txtBan.Text);
        }
        public void addFood()
        {
            DBContext context = new DBContext();
            context.ThemMon(txtBan.Text, cbbFood.Text, Int16.Parse(nudCount.Value.ToString()));
            //Đồng thời tăng total
            setTotal();
        }
        //Tăng số lượng món lên
        public void addCountFood()
        {
            DBContext context = new DBContext();
            context.TangSLMon(txtBan.Text, cbbFood.Text, Int16.Parse(nudCount.Value.ToString()));
            //đồng thời tăng total
            setTotal();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiểm tra bàn trong k
                if (txtSTT.Text == "Trong")
                {
                    //Nếu bàn trống thì mở bàn mới và thêm món
                    openTable();
                    addFood();
                    this.Close();
                }else if(txtSTT.Text == "Online")
                {
                    //Bàn đang hoạt động. chỉ thêm món
                    isCountFood();
                    if (isFood == false)
                    {
                        //Nếu món chưa có thì thêm món
                        addFood();
                        this.Close ();
                    }
                    else
                    {
                        //Nếu món có rồi thì tăng số lượng
                        addCountFood();
                        this.Close () ;
                    }
                }
                MessageBox.Show("Thêm món thành công", " Đã thêm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Thêm món không thành công", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Kiểm tra xem món đã tồn tại chưa|| nếu có: true không có false
        public void isCountFood()
        {
            try
            {
                DBContext context = new DBContext();
                DataTable table = context.checkFoodTable(txtBan.Text, cbbFood.Text);
                if (Int16.Parse(table.Rows[0][3].ToString()) > 0)
                {
                    isFood = true;
                }
            }
            catch
            {
                isFood = false;
            }
        }
        //Hàm trả về đơn giá món hiện tại
        private float getDongGia()
        {
            DBContext context = new DBContext();
            DataTable table = context.getPrice(cbbFood.Text);
            return Int16.Parse(table.Rows[0][0].ToString());
        }
        //Set Tông tiền
        private void setTotal()
        {
            float total = getDongGia()*(float)nudCount.Value;
            DBContext context = new DBContext ();
            context.setTotal(txtBan.Text, float.Parse(total.ToString()));
        }
    }
}
