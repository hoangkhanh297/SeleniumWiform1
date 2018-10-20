using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeleniumWiform
{
    public partial class frmPlayVideo : Form
    {
        public frmPlayVideo()
        {
            InitializeComponent();
        }

        private void frmPlayVideo_Load(object sender, EventArgs e)
        {
            //axWindowsMediaPlayer1.URL = gt;
           
        }

        private void playDoMK_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = "G:\\Graduation_Thesis\\Project\\SeleniumWiform\\Video\\BaoMatVideo.mp4";
        }

        private void playKTAL_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = "G:\\Graduation_Thesis\\Project\\SeleniumWiform\\Video\\ApLucVideo.mp4";
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
                axWindowsMediaPlayer1.URL = file.FileName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
