using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DYSYSTEM;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using System.IO;

namespace zxhtuopan1
{
    public partial class Form2 : Form
    {
        public ContextMenuStrip mainMenu;
        private Timer actimer = new Timer();
        private bool acflag = true;
        

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

        public Form2()
        {
            InitializeComponent();
        }

        /*
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x80000;
                return cp;
            }
        }
        */

        private void Form2_Load(object sender, EventArgs e)
        {
            /*
            DYD dyd = new DYD();

            DYD.ImageSplitRects R = new DYD.ImageSplitRects();
            R.TopLeft = new Rectangle(0, 0, 240, 240);
            R.Top = new Rectangle(240, 0, 240, 240);
            R.TopRight = new Rectangle(480, 0, 240, 240);
            R.Left = new Rectangle(0, 240, 240, 240);
            R.Center = new Rectangle(240, 240, 240, 240);
            R.Right = new Rectangle(480, 240, 240, 240);
            R.BottomLeft = new Rectangle(0, 480, 240, 240);
            R.Bottom = new Rectangle(240, 480, 240, 240);
            R.BottomRight = new Rectangle(480, 480, 240, 240);
            dyd.ImgSplitRects = R;
            */

            //Bitmap png = new Bitmap(Resource1.testblack720_80);

            //Bitmap png2 = (Bitmap)this.BackgroundImage;
            //dyd.MakeSplitedImage(this.Size, png, ref png2);

            //this.BackgroundImage = png2;

            //this.BackgroundImage = png;

            /*
            dyd.SuportMove(this, true);
            dyd.SuportReSize(this, false);
            //dyd.DrawBP(this, png, 255);
            dyd.StartDraw(this);
            */

            
            this.actimer.Enabled = true;
            this.actimer.Interval = 2000;
            this.actimer.Tick += new EventHandler(Actimer_Tick);
            this.actimer.Start();
            
        }

        private void Actimer_Tick(object sender, EventArgs e)
        {
            //SetForegroundWindow(this.a);
            //if (acflag == true)
            //{
                this.TopMost = false;
                this.BringToFront();
                this.TopMost = true;
            //}
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //const int WM_NCLBUTTONDOWN = 0x00A1;
            //const int HTCAPTION = 2;


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

                //ReleaseCapture();   // 释放捕获                    
                //SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);    // 拖动窗体                 
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
                this.Location = new System.Drawing.Point(x+(x2-x1), y+(y2-y1));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.actimer.Stop();
            //this.Hide();

            this.Close();
            this.Dispose();

            //Global.MainForm.Show();
            //Global.MainForm.actimer.Start();

            GC.Collect();

            Formselect frms = new Formselect();
            frms.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.acflag = false;
            Global.MainForm.actimer.Stop();
            this.actimer.Stop();
            mainMenu = new ContextMenuStrip();
            mainMenu.BigButtons();
            mainMenu.Font = new Font("Microsoft YaHei UI", 24, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            mainMenu.Opening += new CancelEventHandler(cms_Opening);

            //menuMaxSize = Convert.ToInt32(Math.Round(Convert.ToDouble(bianchang / 3 * 2)));
            mainMenu.MaximumSize = new Size(480, 480);
            //menuLocate = Convert.ToInt32(Math.Round(Convert.ToDouble(bianchang / 5.5)));
            mainMenu.Show(this, 300, 96);
        }
        void cms_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Image img1 = Resource1.res1;

            // Clear the ContextMenuStrip control's Items collection.
            mainMenu.Items.Clear();

            /*
            System.Timers.Timer closeTimer = new System.Timers.Timer();
            closeTimer.Elapsed += new ElapsedEventHandler(closeOnTimedEvent);
            closeTimer.Interval = 10000;
            closeTimer.Enabled = true;
            closeTimer.AutoReset = false;
            */

            ToolStripMenuItem nextpro = new ToolStripMenuItem("下一模式");
            ToolStripMenuItem previouspro = new ToolStripMenuItem("上一模式");
            nextpro.Click += delegate (Object o, EventArgs e2) { Form1.wb.send("{\"body\" : \"\",\"guid\" : \"M-171\",\"type\" : \"NEXTPROGRAM\"}"); this.actimer.Start(); };
            previouspro.Click += delegate (Object o, EventArgs e3) { Form1.wb.send("{\"body\" : \"\",\"guid\" : \"M-172\",\"type\" : \"PREVIOUSPROGRAM\"}"); this.actimer.Start(); };

            if (Form1.abc.passflag == "loginwin")
            {
                mainMenu.Items.Add(nextpro);
                mainMenu.Items.Add(previouspro);

                // Populate the ContextMenuStrip control with its default items.
                mainMenu.Items.Add("-");
            }

            foreach (Cd s in BuissnesServiceImpl.cdjihe1)
            {
                //mainMenu.Items.Add(s.cdName,img1,delegate(Object o,EventArgs e1) { MenuClicked(s); });
                //mainMenu.Items.Add(s.cdName);
                ToolStripMenuItem test = new ToolStripMenuItem(s.cdName);
                test.Click += delegate (Object o, EventArgs e1) { MenuClicked(s); };
                mainMenu.Items.Add(test);
            }



            //mainMenu.Items.Add("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
            /*
            for (int i = 0; i<50; i++)
            {
                mainMenu.Items.Add(Convert.ToString(i));
            }
            */

            /*
            mainMenu.Items.Add("Apples");
            mainMenu.Items.Add("Oranges");
            mainMenu.Items.Add("Pears");
            */

            // Set Cancel to false. 
            // It is optimized to true based on empty entry.
            e.Cancel = false;
        }
        void MenuClicked(Cd s)
        {
            //MessageBox.Show(s.cdID);

            MoShiplayproreq ppreq1 = new MoShiplayproreq();
            ppreq1.id = Convert.ToInt32(s.cdID);
            Totalplayproreq ppreq2 = new Totalplayproreq();
            ppreq2.body = ppreq1;
            string s1 = new JavaScriptSerializer().Serialize(ppreq2);
            //MessageBox.Show(s1);
            Form1.wb.send(s1);
            //this.actimer.Enabled = true;
            this.actimer.Start();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1.wb.send("{\"body\" : \"\",\"guid\" : \"M-179\",\"type\" : \"PREVIOUSSCENE\"}");
            if (this.acflag == false)
            { this.actimer.Start(); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.wb.send("{\"body\" : \"\",\"guid\" : \"M-172\",\"type\" : \"PREVIOUSPROGRAM\"}");
            if (this.acflag == false)
            { this.actimer.Start(); }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1.wb.send("{\"body\" : \"\",\"guid\" : \"M-171\",\"type\" : \"NEXTPROGRAM\"}");
            if (this.acflag == false)
            { this.actimer.Start(); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1.wb.send("{\"body\" : \"\",\"guid\" : \"M-168\",\"type\" : \"NEXTSCENE\"}");
            if (this.acflag == false)
            { this.actimer.Start(); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Size screensize = SystemInformation.WorkingArea.Size;
            Rectangle screensize = new Rectangle();
            screensize = Screen.GetWorkingArea(this);
            //MessageBox.Show(Convert.ToString(screensize.Height));
            //MessageBox.Show(Convert.ToString(screensize.Width));
            this.actimer.Stop();
            this.Hide();
            Global.MainForm.Location = new Point(-Global.MainForm.bianchang/2, screensize.Height-Global.MainForm.bianchang/2);
            Global.MainForm.Show();
            Global.MainForm.actimer.Start();
        }
    }
}
