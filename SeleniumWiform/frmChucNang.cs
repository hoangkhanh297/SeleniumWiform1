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
    public partial class frmKTChucNang : Form
    {

        public frmKTChucNang()
        {
            InitializeComponent();

        }

        // khởi tạo FirefoxDriver để mở trình duyệt
        FirefoxDriver firefoxDriver;
        //các hàm dùng để chụp màn hình
        private static Bitmap screenBitmap;
        private static Graphics screenGraphics;
        string getDate()
        {
            
            string strDate = DateTime.Now.ToString().Trim();
            strDate = strDate.Substring(0, 2);


           // return str;

            string strMounth = DateTime.Now.ToString().Trim();
            strMounth = strMounth.Substring(3, 2);
            //return str;

            string strYear = DateTime.Now.ToString().Trim();
            strYear = strYear.Substring(6, 4);
            string str = strDate + strMounth + strYear;
            return str;
        }
        void NavigateByID(string Id, string TenKt)//hàm đi đến các công cụ chính trong trang online
        {
           
            //SaveFileDialog saveDialog = new SaveFileDialog();
            //// chọn đường dẫn lưu ảnh chụp màn hình
            //saveDialog.Filter = "PNG Files (.png)|*.png|All Files (*.*)|*.*";
            //saveDialog.FilterIndex = 1;
            ////


            //if (saveDialog.ShowDialog() == DialogResult.OK)
            //{
                //đóng giao diện 
                this.Hide();
          //  MessageBox.Show(getDate());
                //khởi động trình duyệt
                firefoxDriver = new FirefoxDriver();
                //phóng to trình duyệt ra toàn màn hình
                this.firefoxDriver.Manage().Window.Maximize();
                //truy cập trực tiếp tới địa chỉ tuyệt đối của trang online
                firefoxDriver.Url = "https://online.hcmute.edu.vn/";
                firefoxDriver.Navigate();
            try
            {
                // tìm đối tượng theo ID
                var btnDangNhap = firefoxDriver.FindElementById("ctl00_lbtDangnhap");//đang cố ý để erro để bắt try cat
                                                                                     //click vào button đăng nhập
                btnDangNhap.Click();
                //tìm đến chỗ ô tên đăng nhập
                var txtTenDangNhap = firefoxDriver.FindElementByXPath("//*[@id=\"ctl00_ContentPlaceHolder1_ctl00_ctl00_txtUserName\"]");
                //gõ tên đăng nhập vào
                txtTenDangNhap.SendKeys("15110231");

                Thread.Sleep(1000); // ngủ 1 giây

                //tìm đến ô mật khẩu
                var txtMK = firefoxDriver.FindElementByXPath("//*[@id=\"ctl00_ContentPlaceHolder1_ctl00_ctl00_txtPassword\"]");
                //gõ mật khẩu vào
                txtMK.SendKeys("000030");

                Thread.Sleep(1000); //tiếp tục ngủ 1 giây để xem kết quả (Sẽ tắt khi mạng yếu)
                                    //tìm đến button đăng nhập và nhấn vào nó
                var btn1 = firefoxDriver.FindElementByXPath("//*[@id=\"ctl00_ContentPlaceHolder1_ctl00_ctl00_btLogin\"]");
                btn1.Click();
                //tìm đến chỗ THÔNG TIN CÁ NHÂN và nhấn vào nó
                var btn2 = firefoxDriver.FindElementById(Id);
                btn2.Click();
                Thread.Sleep(1000);
                // lại ngủ 1 giây
                Random a = new Random();
                int TenAnh = a.Next(0, 99999999);
                // lưu ảnh chụp màn hình cuối cùng(đoạn này copy hoàn toàn trên Google)
                screenBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                            Screen.PrimaryScreen.Bounds.Height,
                                            PixelFormat.Format32bppArgb);
                screenGraphics = Graphics.FromImage(screenBitmap);
                screenGraphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y,
                                        0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                screenBitmap.Save("G:\\Graduation_Thesis\\Project\\SeleniumWiform\\LuuAnh\\Pictute " + TenKt + " " + getDate() + " " + TenAnh.ToString(), ImageFormat.Png);
                DialogResult dlr = MessageBox.Show("Xong, bạn có muốn tiếp tục. \n" , "Thông báo", MessageBoxButtons.YesNo);

                if (dlr == DialogResult.Yes)
                {
                   
                    
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
            
            catch (Exception e)
            {

                DialogResult dlr = MessageBox.Show("Có lỗi, bạn có muốn tiếp tục. \n" + e, "Thông báo", MessageBoxButtons.YesNo);

                if (dlr == DialogResult.Yes)
                {
                    // lưu ảnh chụp màn hình cuối cùng(đoạn này copy hoàn toàn trên Google)
                    screenBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                                Screen.PrimaryScreen.Bounds.Height,
                                                PixelFormat.Format32bppArgb);
                    screenGraphics = Graphics.FromImage(screenBitmap);
                    screenGraphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y,
                                            0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                    int TenBien;
                    Random a = new Random();
                    TenBien = a.Next(0, 99999999);
                    screenBitmap.Save("G:\\Graduation_Thesis\\Project\\SeleniumWiform\\LuuAnh\\Pictute " + TenKt + " " + getDate() + " " + TenBien.ToString(), ImageFormat.Png);


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
                //  }



            }
        }
        [STAThread]

        private void button1_Click(object sender, EventArgs e) //Phần THỜI KHOÁ BIỂU 
                                                               //đã sửa xong và chạy ngon lành
        {
            string IDThoiKhoaBieu;
            IDThoiKhoaBieu = "ctl00_ContentPlaceHolder1_ctl00_ctl00_lnkThoiKhoaBieu";
            NavigateByID(IDThoiKhoaBieu,"ThoiKhoaBieu");
        }

        private void Form1_Load(object sender, EventArgs e)//hàm loadform
        {
            //  Form1.Resize=false

        }

        private void button5_Click(object sender, EventArgs e)//Phần XEM ĐIỂM
        {
            string IDXemDiem;
            IDXemDiem = "ctl00_ContentPlaceHolder1_ctl00_ctl00_lnkDiem";
            NavigateByID(IDXemDiem,"XemDiem");
        }
        private void btnYoutube_Click(object sender, EventArgs e)//Xem thông tin THANH TOÁN HỌC PHÍ
        {
            string IDThanhToanHocPhi;
            IDThanhToanHocPhi = "ctl00_ContentPlaceHolder1_ctl00_ctl00_lnkThanhToan";
            NavigateByID(IDThanhToanHocPhi,"ThanhToanHocPhi");
        }

        private void btnWhoer_Click(object sender, EventArgs e)//xem THÔNG TIN MÁY CHỦ PROXY
        {
            //Khởi tạo firefox in new windown
            firefoxDriver = new FirefoxDriver();
            //đóng giao diện 
          //  this.Hide();
            //phóng to trình duyệt ra toàn màn hình
            this.firefoxDriver.Manage().Window.Maximize();
            //truy cập trực tiếp tới địa chỉ tuyệt đối của trang online
            firefoxDriver.Url = "https://whoer.net";
            firefoxDriver.Navigate();
            // Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//Phần TRUY CẬP TRANG
                                                                                           //Đã xong
        {
            //Khởi tạo firefox in new windown
            firefoxDriver = new FirefoxDriver();
            //đóng giao diện 
            this.Hide();
            //phóng to trình duyệt ra toàn màn hình
            this.firefoxDriver.Manage().Window.Maximize();
            //truy cập trực tiếp tới địa chỉ tuyệt đối của trang online
            firefoxDriver.Url = "https://online.hcmute.edu.vn/";
            firefoxDriver.Navigate();
           // Application.Exit();
        }

        private void btnTrangCuaBan_Click(object sender, EventArgs e)//Phần THÔNG TIN CÁ NHÂN
                                                                     //đã sửa xong
        {
            string IDThongTinCaNhan;
            IDThongTinCaNhan = "ctl00_ContentPlaceHolder1_ctl00_ctl00_lnkInfo";
            NavigateByID(IDThongTinCaNhan,"ThongTinCaNhan");
        }

        private void btnCTDaoTao_Click(object sender, EventArgs e)//phần CHƯƠNG TRÌNH ĐÀO TẠO
        {
            string IDCTDT;
            IDCTDT = "ctl00_ContentPlaceHolder1_ctl00_ctl00_lnkStudyProgram";
            NavigateByID(IDCTDT,"ChuongTrinhDaoTao");
        }

        private void btnDKHocPhan_Click(object sender, EventArgs e)//Phần ĐĂNG KÍ HỌC PHẦN
        {
            string IDDKhocPhan;
            IDDKhocPhan = "ctl00_ContentPlaceHolder1_ctl00_ctl00_lnkDangKy";
            NavigateByID(IDDKhocPhan,"ChuongTrinhDaoTao");
        }

        private void btnLichThi_Click(object sender, EventArgs e)//PHẦN LỊCH THI
        {
            string IDLichThi;
            IDLichThi = "ctl00_ContentPlaceHolder1_ctl00_ctl00_lnkLichThi_";
            NavigateByID(IDLichThi,"LichThi");
        }

        private void tToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void kiểmTraBảoMậtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBaoMat frm = new frmBaoMat();
            this.Hide();
            frm.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có chắc chắn muốn thoát !", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dlr== DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void kiểmTraÁpLựcToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmKTApLuc frm = new frmKTApLuc();
            this.Hide();
            frm.Show();
        }

        private void kiểmThửTốcĐộToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTocDo fr = new frmTocDo();
            this.Hide();
            fr.Show();
        }

        private void tốcĐộToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTocDo s = new frmTocDo();
            this.Hide();
            s.Show();
            
        }
    }
}
   

