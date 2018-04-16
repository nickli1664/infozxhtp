using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;
using System.Web.Script.Serialization;
using System.IO;
using System.Threading;
using System.Security.Cryptography;

namespace zxhtuopan1
{
    public partial class Form1 : Form
    {
        //delegate void SetTextCallback(string text);
        //private BackgroundWorker backgroundWorker1;


        public ContextMenuStrip mainMenu;
        public int bianchang,menuMaxSize,menuLocate;
        //public static int programflag = 0;
        //public static bool switchformflag = false;
        //static string url = "wss://127.0.0.1:7681";
        static string url;
        //public static bool acflag = true;

        public static BuissnesServiceImpl abc = new BuissnesServiceImpl();
        static WebSocketService wss = abc;
        //public static WebSocketBase wb = new WebSocketBase(url, wss);
        public static WebSocketBase wb;
        public System.Windows.Forms.Timer actimer = new System.Windows.Forms.Timer();

        public List<string> lastcitystring;                  //infocomm用，储存上一个城市，用于删除上一个城市的layer
        public List<string> cachestring;

        public LayerPosition beijing, shandong, shanghai, sichuan, guangdong, hunan, beijingalert, shanghaialert;                         //四个Button的所有信息，包括位置和源ID（待做）
        public string webserver;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);

        public List<CityName> cityinfonick = new List<CityName>();      //全局的城市完整信息存储对象，载入时从文件中读取保存在内存中，保存时保存到文件
        public CityInfo gbci = new CityInfo();


        public struct RECT
        {
            public int Left; //最左坐标
            public int Top; //最上坐标
            public int Right; //最右坐标
            public int Bottom; //最下坐标
        }

        private static int x1, x2, y1, y2;

        public Form1()
        {
             InitializeComponent();
             Global.MainForm = this;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //InitializeComponent();
            //backgroundWorker1 = new BackgroundWorker();
            //backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);

            /*
            string stripport = File.ReadAllText(@"ipandport.txt");
            JavaScriptSerializer jsipport = new JavaScriptSerializer();
            IPPort ipport = jsipport.Deserialize<IPPort>(stripport);
            */

            //url = "wss://" + ipport.ipaddress + ":" + ipport.port;

            //this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);      
    
            //notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
           
            退出ToolStripMenuItem.Click += 退出ToolStripMenuItem_Click;

            System.Timers.Timer aTimer = new System.Timers.Timer();
            System.Timers.Timer loginTimer = new System.Timers.Timer();

            int i;

            string strduquloginflag = File.ReadAllText(@"firstloginflag.txt");
            
            if ((strduquloginflag != "0") && (strduquloginflag != "1"))
            {
                MessageBox.Show("核心文件发生错误，请联系发布者！");
                this.Dispose();
                this.Close();
                System.Environment.Exit(0);
            }
            if (strduquloginflag == "0")
            {
                //this.Enabled = false;
                //Global.MainForm.TopMost = false;
                changeunpw form3 = new changeunpw();
                this.WindowState = FormWindowState.Minimized;
                MessageBox.Show("第一次使用软件，请输入用户名和密码");
                form3.StartPosition = FormStartPosition.CenterScreen;
                form3.Text = "请输入IP,端口,用户名和密码";
                form3.button2.Text = "继续";
                form3.firstloginflag = true;
                form3.Show();
            }
            else
            {
                string strduqu2 = File.ReadAllText(@"size1.txt");
                JavaScriptSerializer js2 = new JavaScriptSerializer();
                FormSize size2 = js2.Deserialize<FormSize>(strduqu2);
                bianchang = Convert.ToInt32(size2.size);
                //Global.MainForm.Height = bianchang;
                //Global.MainForm.Width = bianchang;
                label1.Location = new Point(Convert.ToInt32(Math.Round(0.15 * bianchang)), Convert.ToInt32(Math.Round(0.6 * bianchang)));
                label1.Font = new Font("Microsoft YaHei UI", Convert.ToInt32(Math.Round(0.05 * bianchang)));

                string strduqu = File.ReadAllText(@"upip1.txt");
                JavaScriptSerializer js3 = new JavaScriptSerializer();
                UserPassIpPort upip3 = js3.Deserialize<UserPassIpPort>(strduqu);

                url = "wss://" + upip3.ipAddress + ":" + upip3.port;

                try
                {
                    wb = new WebSocketBase(url, wss);

                    wb.start();
                }
                catch(Exception ewb)
                {
                    MessageBox.Show(ewb.Message);
                }

                for (i = 0; i < 5; i++)
                {
                    wb.send("{\"body\" : {\"userName\" : \"" + upip3.userName + "\",\"userPassword\" : \"" + upip3.passWord + "\"},\"guid\" : \"M-0\",\"type\" : \"QUERYUSERLOGIN\"}");
                    if (abc.passflag == "loginfail")
                    {
                        MessageBox.Show("登录失败，请检查用户名和密码是否正确！");
                        this.label1.Text = "登录失败，请检查";
                        wb.stop();
                        break;
                        //this.Dispose();
                        //this.Close();
                        //System.Environment.Exit(0);
                    }
                    if (abc.passflag == "loginwin")
                    {
                        //MessageBox.Show("您已成功登录！");
                        label1.Text = "您已成功登录！";
                        notifyIcon1.ShowBalloonTip(1000,"提示：","您已成功登录！",ToolTipIcon.Info);

                        wb.send(@" { ""body"" : """", ""guid"" : ""M-44"", ""type"" : ""LOADVIDEOWALLINFO"" }");

                        Thread.Sleep(200);

                        wb.send(@"{ ""body"" : """", ""guid"" : ""M-212"", ""type"" : ""STARTEXHIBITION"" }");

                        Thread.Sleep(200);

                        this.webserver = File.ReadAllText(@"webserver.txt");

                        /*
                        string strbjposition = File.ReadAllText(@"beijing.txt",Encoding.Default);
                        JavaScriptSerializer jsbjp = new JavaScriptSerializer();
                        this.beijing = jsbjp.Deserialize<LayerPosition>(strbjposition);

                        string strsdposition = File.ReadAllText(@"shandong.txt",Encoding.Default);
                        JavaScriptSerializer jssdp = new JavaScriptSerializer();
                        this.shandong = jssdp.Deserialize<LayerPosition>(strsdposition);

                        string strshposition = File.ReadAllText(@"shanghai.txt",Encoding.Default);
                        JavaScriptSerializer jsshp = new JavaScriptSerializer();
                        this.shanghai = jsshp.Deserialize<LayerPosition>(strshposition);

                        string strscposition = File.ReadAllText(@"sichuan.txt",Encoding.Default);
                        JavaScriptSerializer jsscp = new JavaScriptSerializer();
                        this.sichuan = jsscp.Deserialize<LayerPosition>(strscposition);

                        string strgdposition = File.ReadAllText(@"guangdong.txt", Encoding.Default);
                        JavaScriptSerializer jsgdp = new JavaScriptSerializer();
                        this.guangdong = jsgdp.Deserialize<LayerPosition>(strgdposition);

                        string strhnposition = File.ReadAllText(@"hunan.txt", Encoding.Default);
                        JavaScriptSerializer jshnp = new JavaScriptSerializer();
                        this.hunan = jshnp.Deserialize<LayerPosition>(strhnposition);

                        string strbjaposition = File.ReadAllText(@"beijingalert.txt", Encoding.Default);
                        JavaScriptSerializer jsbjap = new JavaScriptSerializer();
                        this.beijingalert = jsbjap.Deserialize<LayerPosition>(strbjaposition);

                        string strshaposition = File.ReadAllText(@"shanghaialert.txt", Encoding.Default);
                        JavaScriptSerializer jsshap = new JavaScriptSerializer();
                        this.shanghaialert = jsshap.Deserialize<LayerPosition>(strshaposition);
                        */

                        //MessageBox.Show(this.beijing.slaveheight);

                        string strduquci = File.ReadAllText(@"cityinfonick.txt", Encoding.UTF8);
                        JavaScriptSerializer jsci2 = new JavaScriptSerializer();
                        CityInfo ciduqu = jsci2.Deserialize<CityInfo>(strduquci);
                        this.gbci = ciduqu;
                        this.cityinfonick = ciduqu.cityinfo;

                        break;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(2000);
                    }
                    /*
                    if (i==4)
                    {
                        MessageBox.Show("登录失败，请检查");
                        wb.stop();
                        this.Dispose();
                        this.Close();
                        System.Environment.Exit(0);
                    }
                    */
                }

                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Interval = 2000;
                aTimer.Enabled = true;
                aTimer.AutoReset = false;
            }
            
            this.actimer.Enabled = true;
            this.actimer.Interval = 100;
            this.actimer.Tick += new EventHandler(Actimer_Tick);
            this.actimer.Start();


            //----以下操作转到登录成功的if后面----//
            /*
            wb.send(@" { ""body"" : """", ""guid"" : ""M-44"", ""type"" : ""LOADVIDEOWALLINFO"" }");

            Thread.Sleep(200);

            wb.send(@"{ ""body"" : """", ""guid"" : ""M-212"", ""type"" : ""STARTEXHIBITION"" }");

            Thread.Sleep(200);
            */

        }
        private void Actimer_Tick(object sender, EventArgs e)
        {
            //SetForegroundWindow(this.a);
            //if (acflag == true)
            //{

            /*
                this.TopMost = false;
                this.BringToFront();
                this.TopMost = true;
            */
            //label2.Text = DateTime.Now.ToString() + CityBack();

            CityBack();
        }

