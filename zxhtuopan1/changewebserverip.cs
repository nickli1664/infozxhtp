using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace zxhtuopan1
{
    public partial class changewebserverip : Form
    {
        public changewebserverip()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void changewebserverip_Load(object sender, EventArgs e)
        {
            string strduqu = File.ReadAllText(@"webserver.txt");
            textBox3.Text = strduqu;           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            //Global.MainForm.actimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("各项均不能为空!");
            }
            else
            {
                string stringwebserverip = textBox3.Text;
                FileStream fs = new FileStream(@"webserver.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(stringwebserverip);
                sw.Close();
                fs.Close();
                Global.MainForm.webserver = stringwebserverip;
                Global.MainForm.actimer.Enabled = true;
                MessageBox.Show(stringwebserverip + "----保存成功!");
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
