using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using OpenQA.Selenium.Chrome;
using Keys = System.Windows.Forms.Keys;
using OpenQA.Selenium.Interactions;

namespace SeleniumWiform
{
    public partial class frmKTApLuc : Form
    {
        public frmKTApLuc()
        {
            InitializeComponent();
        }

        private void frmInfomation_Load(object sender, EventArgs e)
        {

        }



        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kiểmTraÁpLựcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKTApLuc frm = new frmKTApLuc();
            this.Hide();
            frm.Show();
        }

        private void kiểmTraChứcNăngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKTChucNang fr11 = new frmKTChucNang();
            this.Hide();
            fr11.Show();
        }

        private void kiểmTraBảoMậtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        FirefoxDriver firefoxDriver;


        private void button1_Click(object sender, EventArgs e)
        {
            for (int j = 1; j < 50; j++)
            {
                firefoxDriver = new FirefoxDriver();
                //IWebDriver driver = new FirefoxDriver();
                this.firefoxDriver.Manage().Window.Maximize();
                //truy cập trực tiếp tới địa chỉ tuyệt đối của trang online
                firefoxDriver.Url = "https://online.hcmute.edu.vn/";
                firefoxDriver.Navigate();
                // tìm đối tượng theo ID
                var btnDangNhapn = firefoxDriver.FindElementById("ctl00_lbtDangnhap");//đang cố ý để erro để bắt try cat
                                                                                      //click vào button đăng nhập
                btnDangNhapn.Click();
                //tìm đến chỗ ô tên đăng nhập
                var txtUsername = firefoxDriver.FindElementByXPath("//*[@id=\"ctl00_ContentPlaceHolder1_ctl00_ctl00_txtUserName\"]");
                //gõ tên đăng nhập vào
                txtUsername.SendKeys("15110231");
                //tìm đến ô mật khẩu
                var txtMKn = firefoxDriver.FindElementById("ctl00_ContentPlaceHolder1_ctl00_ctl00_txtPassword");
                //gõ mật khẩu vào
                txtMKn.SendKeys("000030");
                //tìm đến button đăng nhập và nhấn vào nó
                var btn1n = firefoxDriver.FindElementById("ctl00_ContentPlaceHolder1_ctl00_ctl00_btLogin");
                btn1n.Click();
                for (int i = 1; i < 20; i++)
                {

                    firefoxDriver.ExecuteScript("window.open('http://online.hcmute.edu.vn/');");


                }
            }
           
        }

        private void kiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBaoMat frm = new frmBaoMat();
            this.Hide();
            frm.Show();
        }

        private void kiểmTraÁpLựcToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmKTApLuc frm = new frmKTApLuc();
            this.Hide();
            frm.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn thoát !", "Thông báo",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void kiểmTraTốcĐộToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTocDo f = new frmTocDo();
            this.Hide();
            f.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            frmPlayVideo aaa= new frmPlayVideo();
            aaa.ShowDialog();
        }
    }
    
}