        public static void CityBack()
        {
            try
            {
                DateTime time1 = DateTime.Now;
                //Console.WriteLine(time1);
                int a = ConvertDateTimeInt(time1);
                string timestamp = a.ToString();
                //Console.WriteLine(timestamp);

                string shijianchuo1 = a.ToString();                //由当前系统的时间转为时间戳

                string token = "infocomm";

                string nojiami = shijianchuo1 + token;             //待使用SHA1加密的组合字符串

                string signature = EncryptToSHA1(nojiami).ToLower();

                //Console.WriteLine(signature);                      //加密后的字符串

                //string strURL = "http://192.168.1.9/infocomm/interface/heartbeat/?ip=192.168.1.12&timestamp=" + timestamp + "&signature=" + signature;                 //GET里边的IP似乎没有check
                string strURL = "http://" + Global.MainForm.webserver + "/infocomm/interface/heartbeat/?ip=192.168.1.12&timestamp=" + timestamp + "&signature=" + signature;
     
                string post1 = @"{ ""item"" : ""heartbeat""}";
                string post2 = @"{ ""item"" : ""heartbeat"", ""ack"" : ""ack"" }";

                string city = PostData(strURL, post1);

                if (city != "0")
                {
                    Global.MainForm.label2.Text = city;
                }
                //return city;

                if (city == "clear")
                {
                    PostData(strURL, post2);
                    DeleteLastLayer();                 
                }
                else
                {
                    foreach (CityName cnmain in Global.MainForm.cityinfonick)
                    {
                        if (city == cnmain.cityname)
                        {
                            PostData(strURL, post2);
                            Global.MainForm.cachestring = Global.MainForm.City_Click(cnmain); ;              //播放新的layer上墙，并将整个JSON字符串返回给cachestring集合
                            DeleteLastLayer();                                                               //删除旧的layer
                            Global.MainForm.lastcitystring = Global.MainForm.cachestring;                    //将cachestring集合完整赋值给lastcitystring，删除时使用lastcitystring，替换Add为Delete等操作即可
                            break;
                        }
                    }
                }

                /*
                switch (city)
                {
                    case "beijing":
                        //Global.MainForm.notifyIcon1.ShowBalloonTip(1000, "提示：", "进入北京切换过程", ToolTipIcon.Info);
                        PostData(strURL, post2);                                                         //接收成功，发送ack使服务端停止继续发送请求
                        Global.MainForm.cachestring = Global.MainForm.button3_Click();                   //播放新的layer上墙，并将整个JSON字符串返回给cachestring集合
                        DeleteLastLayer();                                                               //删除旧的layer
                        Global.MainForm.lastcitystring = Global.MainForm.cachestring;                    //将cachestring集合完整赋值给lastcitystring，删除时使用lastcitystring，替换Add为Delete等操作即可
                        break;
                    case "shanghai":
                        //Global.MainForm.notifyIcon1.ShowBalloonTip(1000, "提示：", "进入上海切换过程", ToolTipIcon.Info);
                        PostData(strURL, post2);
                        Global.MainForm.cachestring = Global.MainForm.button4_Click();
                        DeleteLastLayer();
                        Global.MainForm.lastcitystring = Global.MainForm.cachestring;
                        break;
                    case "shandong":
                        //Global.MainForm.notifyIcon1.ShowBalloonTip(1000, "提示：", "进入山东切换过程", ToolTipIcon.Info);
                        PostData(strURL, post2);
                        Global.MainForm.cachestring = Global.MainForm.button5_Click();
                        DeleteLastLayer();
                        Global.MainForm.lastcitystring = Global.MainForm.cachestring;
                        break;
                    case "sichuan":
                        //Global.MainForm.notifyIcon1.ShowBalloonTip(1000, "提示：", "进入四川切换过程", ToolTipIcon.Info);
                        PostData(strURL, post2);
                        Global.MainForm.cachestring = Global.MainForm.button6_Click();
                        DeleteLastLayer();
                        Global.MainForm.lastcitystring = Global.MainForm.cachestring;
                        break;
                    case "guangdong":
                        PostData(strURL, post2);
                        Global.MainForm.cachestring = Global.MainForm.button7_Click();
                        DeleteLastLayer();
                        Global.MainForm.lastcitystring = Global.MainForm.cachestring;
                        break;
                    case "hunan":
                        PostData(strURL, post2);
                        Global.MainForm.cachestring = Global.MainForm.button8_Click();
                        DeleteLastLayer();
                        Global.MainForm.lastcitystring = Global.MainForm.cachestring;
                        break;
                    case "clear":                                                                            //删除功能
                        PostData(strURL, post2);
                            //Global.MainForm.cachestring = Global.MainForm.button6_Click();
                        DeleteLastLayer();
                            //Global.MainForm.lastcitystring = Global.MainForm.cachestring;
                        break;
                    case "beijingalert":
                        PostData(strURL, post2);
                        Global.MainForm.cachestring = Global.MainForm.button9_Click();
                        DeleteLastLayer();
                        //Global.MainForm.notifyIcon1.ShowBalloonTip(1000, "提示：", "北京alert", ToolTipIcon.Info);
                        Global.MainForm.lastcitystring = Global.MainForm.cachestring;
                        break;
                    case "shanghaialert":
                        PostData(strURL, post2);
                        Global.MainForm.cachestring = Global.MainForm.button10_Click();
                        DeleteLastLayer();
                        //Global.MainForm.notifyIcon1.ShowBalloonTip(1000, "提示：", "上海alert", ToolTipIcon.Info);
                        Global.MainForm.lastcitystring = Global.MainForm.cachestring;
                        break;

                        case "shanghai":
                            PostData(strURL, post2);
                            DeleteLastLayer();
                            Global.MainForm.lastcitystring = Global.MainForm.button4_Click();                  
                            break;

                }
                */
            }
            catch (Exception ecb)
            {
                MessageBox.Show("citybackerror" + ecb.Message);
            }
        }

