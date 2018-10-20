using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Collections.Generic;
using OpenQA.Selenium.Firefox;
//using System.Threading;
//using System.Threading;

namespace SeleniumWiform
{
    public partial class frmTocDo : Form
    {
        private const double timerUpdate = 1000;
        //
        private NetworkInterface[] nicArr;
        private Timer timer;
        public frmTocDo()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            InitializeTimer();
        }
        private void InitializeNetworkInterface()
        {
            nicArr = NetworkInterface.GetAllNetworkInterfaces();
            List<string> goodAdapters = new List<string>();

            foreach (NetworkInterface nicnac in nicArr)
            {
                if (nicnac.SupportsMulticast && nicnac.GetIPv4Statistics().UnicastPacketsReceived >= 1 && nicnac.OperationalStatus.ToString() == "Up")
                {
                    goodAdapters.Add(nicnac.Name);
                    //cmbInterface.Items.Add(nicnac.Name);
                }

            }
            if (goodAdapters.Count != cmbInterface.Items.Count && goodAdapters.Count != 0)
            {
                cmbInterface.Items.Clear();
                foreach (string gadpt in goodAdapters)
                {
                    cmbInterface.Items.Add(gadpt);
                }
                cmbInterface.SelectedIndex = 0;
            }
            if (goodAdapters.Count == 0) cmbInterface.Items.Clear();
        }
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = (int)timerUpdate;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

        }
        public bool IsConnectedToInternet(string host)//hàm kiểm tra kết nối Internet
        {
            Ping p = new Ping();
            try
            {
                PingReply pr = p.Send(host, 3000);
                if (pr.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private void UpdateNetworkInterface()//hiện các thông tin băng thông mạng
        {
            //MessageBox.Show(cmbInterface.Items.Count.ToString());
            if (cmbInterface.Items.Count >= 1)
            {
                // Grab NetworkInterface object that describes the current interface
                NetworkInterface nic = nicArr[cmbInterface.SelectedIndex];

                // Grab the stats for that interface
                IPv4InterfaceStatistics interfaceStats = nic.GetIPv4Statistics();


                long bytesSentSpeed = (long)(interfaceStats.BytesSent - double.Parse(lblBytesSent.Text)) / 1024;
                long bytesReceivedSpeed = (long)(interfaceStats.BytesReceived - double.Parse(lblBytesReceived.Text)) / 1024;

                if(IsConnectedToInternet("google.com"))
                {
                    cmbInterface.Enabled = true;
                    lblInternetConect.Text = "Conect";
                    lblInternetConect.ForeColor = Color.Blue;
                }
                else
                {
                    cmbInterface.Enabled = false;
                    lblInternetConect.Text = "Disconect";
                    lblInternetConect.ForeColor = Color.Red;
                }
                // Update the labels
                lblSpeed.Text = nic.Speed.ToString();
                lblInterfaceType.Text = nic.NetworkInterfaceType.ToString();
                lblSpeed.Text = (nic.Speed).ToString("N0");
                lblBytesReceived.Text = interfaceStats.BytesReceived.ToString("N0");
                lblBytesSent.Text = interfaceStats.BytesSent.ToString("N0");
                lblUpload.Text = bytesSentSpeed.ToString() + " KB/s";
                lblDownload.Text = bytesReceivedSpeed.ToString() + " KB/s";

                UnicastIPAddressInformationCollection ipInfo = nic.GetIPProperties().UnicastAddresses;

                foreach (UnicastIPAddressInformation item in ipInfo)
                {
                    if (item.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        labelIPAddress.Text = item.Address.ToString();
                        //uniCastIPInfo = item;
                        break;
                    }
                }
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            InitializeNetworkInterface();
            UpdateNetworkInterface();

        }
        public int s = 0, m = 0, h = 0, sml = 0;

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        FirefoxDriver firefoxDriver;
           
     
      
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Refresh();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate("http://google.com/");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(txtAddress.Text);
        }

        private void kiểmThửChứcNăngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKTChucNang f1 = new frmKTChucNang();
            this.Hide();
            f1.Show();
        }

        private void kiểmThửBảoMậtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBaoMat f1 = new frmBaoMat();
            this.Hide();
            f1.Show();
        }

        private void kiểmThửÁpLựcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKTApLuc f1 = new frmKTApLuc();
            this.Hide();
            f1.Show();
        }

        private void kiểmThửTốcĐộToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTocDo f1 = new frmTocDo();
            this.Hide();
            f1.Show();
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

        void Count()
        {
            timer1.Start();
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            timer1.Start();
            webBrowser1.ScriptErrorsSuppressed = true;
            if (IsConnectedToInternet("google.com") == false)
            {
                MessageBox.Show("Kiểm tra kết nối internet và thử lại",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

                this.webBrowser1.Navigate("https://online.hcmute.edu.vn/");
            }
            btnCheck.Text = "Đang kiểm tra";
            btnCheck.Enabled = false;
           
           
           // timer1.Stop();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            string ss, sm, sh,ssml;
            sml++;
            if (sml == 5)
            {
                btnCheck.Text = "Kiểm tra";
                btnCheck.Enabled = true;
                lblTimeRun.Text = "00 : 00 : 00 :  05";
            }
            if (sml == 10)

            {
                sml = 0;
                s++;
                ssml = sml.ToString();
                if (s == 60)
                {
                    m++;
                    s = 0;
                    if (m == 60)
                    {
                        h++;
                        m = 0;
                    }
                }
            }
            else ssml = "0" + sml.ToString();
            if (s < 10)
                ss = "0" + s.ToString();
            else
                ss = s.ToString();
            if (m < 10)
                sm = "0" + m.ToString();
            else
                sm = m.ToString();
            if (h < 10)
                sh = "0" + h.ToString();
            else
                sh = h.ToString();
            


            lblTime.Text = sh + " : " + sm+ " : " + ss + " : "+ssml;
        }
    }
}
