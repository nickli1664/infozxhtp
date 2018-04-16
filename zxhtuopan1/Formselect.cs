using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using System.IO;

namespace zxhtuopan1
{
    public partial class Formselect : Form
    {
        private Timer actimer = new Timer();
        //private bool acflag = true;

        public static Formxianshiqiang frmxsq;

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }

        private static int x1, x2, y1, y2;
        public Formselect()
        {
            InitializeComponent();
        }

        private void Formselect_Load(object sender, EventArgs e)
        {
            this.actimer.Enabled = true;
            this.actimer.Interval = 2000;
            this.actimer.Tick += new EventHandler(Actimer_Tick);
            this.actimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.actimer.Stop();
            //this.Hide();
            this.Close();
            this.Dispose();
            GC.Collect();

            Global.MainForm.Show();
            Global.MainForm.actimer.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.actimer.Stop();
            //this.Hide();

            this.Close();
            this.Dispose();
            GC.Collect();

            Form2 frm2 = new Form2();
            frm2.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.actimer.Stop();

            this.Close();
            this.Dispose();
            GC.Collect();

            frmxsq = new Formxianshiqiang();
            frmxsq.ShowDialog();
        }

        private void Formselect_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void Actimer_Tick(object sender, EventArgs e)
        {
            this.TopMost = false;
            this.BringToFront();
            this.TopMost = true;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {

            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)  // 按下的是鼠标左键                 
            {
                IntPtr awin = GetForegroundWindow();    //获取当前窗口句柄
                RECT rect = new RECT();
                GetWindowRect(awin, ref rect);
                int width = rect.Right - rect.Left;                        //窗口的宽度
                int height = rect.Bottom - rect.Top;                   //窗口的高度
                int x = rect.Left;
                int y = rect.Top;

                int a = Control.MousePosition.X;
                int b = Control.MousePosition.Y;

                x1 = a;
                y1 = b;

                x = this.Location.X;
                y = this.Location.Y;
              
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            IntPtr awin = GetForegroundWindow();                   //获取当前窗口句柄
            RECT rect = new RECT();
            GetWindowRect(awin, ref rect);
            int width = rect.Right - rect.Left;                    //窗口的宽度
            int height = rect.Bottom - rect.Top;                   //窗口的高度
            int x = rect.Left;
            int y = rect.Top;

            int a = Control.MousePosition.X;
            int b = Control.MousePosition.Y;

            x2 = a;
            y2 = b;

            if (e.Button == MouseButtons.Left)
            {
                //this.Location = new System.Drawing.Point(a - width / 2, b - height / 2);
                this.Location = new System.Drawing.Point(x + (x2 - x1), y + (y2 - y1));
            }
        }
    }
}