        public static void DeleteLastLayer()
        {
            if (Global.MainForm.lastcitystring != null)
            {
                foreach (string d1 in Global.MainForm.lastcitystring)
                {
                    string deletetest = d1.Replace("Add", "Delete");
                    Thread.Sleep(500);
                    Form1.wb.send(deletetest);
                }
            }
        }

        public static int ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        public static string EncryptToSHA1(string str)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] str1 = Encoding.UTF8.GetBytes(str);
            byte[] str2 = sha1.ComputeHash(str1);
            sha1.Clear();
            (sha1 as IDisposable).Dispose();
            //return Convert.ToBase64String(str2);
            return BitConverter.ToString(str2).Replace("-", "");
        }

        public static string PostData(string url, string parm)
        {
            try
            { 
                byte[] data = new ASCIIEncoding().GetBytes(parm);
            // 发送请求    
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
            //try
            //{ 
                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                // 获得回复    
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string result = reader.ReadToEnd();
                reader.Close();
                return result;
            }
            catch (Exception e)
            {
                Global.MainForm.actimer.Enabled = false;
                MessageBox.Show("PostData发生错误，有可能是网络连接断开。请检查并重启程序。" + "\n\n" + "在本软件中，出现这种错误很可能是因为您填写了错误的IP。请更正并重启程序。" + "\n\n" + e.Message);                
                return null;
            }          
        }


        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            wb.send("{\"body\" : \"\",\"guid\" : \"M-18\",\"type\" : \"SEARCHPROGRAMBASICINFO\"}");
            //MessageBox.Show("sendwin");
            /*
            while (true)
            {
                if (Form1.programflag == 1)
                {
                    //SetText("载入模式成功！");                    
                    //backgroundWorker1.RunWorkerAsync();
                    break;
                }
            }
            */
        }
        /*
        private void backgroundWorker1_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e)
        {
            label1.Text ="载入模式成功！";
        }

        private void SetText(string text)
        {
            if (label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                label1.Invoke(d, new object[] { text });
            }
            else
            {
                label1.Text = text;
            }
        }
        */
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*      这个项目不需要这个功能  --20170503 nick
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;

                HideMainForm();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                ShowMainForm();
            }
            */
        }
        
        private void HideMainForm()
        {
            this.Hide();
        }

        private void ShowMainForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
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

                //ReleaseCapture();   // 释放捕获                    
                //SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);    // 拖动窗体                 
            }
        }

        private void label1_mousedown(object sender, MouseEventArgs e)
        {
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

                //ReleaseCapture();   // 释放捕获                    
                //SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);    // 拖动窗体                 
            }

        }

        private void label1_mouseup(object sender, MouseEventArgs e)
        {
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
                if (System.Math.Abs(x1 - x2) < width / 2 && System.Math.Abs(y1 - y2) < height / 2)
                {
                    /*
                    mainMenu = new ContextMenuStrip();
                    mainMenu.BigButtons();
                    mainMenu.Font = new Font("Microsoft YaHei UI", bianchang / 30, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    mainMenu.Opening += new CancelEventHandler(cms_Opening);

                    menuMaxSize = Convert.ToInt32(Math.Round(Convert.ToDouble(bianchang / 3 * 2)));
                    mainMenu.MaximumSize = new Size(menuMaxSize, menuMaxSize);
                    menuLocate = Convert.ToInt32(Math.Round(Convert.ToDouble(bianchang / 5.5)));
                    mainMenu.Show(this, menuLocate, menuLocate);
                    */
                    
                    this.Hide();
                    //Form2 frm2 = new Form2();

                    //frm2.Height = bianchang * 2;
                    //frm2.Width = bianchang * 2;

                    //frm2.ShowDialog();

                    Formselect frms = new Formselect();
                    frms.ShowDialog();
                }
                else
                {
                    this.Location = new System.Drawing.Point(a - width / 2, b - height / 2);

                }
            }

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //acflag = false;
            //this.actimer.Stop();
            this.TopMost = false;
            /*
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                wb.stop();
                this.Dispose();
                this.Close();
                System.Environment.Exit(0);
            }
            else
            {
                //acflag = true;
                this.actimer.Start();
            }
            */
            exit exit = new exit();
            exit.StartPosition = FormStartPosition.CenterScreen;
            exit.Show();
        }
        private void form1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("123!");

            /*
            IntPtr awin = GetForegroundWindow();                   //获取当前窗口句柄
            RECT rect = new RECT();
            GetWindowRect(awin, ref rect);
            int width = rect.Right - rect.Left;                    //窗口的宽度
            int height = rect.Bottom - rect.Top;                   //窗口的高度
            int x = rect.Left;
            int y = rect.Top;
            */

            //MessageBox.Show(String.Format("x={0},y={1}", x, y));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            //this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
            Thread.Sleep(1000);

            LayerItemelement onelayeritem3 = new LayerItemelement();
            onelayeritem3.ID = BuissnesServiceImpl.idlist[1].id;
            onelayeritem3.description = "";
            onelayeritem3.majorID = 345;
            onelayeritem3.minorID = 346;
            onelayeritem3.name = "VideoEncoderConfig_001";
            onelayeritem3.playOrder = 0;
            onelayeritem3.playTime = 30;
            onelayeritem3.refreshTime = 1800;
            onelayeritem3.type = 42;

            LayerActionreqbody onelayerbody3 = new LayerActionreqbody();
            List<LayerItemelement> onelayeritem = new List<LayerItemelement>();
            onelayeritem.Add(onelayeritem3);

            onelayerbody3.alpha = 1.0;
            onelayerbody3.highlight = false;
            onelayerbody3.layerID = BuissnesServiceImpl.idlist[0].id;
            onelayerbody3.layerItem = onelayeritem;

            string piecexmlhalf = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.65\" slavetop=\"0.08\" slavewidth=\"0.3\" slaveheight=\"0.3\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            string slave0ID = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
            onelayerbody3.pieceXml = piecexmlhalf.Replace("abc", slave0ID);


            onelayerbody3.type = "Add";
            onelayerbody3.zOrder = 1501;

            LayerActionreq onelayerreq3 = new LayerActionreq();
            onelayerreq3.body = onelayerbody3;

            string onelayerreqstr = new JavaScriptSerializer().Serialize(onelayerreq3);
            string newstr = onelayerreqstr.Replace("\"alpha\":1,", "\"alpha\":1.0,");
            //MessageBox.Show(newstr);
            Form1.wb.send(newstr);

        }

        private void button2_Click(object sender, EventArgs e)
        {
                        
        }

        private List<string> button3_Click()
        {
            List<string> beijingstring = new List<string>();

            Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
            Thread.Sleep(1000);

            LayerItemelement onelayeritem3 = new LayerItemelement();
            onelayeritem3.ID = BuissnesServiceImpl.idlist[1].id;
            onelayeritem3.description = "";
            /*
            onelayeritem3.majorID = -1;
            onelayeritem3.minorID = 207;
            onelayeritem3.name = "北京风景.jpg";
            */

            onelayeritem3.majorID = Convert.ToInt64(beijing.majorID);
            onelayeritem3.minorID = Convert.ToInt64(beijing.minorID);
            onelayeritem3.name = beijing.name;
            //MessageBox.Show(beijing.name);


            onelayeritem3.playOrder = 0;
            onelayeritem3.playTime = 30;
            onelayeritem3.refreshTime = 1800;
            //onelayeritem3.type = 43;
            onelayeritem3.type = Convert.ToInt32(beijing.type);

            //MessageBox.Show(onelayeritem3.majorID + "----"+ onelayeritem3.minorID + "----" + onelayeritem3.type);

            LayerActionreqbody onelayerbody3 = new LayerActionreqbody();
            List<LayerItemelement> onelayeriteml = new List<LayerItemelement>();
            onelayeriteml.Add(onelayeritem3);

            onelayerbody3.alpha = 1.0;
            onelayerbody3.highlight = false;
            onelayerbody3.layerID = BuissnesServiceImpl.idlist[0].id;
            onelayerbody3.layerItem = onelayeriteml;

            //string piecexmlhalf = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.6667\" slavetop=\"0\" slavewidth=\"0.3333\" slaveheight=\"0.5\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下位置为叠加版本
            //string piecexmlhalf = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.2998\" slavetop=\"0.1\" slavewidth=\"0.1667\" slaveheight=\"0.25\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下位置为可通过txt编辑的叠加版本
            string piecexmlhalf = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"" + beijing.slaveleft + "\" slavetop=\"" + beijing.slavetop + "\" slavewidth=\"" + beijing.slavewidth + "\" slaveheight=\"" + beijing.slaveheight + "\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";

            string slave0ID = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
            onelayerbody3.pieceXml = piecexmlhalf.Replace("abc", slave0ID);


            onelayerbody3.type = "Add";
            onelayerbody3.zOrder = 1502;

            LayerActionreq onelayerreq3 = new LayerActionreq();
            onelayerreq3.body = onelayerbody3;

            string onelayerreqstr = new JavaScriptSerializer().Serialize(onelayerreq3);
            string newstr = onelayerreqstr.Replace("\"alpha\":1,", "\"alpha\":1.0,");
            //MessageBox.Show(newstr);
            Form1.wb.send(newstr);

            beijingstring.Add(newstr);
            return beijingstring;
            
            //textBox2.Visible = true;
        }

        private List<string> button4_Click()
        {
            List<string> shanghaistring = new List<string>();
            /*
            Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
            Thread.Sleep(1000);

            LayerItemelement onelayeritem3 = new LayerItemelement();
            onelayeritem3.ID = BuissnesServiceImpl.idlist[1].id;
            onelayeritem3.description = "";
            onelayeritem3.majorID = 356;
            onelayeritem3.minorID = -1;
            onelayeritem3.name = "hks";
            onelayeritem3.playOrder = 0;
            onelayeritem3.playTime = 30;
            onelayeritem3.refreshTime = 1800;
            onelayeritem3.type = 5;

            LayerActionreqbody onelayerbody3 = new LayerActionreqbody();
            List<LayerItemelement> onelayeritem = new List<LayerItemelement>();
            onelayeritem.Add(onelayeritem3);

            onelayerbody3.alpha = 1.0;
            onelayerbody3.highlight = false;
            onelayerbody3.layerID = BuissnesServiceImpl.idlist[0].id;
            onelayerbody3.layerItem = onelayeritem;

            string piecexmlhalf = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.15\" slavetop=\"0.06\" slavewidth=\"0.26\" slaveheight=\"0.32\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            string slave0ID = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
            onelayerbody3.pieceXml = piecexmlhalf.Replace("abc", slave0ID);


            onelayerbody3.type = "Add";
            onelayerbody3.zOrder = 1503;

            LayerActionreq onelayerreq3 = new LayerActionreq();
            onelayerreq3.body = onelayerbody3;

            string onelayerreqstr = new JavaScriptSerializer().Serialize(onelayerreq3);
            string newstr = onelayerreqstr.Replace("\"alpha\":1,", "\"alpha\":1.0,");
            //MessageBox.Show(newstr);
            Form1.wb.send(newstr);
            shanghaistring.Add(newstr);

            Thread.Sleep(1000);
            */
            Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
            Thread.Sleep(1000);

            LayerItemelement onelayeritem4 = new LayerItemelement();
            onelayeritem4.ID = BuissnesServiceImpl.idlist[1].id;
            onelayeritem4.description = "";
            /*
            onelayeritem4.majorID = 137;
            onelayeritem4.minorID = 138;
            onelayeritem4.name = "VideoEncoderConfig_000";
            */

            onelayeritem4.majorID = Convert.ToInt64(shanghai.majorID);
            onelayeritem4.minorID = Convert.ToInt64(shanghai.minorID);
            onelayeritem4.name = shanghai.name;

            onelayeritem4.playOrder = 0;
            onelayeritem4.playTime = 30;
            onelayeritem4.refreshTime = 1800;
            //onelayeritem4.type = 4;
            onelayeritem4.type = Convert.ToInt32(shanghai.type);

            LayerActionreqbody onelayerbody4 = new LayerActionreqbody();
            List<LayerItemelement> onelayeriteml2 = new List<LayerItemelement>();
            onelayeriteml2.Add(onelayeritem4);

            onelayerbody4.alpha = 1.0;
            onelayerbody4.highlight = false;
            onelayerbody4.layerID = BuissnesServiceImpl.idlist[0].id;
            onelayerbody4.layerItem = onelayeriteml2;

            //string piecexmlhalf2 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.15\" slavetop=\"0.42\" slavewidth=\"0.26\" slaveheight=\"0.32\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //string piecexmlhalf2 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.6667\" slavetop=\"0\" slavewidth=\"0.3333\" slaveheight=\"0.5\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下位置为叠加版本
            //string piecexmlhalf2 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.2949\" slavetop=\"0.5819\" slavewidth=\"0.1667\" slaveheight=\"0.25\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下位置为可通过txt编辑的叠加版本
            string piecexmlhalf2 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"" + shanghai.slaveleft + "\" slavetop=\"" + shanghai.slavetop + "\" slavewidth=\"" + shanghai.slavewidth + "\" slaveheight=\"" + shanghai.slaveheight + "\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";

            string slave0ID2 = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
            onelayerbody4.pieceXml = piecexmlhalf2.Replace("abc", slave0ID2);


            onelayerbody4.type = "Add";
            onelayerbody4.zOrder = 1504;

            LayerActionreq onelayerreq4 = new LayerActionreq();
            onelayerreq4.body = onelayerbody4;

            string onelayerreqstr2 = new JavaScriptSerializer().Serialize(onelayerreq4);
            string newstr2 = onelayerreqstr2.Replace("\"alpha\":1,", "\"alpha\":1.0,");
            //MessageBox.Show(newstr);
            Form1.wb.send(newstr2);
            shanghaistring.Add(newstr2);

            return shanghaistring;
            //textBox3.Visible = true;
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            HideCaret(((TextBox)sender).Handle);
        }

        private void 设置城市信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (abc.passflag == "loginwin")
            {
                //acflag = false;
                this.actimer.Stop();
                this.TopMost = false;
                //MessageBox.Show("此处用来修改用户名和密码");
                FormEditCity formec = new FormEditCity();
                formec.StartPosition = FormStartPosition.CenterScreen;
                formec.Show();
            }
            else
            {
                MessageBox.Show("未成功登陆用户。请检查设置并重开软件。");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private List<string> button5_Click()
        {
            List<string> shandongstring = new List<string>();

            Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
            Thread.Sleep(1000);

            LayerItemelement onelayeritem5 = new LayerItemelement();
            onelayeritem5.ID = BuissnesServiceImpl.idlist[1].id;
            onelayeritem5.description = "";
            /*
            onelayeritem5.majorID = 156;
            onelayeritem5.minorID = 157;
            onelayeritem5.name = "Main stream encoder";
            */

            onelayeritem5.majorID = Convert.ToInt64(shandong.majorID);
            onelayeritem5.minorID = Convert.ToInt64(shandong.minorID);
            onelayeritem5.name = shandong.name;

            onelayeritem5.playOrder = 0;
            onelayeritem5.playTime = 30;
            onelayeritem5.refreshTime = 1800;
            //onelayeritem5.type = 4;
            onelayeritem5.type = Convert.ToInt32(shandong.type);

            LayerActionreqbody onelayerbody5 = new LayerActionreqbody();
            List<LayerItemelement> onelayeriteml3 = new List<LayerItemelement>();
            onelayeriteml3.Add(onelayeritem5);

            onelayerbody5.alpha = 1.0;
            onelayerbody5.highlight = false;
            onelayerbody5.layerID = BuissnesServiceImpl.idlist[0].id;
            onelayerbody5.layerItem = onelayeriteml3;

            //string piecexmlhalf2 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.15\" slavetop=\"0.42\" slavewidth=\"0.26\" slaveheight=\"0.32\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下为叠加版本的位置，固定版本的参考上一个函数
            //string piecexmlhalf3 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.1511\" slavetop=\"0.2222\" slavewidth=\"0.1667\" slaveheight=\"0.25\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下位置为可通过txt编辑的叠加版本
            string piecexmlhalf3 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"" + shandong.slaveleft + "\" slavetop=\"" + shandong.slavetop + "\" slavewidth=\"" + shandong.slavewidth + "\" slaveheight=\"" + shandong.slaveheight + "\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            string slave0ID3 = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
            onelayerbody5.pieceXml = piecexmlhalf3.Replace("abc", slave0ID3);


            onelayerbody5.type = "Add";
            onelayerbody5.zOrder = 1505;

            LayerActionreq onelayerreq5 = new LayerActionreq();
            onelayerreq5.body = onelayerbody5;

            string onelayerreqstr3 = new JavaScriptSerializer().Serialize(onelayerreq5);
            string newstr3 = onelayerreqstr3.Replace("\"alpha\":1,", "\"alpha\":1.0,");
            //MessageBox.Show(newstr);
            Form1.wb.send(newstr3);
            shandongstring.Add(newstr3);

            return shandongstring;
        }

        private void 设置WebServerIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //acflag = false;
            //this.actimer.Stop();
            this.TopMost = false;
            //MessageBox.Show("此处用来修改WebServerIP");
            changewebserverip formcw = new changewebserverip();
            formcw.StartPosition = FormStartPosition.CenterScreen;
            formcw.Show();
        }

        private void test123ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string output = new JavaScriptSerializer().Serialize(Global.MainForm.cityinfonick);
            MessageBox.Show(output);
        }

        private List<string> button6_Click()
        {
            List<string> sichuanstring = new List<string>();

            Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
            Thread.Sleep(1000);

            LayerItemelement onelayeritem6 = new LayerItemelement();
            onelayeritem6.ID = BuissnesServiceImpl.idlist[1].id;
            onelayeritem6.description = "";
            /*
            onelayeritem6.majorID = -1;
            onelayeritem6.minorID = 205;
            onelayeritem6.name = "四川风景.jpg";
            */

            onelayeritem6.majorID = Convert.ToInt64(sichuan.majorID);
            onelayeritem6.minorID = Convert.ToInt64(sichuan.minorID);
            onelayeritem6.name = sichuan.name;

            onelayeritem6.playOrder = 0;
            onelayeritem6.playTime = 30;
            onelayeritem6.refreshTime = 1800;
            //onelayeritem6.type = 43;
            onelayeritem6.type = Convert.ToInt32(sichuan.type);

            LayerActionreqbody onelayerbody6 = new LayerActionreqbody();
            List<LayerItemelement> onelayeriteml4 = new List<LayerItemelement>();
            onelayeriteml4.Add(onelayeritem6);

            onelayerbody6.alpha = 1.0;
            onelayerbody6.highlight = false;
            onelayerbody6.layerID = BuissnesServiceImpl.idlist[0].id;
            onelayerbody6.layerItem = onelayeriteml4;

            //string piecexmlhalf2 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.15\" slavetop=\"0.42\" slavewidth=\"0.26\" slaveheight=\"0.32\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下为叠加版本的位置，固定版本的参考上一个函数
            //string piecexmlhalf4 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.0382\" slavetop=\"0.5556\" slavewidth=\"0.1667\" slaveheight=\"0.25\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下位置为可通过txt编辑的叠加版本
            string piecexmlhalf4 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"" + sichuan.slaveleft + "\" slavetop=\"" + sichuan.slavetop + "\" slavewidth=\"" + sichuan.slavewidth + "\" slaveheight=\"" + sichuan.slaveheight + "\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";

            string slave0ID4 = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
            onelayerbody6.pieceXml = piecexmlhalf4.Replace("abc", slave0ID4);


            onelayerbody6.type = "Add";
            onelayerbody6.zOrder = 1506;

            LayerActionreq onelayerreq6 = new LayerActionreq();
            onelayerreq6.body = onelayerbody6;

            string onelayerreqstr4 = new JavaScriptSerializer().Serialize(onelayerreq6);
            string newstr4 = onelayerreqstr4.Replace("\"alpha\":1,", "\"alpha\":1.0,");
            //MessageBox.Show(newstr);
            Form1.wb.send(newstr4);
            sichuanstring.Add(newstr4);

            return sichuanstring;
        }

        private List<string> button7_Click()
        {
            List<string> guangdongstring = new List<string>();

            Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
            Thread.Sleep(1000);

            LayerItemelement onelayeritem7 = new LayerItemelement();
            onelayeritem7.ID = BuissnesServiceImpl.idlist[1].id;
            onelayeritem7.description = "";
            

            onelayeritem7.majorID = Convert.ToInt64(guangdong.majorID);
            onelayeritem7.minorID = Convert.ToInt64(guangdong.minorID);
            onelayeritem7.name = guangdong.name;

            onelayeritem7.playOrder = 0;
            onelayeritem7.playTime = 30;
            onelayeritem7.refreshTime = 1800;
            //onelayeritem6.type = 43;
            onelayeritem7.type = Convert.ToInt32(guangdong.type);

            LayerActionreqbody onelayerbody7 = new LayerActionreqbody();
            List<LayerItemelement> onelayeriteml5 = new List<LayerItemelement>();
            onelayeriteml5.Add(onelayeritem7);

            onelayerbody7.alpha = 1.0;
            onelayerbody7.highlight = false;
            onelayerbody7.layerID = BuissnesServiceImpl.idlist[0].id;
            onelayerbody7.layerItem = onelayeriteml5;

            //string piecexmlhalf2 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.15\" slavetop=\"0.42\" slavewidth=\"0.26\" slaveheight=\"0.32\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下为叠加版本的位置，固定版本的参考上一个函数
            //string piecexmlhalf4 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.0382\" slavetop=\"0.5556\" slavewidth=\"0.1667\" slaveheight=\"0.25\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下位置为可通过txt编辑的叠加版本
            string piecexmlhalf5 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"" + guangdong.slaveleft + "\" slavetop=\"" + guangdong.slavetop + "\" slavewidth=\"" + guangdong.slavewidth + "\" slaveheight=\"" + guangdong.slaveheight + "\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";

            string slave0ID5 = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
            onelayerbody7.pieceXml = piecexmlhalf5.Replace("abc", slave0ID5);


            onelayerbody7.type = "Add";
            onelayerbody7.zOrder = 1506;

            LayerActionreq onelayerreq7 = new LayerActionreq();
            onelayerreq7.body = onelayerbody7;

            string onelayerreqstr5 = new JavaScriptSerializer().Serialize(onelayerreq7);
            string newstr5 = onelayerreqstr5.Replace("\"alpha\":1,", "\"alpha\":1.0,");
            //MessageBox.Show(newstr);
            Form1.wb.send(newstr5);
            guangdongstring.Add(newstr5);

            return guangdongstring;
        }

        private List<string> button8_Click()
        {
            List<string> hunanstring = new List<string>();

            Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
            Thread.Sleep(1000);

            LayerItemelement onelayeritem8 = new LayerItemelement();
            onelayeritem8.ID = BuissnesServiceImpl.idlist[1].id;
            onelayeritem8.description = "";


            onelayeritem8.majorID = Convert.ToInt64(hunan.majorID);
            onelayeritem8.minorID = Convert.ToInt64(hunan.minorID);
            onelayeritem8.name = hunan.name;

            onelayeritem8.playOrder = 0;
            onelayeritem8.playTime = 30;
            onelayeritem8.refreshTime = 1800;
            //onelayeritem6.type = 43;
            onelayeritem8.type = Convert.ToInt32(hunan.type);

            LayerActionreqbody onelayerbody8 = new LayerActionreqbody();
            List<LayerItemelement> onelayeriteml6 = new List<LayerItemelement>();
            onelayeriteml6.Add(onelayeritem8);

            onelayerbody8.alpha = 1.0;
            onelayerbody8.highlight = false;
            onelayerbody8.layerID = BuissnesServiceImpl.idlist[0].id;
            onelayerbody8.layerItem = onelayeriteml6;

            //string piecexmlhalf2 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.15\" slavetop=\"0.42\" slavewidth=\"0.26\" slaveheight=\"0.32\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下为叠加版本的位置，固定版本的参考上一个函数
            //string piecexmlhalf4 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.0382\" slavetop=\"0.5556\" slavewidth=\"0.1667\" slaveheight=\"0.25\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下位置为可通过txt编辑的叠加版本
            string piecexmlhalf6 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"" + hunan.slaveleft + "\" slavetop=\"" + hunan.slavetop + "\" slavewidth=\"" + hunan.slavewidth + "\" slaveheight=\"" + hunan.slaveheight + "\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";

            string slave0ID6 = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
            onelayerbody8.pieceXml = piecexmlhalf6.Replace("abc", slave0ID6);


            onelayerbody8.type = "Add";
            onelayerbody8.zOrder = 1506;

            LayerActionreq onelayerreq8 = new LayerActionreq();
            onelayerreq8.body = onelayerbody8;

            string onelayerreqstr6 = new JavaScriptSerializer().Serialize(onelayerreq8);
            string newstr6 = onelayerreqstr6.Replace("\"alpha\":1,", "\"alpha\":1.0,");
            //MessageBox.Show(newstr);
            Form1.wb.send(newstr6);
            hunanstring.Add(newstr6);

            return hunanstring;
        }

        private List<string> button9_Click()
        {
            List<string> beijingalertstring = new List<string>();

            Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
            Thread.Sleep(1000);

            LayerItemelement onelayeritem9 = new LayerItemelement();
            onelayeritem9.ID = BuissnesServiceImpl.idlist[1].id;
            onelayeritem9.description = "";


            onelayeritem9.majorID = Convert.ToInt64(beijingalert.majorID);
            onelayeritem9.minorID = Convert.ToInt64(beijingalert.minorID);
            onelayeritem9.name = beijingalert.name;

            onelayeritem9.playOrder = 0;
            onelayeritem9.playTime = 30;
            onelayeritem9.refreshTime = 1800;
            //onelayeritem6.type = 43;
            onelayeritem9.type = Convert.ToInt32(beijingalert.type);

            LayerActionreqbody onelayerbody9 = new LayerActionreqbody();
            List<LayerItemelement> onelayeriteml7 = new List<LayerItemelement>();
            onelayeriteml7.Add(onelayeritem9);

            onelayerbody9.alpha = 1.0;
            onelayerbody9.highlight = false;
            onelayerbody9.layerID = BuissnesServiceImpl.idlist[0].id;
            onelayerbody9.layerItem = onelayeriteml7;

            //string piecexmlhalf2 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.15\" slavetop=\"0.42\" slavewidth=\"0.26\" slaveheight=\"0.32\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下为叠加版本的位置，固定版本的参考上一个函数
            //string piecexmlhalf4 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.0382\" slavetop=\"0.5556\" slavewidth=\"0.1667\" slaveheight=\"0.25\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下位置为可通过txt编辑的叠加版本
            string piecexmlhalf7 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"" + beijingalert.slaveleft + "\" slavetop=\"" + beijingalert.slavetop + "\" slavewidth=\"" + beijingalert.slavewidth + "\" slaveheight=\"" + beijingalert.slaveheight + "\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";

            string slave0ID7 = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
            onelayerbody9.pieceXml = piecexmlhalf7.Replace("abc", slave0ID7);


            onelayerbody9.type = "Add";
            onelayerbody9.zOrder = 1506;

            LayerActionreq onelayerreq9 = new LayerActionreq();
            onelayerreq9.body = onelayerbody9;

            string onelayerreqstr7 = new JavaScriptSerializer().Serialize(onelayerreq9);
            string newstr7 = onelayerreqstr7.Replace("\"alpha\":1,", "\"alpha\":1.0,");
            //MessageBox.Show(newstr);
            Form1.wb.send(newstr7);
            beijingalertstring.Add(newstr7);

            return beijingalertstring;
        }

        private List<string> button10_Click()
        {
            List<string> shanghaialertstring = new List<string>();

            Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
            Thread.Sleep(1000);

            LayerItemelement onelayeritem10 = new LayerItemelement();
            onelayeritem10.ID = BuissnesServiceImpl.idlist[1].id;
            onelayeritem10.description = "";


            onelayeritem10.majorID = Convert.ToInt64(shanghaialert.majorID);
            onelayeritem10.minorID = Convert.ToInt64(shanghaialert.minorID);
            onelayeritem10.name = shanghaialert.name;

            onelayeritem10.playOrder = 0;
            onelayeritem10.playTime = 30;
            onelayeritem10.refreshTime = 1800;
            //onelayeritem6.type = 43;
            onelayeritem10.type = Convert.ToInt32(shanghaialert.type);

            LayerActionreqbody onelayerbody10 = new LayerActionreqbody();
            List<LayerItemelement> onelayeriteml8 = new List<LayerItemelement>();
            onelayeriteml8.Add(onelayeritem10);

            onelayerbody10.alpha = 1.0;
            onelayerbody10.highlight = false;
            onelayerbody10.layerID = BuissnesServiceImpl.idlist[0].id;
            onelayerbody10.layerItem = onelayeriteml8;

            //string piecexmlhalf2 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.15\" slavetop=\"0.42\" slavewidth=\"0.26\" slaveheight=\"0.32\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下为叠加版本的位置，固定版本的参考上一个函数
            //string piecexmlhalf4 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0.0382\" slavetop=\"0.5556\" slavewidth=\"0.1667\" slaveheight=\"0.25\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
            //以下位置为可通过txt编辑的叠加版本
            string piecexmlhalf8 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"" + shanghaialert.slaveleft + "\" slavetop=\"" + shanghaialert.slavetop + "\" slavewidth=\"" + shanghaialert.slavewidth + "\" slaveheight=\"" + shanghaialert.slaveheight + "\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";

            string slave0ID8 = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
            onelayerbody10.pieceXml = piecexmlhalf8.Replace("abc", slave0ID8);


            onelayerbody10.type = "Add";
            onelayerbody10.zOrder = 1506;

            LayerActionreq onelayerreq10 = new LayerActionreq();
            onelayerreq10.body = onelayerbody10;

            string onelayerreqstr8 = new JavaScriptSerializer().Serialize(onelayerreq10);
            string newstr8 = onelayerreqstr8.Replace("\"alpha\":1,", "\"alpha\":1.0,");
            //MessageBox.Show(newstr);
            Form1.wb.send(newstr8);
            shanghaialertstring.Add(newstr8);

            return shanghaialertstring;
        }

        private List<string> City_Click(CityName cn)
        {
            List<string> citystring = new List<string>();

            Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
            Thread.Sleep(1000);

            LayerItemelement onelayeritem10 = new LayerItemelement();
            onelayeritem10.ID = BuissnesServiceImpl.idlist[1].id;
            onelayeritem10.description = "";

            onelayeritem10.majorID = Convert.ToInt64(cn.majorID);
            onelayeritem10.minorID = Convert.ToInt64(cn.minorID);
            onelayeritem10.name = cn.name;

            onelayeritem10.playOrder = 0;
            onelayeritem10.playTime = 30;
            onelayeritem10.refreshTime = 1800;
            //onelayeritem6.type = 43;
            onelayeritem10.type = Convert.ToInt32(cn.type);

            LayerActionreqbody onelayerbody10 = new LayerActionreqbody();
            List<LayerItemelement> onelayeriteml8 = new List<LayerItemelement>();
            onelayeriteml8.Add(onelayeritem10);

            onelayerbody10.alpha = 1.0;
            onelayerbody10.highlight = false;
            onelayerbody10.layerID = BuissnesServiceImpl.idlist[0].id;
            onelayerbody10.layerItem = onelayeriteml8;

            string piecexmlhalf8 = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"" + cn.slaveleft + "\" slavetop=\"" + cn.slavetop + "\" slavewidth=\"" + cn.slavewidth + "\" slaveheight=\"" + cn.slaveheight + "\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";

            string slave0ID8 = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
            onelayerbody10.pieceXml = piecexmlhalf8.Replace("abc", slave0ID8);


            onelayerbody10.type = "Add";
            onelayerbody10.zOrder = 1506;

            LayerActionreq onelayerreq10 = new LayerActionreq();
            onelayerreq10.body = onelayerbody10;

            string onelayerreqstr8 = new JavaScriptSerializer().Serialize(onelayerreq10);
            string newstr8 = onelayerreqstr8.Replace("\"alpha\":1,", "\"alpha\":1.0,");
            Form1.wb.send(newstr8);
            citystring.Add(newstr8); 

            return citystring;
        }

        public void NotifyString(string s)
        {
            this.notifyIcon1.ShowBalloonTip(1000, "提示：", s, ToolTipIcon.Info);
        }

        private void 设置登录用户名密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //acflag = false;
            //this.actimer.Stop();
            this.TopMost = false;
            //MessageBox.Show("此处用来修改用户名和密码");
            changeunpw form2 = new changeunpw();
            form2.StartPosition = FormStartPosition.CenterScreen;
            form2.Show();         
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
                if (System.Math.Abs(x1 - x2) < width / 2 && System.Math.Abs(y1 - y2) < height / 2)
                {
                    /*
                    mainMenu = new ContextMenuStrip();
                    mainMenu.BigButtons();
                    mainMenu.Font = new Font("Microsoft YaHei UI", bianchang / 30, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
                    mainMenu.Opening += new CancelEventHandler(cms_Opening);
                    
                    menuMaxSize = Convert.ToInt32(Math.Round(Convert.ToDouble(bianchang / 3 * 2)));
                    mainMenu.MaximumSize = new Size(menuMaxSize,menuMaxSize);
                    menuLocate = Convert.ToInt32(Math.Round(Convert.ToDouble(bianchang / 5.5)));
                    mainMenu.Show(this,menuLocate,menuLocate);
                    */
                    //this.Hide();
                    //Form2 frm2 = new Form2();

                    //frm2.Height = bianchang * 2;
                    //frm2.Width = bianchang * 2;

                    //frm2.ShowDialog();

                    //Formselect frms = new Formselect();
                    //frms.ShowDialog();
                }
                else
                {
                    this.Location = new System.Drawing.Point(a - width / 2, b - height / 2);

                }
            }
        }

        private void 设置控件大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //acflag = false;
            //this.actimer.Stop();
            this.TopMost = false;
            changeSize form3 = new changeSize();
            form3.StartPosition = FormStartPosition.CenterScreen;
            form3.Show();
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
            nextpro.Click += delegate (Object o, EventArgs e2) { wb.send("{\"body\" : \"\",\"guid\" : \"M-171\",\"type\" : \"NEXTPROGRAM\"}"); };
            previouspro.Click += delegate (Object o, EventArgs e3) { wb.send("{\"body\" : \"\",\"guid\" : \"M-172\",\"type\" : \"PREVIOUSPROGRAM\"}"); };

            if (abc.passflag == "loginwin")
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
            wb.send(s1);
        }

        /*
        private void closeOnTimedEvent(object source, ElapsedEventArgs e)
        {
            
        }
        private void closeMenu()
        {
            mainMenu.Close();
        }
        */

    }
    public static class Global
    {
        public static Form1 MainForm { get; set; }
    }
}
