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
    public partial class changeSize : Form
    {
        public changeSize()
        {
            InitializeComponent();
            //button1.Enabled = false;
        }

        private void changeSize_Load(object sender, EventArgs e)
        {          
            string strduqu2 = File.ReadAllText(@"size1.txt");
            JavaScriptSerializer js2 = new JavaScriptSerializer();
            FormSize size2 = js2.Deserialize<FormSize>(strduqu2);
            textBox1.Text = size2.size;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            //Form1.acflag = true;
            //Global.MainForm.actimer.Start();                                              //有可能引发时序严重错误，暂时关闭。20170502 nick
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSize size1 = new FormSize();
            if (textBox1.TextLength == 0 || Convert.ToInt32(textBox1.Text) < 100)
            {
                MessageBox.Show("长度不能小于100!");
            }
            else
            {
                size1.size = textBox1.Text;
                string stringup = new JavaScriptSerializer().Serialize(size1);
                //MessageBox.Show(stringup);
                FileStream fs = new FileStream(@"size1.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(stringup);
                sw.Close();
                fs.Close();
                //MessageBox.Show(stringup + "----保存成功!");
                int bianchang = Convert.ToInt32(textBox1.Text);
                Global.MainForm.bianchang = bianchang;
                Global.MainForm.Height = bianchang;
                Global.MainForm.Width = bianchang;
                Global.MainForm.label1.Location = new Point(Convert.ToInt32(Math.Round(0.15 * bianchang)), Convert.ToInt32(Math.Round(0.6 * bianchang)));
                Global.MainForm.label1.Font = new Font("宋体", Convert.ToInt32(Math.Round(0.05 * bianchang)));
                //Global.MainForm.mainMenu.Font = new Font("宋体", Convert.ToInt32(Math.Round(0.04 * bianchang)));
                //Global.MainForm.nextpro.Font = new Font("宋体", Convert.ToInt32(Math.Round(0.04 * bianchang)));
                //Global.MainForm.previouspro.Font = new Font("宋体", Convert.ToInt32(Math.Round(0.04 * bianchang)));
                //Global.MainForm.test.Font = new Font("宋体", Convert.ToInt32(Math.Round(0.04 * bianchang)));
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        /*
private void ValidateOK()
{
  button1.Enabled = textBox1.BackColor != Color.Red;
}
private void textBox1_Validated(object sender, EventArgs e)
{
  if (textBox1.TextLength == 0)
  {
      textBox1.BackColor = Color.Red;
  }
  else
  {
      textBox1.BackColor = System.Drawing.SystemColors.Window;
  }
  ValidateOK();
}
*/
    }
}
