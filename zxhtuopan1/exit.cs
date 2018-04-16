using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zxhtuopan1
{
    public partial class exit : Form
    {
        public exit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            Form1.wb.stop();
            Global.MainForm.Dispose();
            Global.MainForm.Close();
            System.Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            //Global.MainForm.actimer.Start();                                     //有可能引发时序严重错误，暂时关闭。20170502 nick
        }
    }
}
