using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumWiform
{
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }
    
        public bool IsConnectedToInternet(string host)
        {
            Ping p = new Ping();
            try
            {
                PingReply pr = p.Send(host, 3000);
                if (pr.Status == IPStatus.Success)
                {
                    return true;
                }
                //return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
      
        //SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-7D1PMHD\\SQLEXPRESS;Initial Catalog=TLCN;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            
              if (IsConnectedToInternet("google.com"))
              {

                  SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-7D1PMHD\SQLEXPRESS;Initial Catalog=TLCN;Integrated Security=True");

                  string sqlSelect = "";
                  try
                  {
                      conn.Open();
                      string tk = txtUser.Text;
                          string mk = txtPassword.Text;
                      string sql = "select * from Login where TenDangNhap='" + tk + "' and MatKhau='" + mk + "'";
                      SqlCommand cmd = new SqlCommand(sql, conn);
                      SqlDataReader dta = cmd.ExecuteReader();
                      if (dta.Read() == true)
                      {
                          this.Hide();
                          frmKTChucNang aa = new frmKTChucNang();
                          aa.Show();
                      }
                      else
                          MessageBox.Show("Kiểm tra lại tên đăng nhập và mật khẩu.. !","Thông báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);

                  }
                  catch
                  {
                      MessageBox.Show("Kết nối cơ sở dữ liệu thất bại..",
                          "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  }

              }
              else
                  MessageBox.Show("Kiểm tra kết nối internet và thử lại",
                      "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
         // }*/ //Phần đăng nhập và kiểm tra kết nối internet, bỏ qua để test cho nhanh.. khi nào cần thì xài

        }
        //Data Source=DESKTOP-7D1PMHD\SQLEXPRESS;Initial Catalog=TLCN;Integrated Security=True
        
        private void label3_Click(object sender, EventArgs e)
        {
           DialogResult dl=  MessageBox.Show("Quên thì thôi ! :D :))", 
               "Thông báo",MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            
        }
        

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            
            txtUser.Focus();
            //txtUser.F=true;
            
        }
    }
}
