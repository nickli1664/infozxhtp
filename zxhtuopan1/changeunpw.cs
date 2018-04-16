using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.IO;


namespace zxhtuopan1
{
    public partial class changeunpw : Form
    {
        public bool firstloginflag = false;
        public changeunpw()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            resetWholeProgram(firstloginflag);
            //Form1.acflag = true;
            //Global.MainForm.actimer.Start();                                             //有可能引发时序严重错误，暂时关闭。20170502 nick
        }
        private void button1_Click(object sender, EventArgs e)
        {
            UserPassIpPort upip1 = new UserPassIpPort();
            
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("各项均不能为空!");
            }
            else
            {
                if (firstloginflag)
                {
                    FileStream loginflagfs = new FileStream(@"firstloginflag.txt", FileMode.Create);
                    StreamWriter loginflagsw = new StreamWriter(loginflagfs);
                    loginflagsw.Write("1");
                    loginflagsw.Close();
                    loginflagfs.Close();
                }
                upip1.ipAddress = textBox3.Text;
                upip1.port = textBox4.Text;
                upip1.userName = textBox1.Text;
                upip1.passWord = textBox2.Text;
                string stringupip = new JavaScriptSerializer().Serialize(upip1);
                FileStream fs = new FileStream(@"upip1.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(stringupip);
                sw.Close();
                fs.Close();
                MessageBox.Show(stringupip + "----保存成功!");            
            }
        }
        private void changeunpw_Load(object sender, EventArgs e)
        {
            string strduqu = File.ReadAllText(@"upip1.txt");
            JavaScriptSerializer js2 = new JavaScriptSerializer();
            UserPassIpPort upip2 = js2.Deserialize<UserPassIpPort>(strduqu);
            textBox3.Text = upip2.ipAddress;
            textBox4.Text = upip2.port;
            textBox1.Text = upip2.userName;
            textBox2.Text = upip2.passWord;
        }
        private void resetWholeProgram(bool a)
        {
            if (firstloginflag)
            {
                Application.Exit();
                Application.Restart();
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 46))
            {
                e.Handled = true;
            }
        }
    }
}
