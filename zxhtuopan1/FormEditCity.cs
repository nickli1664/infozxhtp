using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Web.Script.Serialization;
using System.IO;

namespace zxhtuopan1
{
    public partial class FormEditCity : Form
    {
        //public List<Panel> pTemp = new List<Panel>();             //这里需要注意，新建集合时不要设置为null，否则之后只可以被赋值，调用Add方法就会出错 || 此全局变量暂时废弃，储存的Panel无法多次添加，添加新的后即会把上一次添加的删除。

        private PickBox pb = new PickBox();                         //实现运行时可编辑控件（NB的封装思路）。

        private static int pianyi = 20;                             //绘制界面相对于（0，0）点的偏移量
        private static int suoxiaobili = 10;                        //绘制界面的全局缩小比例

        private int totalx;                                         //videowall的总长度，目前不支持级联，下同
        private int totaly;

        //private List<CityName> cityinfonick = new List<CityName>();      //全局的城市完整信息存储对象，载入时从文件中读取保存在内存中，保存时保存到文件
        //private CityInfo gbci = new CityInfo();                    


        public FormEditCity()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(Control c in this.tabControl1.Controls)
            {
                if (c is TabPage)
                {
                    this.tabControl1.Controls.Remove(c);
                }
            }
            listBox1.Items.Clear();

            BuissnesServiceImpl.folderjihe1.Clear();
            BuissnesServiceImpl.mediajihe1.Clear();

            BuissnesServiceImpl.devicefolderjihe1.Clear();
            BuissnesServiceImpl.devicejihe1.Clear();

            Close();
            Global.MainForm.actimer.Start();                            //已关闭大多数的定时器开关操作。这里打开是合理的，在编辑状态不工作，退出后开始工作。  --20170503 nick
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool add = true;               
                foreach (var i in listBox1.Items)
                {
                    //MessageBox.Show(i.ToString());
                    if (i.ToString() == textBox1.Text || textBox1.Text == "")
                   {
                        MessageBox.Show("请不要添加重复的位置或空。");
                        add = false;
                        break;
                   }
                }
                if (add == true)
                {
                    listBox1.Items.Add(textBox1.Text);

                    TabPage Newpage1 = new TabPage();
                    //Newpage1.Name = "newPage0";
                    Newpage1.Padding = new System.Windows.Forms.Padding(3);
                    Newpage1.Size = new System.Drawing.Size(694, 484);
                    //Newpage1.TabIndex = 0;
                    Newpage1.Text = textBox1.Text;
                    Newpage1.UseVisualStyleBackColor = true;

                    /*
                    int pianyi = 20;
                    int suoxiaobili = 10;

                    foreach (MonitorInfoelement monitor in BuissnesServiceImpl.slaveinfoele[0].monitorInfo)
                    {
                        Panel p1 = new Panel();
                        p1.BorderStyle = BorderStyle.FixedSingle;
                        p1.Size = new Size(monitor.width / suoxiaobili, monitor.height / suoxiaobili);
                        p1.Location = new Point(pianyi + monitor.left / suoxiaobili, pianyi + monitor.top / suoxiaobili);

                        Newpage1.Controls.Add(p1);
                    }
                    */
                    AddPanelToTabpage(Newpage1);

                    this.tabControl1.Controls.Add(Newpage1);

                    this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                    //this.tabControl1.SelectedIndex = 1;

                    //MessageBox.Show(this.tabControl1.SelectedIndex.ToString() + "----" + this.tabControl1.TabPages.Count);
                }                
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItem != null)
                {                   
                    foreach(TabPage tp in this.tabControl1.Controls)
                    {
                        if(tp.Text == listBox1.SelectedItem.ToString())
                        {
                            this.tabControl1.TabPages.Remove(tp);
                        }
                    }

                    listBox1.Items.Remove(listBox1.SelectedItem);
                }
                if (listBox1.Items.Count == 0)
                {
                    return;
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }

        private void FormEditCity_Load(object sender, EventArgs e)
        {
            Form1.wb.send(@" { ""body"" : """", ""guid"" : ""M-44"", ""type"" : ""LOADVIDEOWALLINFO"" }");

            Global.MainForm.NotifyString("努力加载中，请稍候。");

            Thread.Sleep(1000);

            int tx = BuissnesServiceImpl.slaveinfoele[0].monitorInfo[0].left + BuissnesServiceImpl.slaveinfoele[0].monitorInfo[0].width;

            int i = 0;
            while (i < BuissnesServiceImpl.slaveinfoele[0].monitorInfo.Count * 2)
            {
                foreach (MonitorInfoelement monitor in BuissnesServiceImpl.slaveinfoele[0].monitorInfo)
                {
                    if (monitor.left == tx)
                    {
                        tx = tx + monitor.width;
                        break;
                    }
                }
                i++;
            }

            i = 0;
            int ty = BuissnesServiceImpl.slaveinfoele[0].monitorInfo[0].top + BuissnesServiceImpl.slaveinfoele[0].monitorInfo[0].height;
            while (i < BuissnesServiceImpl.slaveinfoele[0].monitorInfo.Count * 2)
            {
                foreach (MonitorInfoelement monitor in BuissnesServiceImpl.slaveinfoele[0].monitorInfo)
                {
                    if (monitor.top == ty)
                    {
                        ty = ty + monitor.height;
                        break;
                    }
                }
                i++;
            }
            this.totalx = tx;
            this.totaly = ty;

            //Global.MainForm.NotifyString(totalx.ToString() + "----" + totaly.ToString());

            /*   以下代码转入Form1
            string strduquci = File.ReadAllText(@"cityinfonick.txt");
            JavaScriptSerializer jsci2 = new JavaScriptSerializer();
            CityInfo ciduqu = jsci2.Deserialize<CityInfo>(strduquci);
            this.gbci = ciduqu;
            this.cityinfonick = ciduqu.cityinfo;
            */

            foreach (CityName cn1 in Global.MainForm.cityinfonick)
            {
                listBox1.Items.Add(cn1.cityname);

                TabPage Newpage1 = new TabPage();
                Newpage1.Padding = new System.Windows.Forms.Padding(3);
                Newpage1.Size = new System.Drawing.Size(694, 484);
                Newpage1.Text = cn1.cityname;
                Newpage1.UseVisualStyleBackColor = true;

                //AddPanelToTabpage(Newpage1);

                LayerMiddle lm = new LayerMiddle();
                lm.name = cn1.name;
                lm.type = cn1.type;
                lm.majorID = cn1.majorID;
                lm.minorID = cn1.minorID;

                string tagadd = new JavaScriptSerializer().Serialize(lm);

                Button b1 = new Button();

                b1.Text = cn1.name;
                b1.Tag = tagadd;


                b1.Size = new Size(FourMultiplication(cn1.slavewidth, totalx), FourMultiplication(cn1.slaveheight,totaly));                         //重点留意
                //b1.Size = new Size(600,600);
                b1.Location = new Point(FourMultiplication(cn1.slaveleft, totalx) + pianyi, FourMultiplication(cn1.slavetop, totaly) + pianyi);     //重点留意
                //b1.Location = new Point(20,20);

                b1.ContextMenuStrip = contextMenuStrip1;

                b1.LocationChanged += new System.EventHandler(B1_LocationChanged);
                b1.SizeChanged += new System.EventHandler(B1_SizeChanged);
                b1.Click += new System.EventHandler(B1_Click);
                b1.Resize += new System.EventHandler(B1_Resize);

                pb.WireControl(b1);

                //b1.BringToFront();

                Newpage1.Controls.Add(b1);

                AddPanelToTabpage(Newpage1);

                this.tabControl1.Controls.Add(Newpage1);
            }


            try
            {
                /*
                foreach (MonitorInfoelement monitor in BuissnesServiceImpl.slaveinfoele[0].monitorInfo)
                {
                    Panel p1 = new Panel();
                    p1.BorderStyle = BorderStyle.FixedSingle;
                    p1.Size = new Size(monitor.width / suoxiaobili, monitor.height / suoxiaobili);
                    p1.Location = new Point(pianyi + monitor.left / suoxiaobili, pianyi + monitor.top / suoxiaobili);

                    //pTemp.Add(p1);

                    this.tabPage1.Controls.Add(p1);
                }
                */

                //AddPanelToTabpage(this.tabPage1);


                NewTabControl ntc1 = new NewTabControl();
                ntc1.Location = new Point(1037, 50);
                ntc1.SelectedIndexChanged += new EventHandler(ntc1_SelectedIndexChanged);

                TabPage Newpage1 = new TabPage();
                //Newpage1.Location = new System.Drawing.Point(4, 22);
                Newpage1.Name = "newPage0";
                Newpage1.Padding = new System.Windows.Forms.Padding(3);
                Newpage1.Size = new System.Drawing.Size(187, 444);
                Newpage1.TabIndex = 0;
                Newpage1.Text = "设备";
                Newpage1.UseVisualStyleBackColor = true;

                TabPage Newpage2 = new TabPage();
                Newpage2.Name = "newPage1";
                Newpage2.Padding = new System.Windows.Forms.Padding(3);
                Newpage2.Size = new System.Drawing.Size(187, 444);
                Newpage2.TabIndex = 1;
                Newpage2.Text = "媒体";
                Newpage2.UseVisualStyleBackColor = true;

                TabPage Newpage3 = new TabPage();
                Newpage3.Name = "newPage2";
                Newpage3.Padding = new System.Windows.Forms.Padding(3);
                Newpage3.Size = new System.Drawing.Size(187, 444);
                Newpage3.TabIndex = 2;
                Newpage3.Text = "录像";
                Newpage3.UseVisualStyleBackColor = true;

                TabPage Newpage4 = new TabPage();
                Newpage4.Name = "newPage3";
                Newpage4.Padding = new System.Windows.Forms.Padding(3);
                Newpage4.Size = new System.Drawing.Size(187, 444);
                Newpage4.TabIndex = 3;
                Newpage4.Text = "分组";
                Newpage4.UseVisualStyleBackColor = true;

                treeView1.Location = new Point(3, 6);
                Newpage1.Controls.Add(treeView1);

                treeView2.Location = new Point(3, 6);
                Newpage2.Controls.Add(treeView2);

                ntc1.Name = "newtabControl";
                ntc1.SelectedIndex = 0;
                ntc1.Size = new System.Drawing.Size(195, 470);
                ntc1.TabIndex = 0;
                ntc1.Controls.Add(Newpage1);
                ntc1.Controls.Add(Newpage2);
                ntc1.Controls.Add(Newpage3);
                ntc1.Controls.Add(Newpage4);
                this.Controls.Add(ntc1);


                for (int id4confirm = 11; id4confirm < 15; id4confirm++)                                      //载入媒体4个根目录的基本信息
                {
                    GetMediaFolderInforeqbody id4body = new GetMediaFolderInforeqbody();
                    id4body.folderID = id4confirm;
                    GetMediaFolderInforeq id4 = new GetMediaFolderInforeq();
                    id4.body = id4body;
                    string sGetMediaFolderInforeq = new JavaScriptSerializer().Serialize(id4);
                    Form1.wb.send(sGetMediaFolderInforeq);

                    Thread.Sleep(200);

                    if (BuissnesServiceImpl.mediafolderinfobody.hasChild == true)
                    {
                        //MessageBox.Show("文件夹" + BuissnesServiceImpl.mediafolderinfobody.folderName + "还有子项（经check此子项是指子文件夹）");

                        GetChildMediaFolderListreqbody childmediafolderlistreqbody = new GetChildMediaFolderListreqbody();    //生成获取子文件夹的请求并发送
                        childmediafolderlistreqbody.parentID = BuissnesServiceImpl.mediafolderinfobody.folderID;
                        GetChildMediaFolderListreq childmediafolderlistreq = new GetChildMediaFolderListreq();
                        childmediafolderlistreq.body = childmediafolderlistreqbody;
                        string sGetChildMediaFolderListreq = new JavaScriptSerializer().Serialize(childmediafolderlistreq);
                        Form1.wb.send(sGetChildMediaFolderListreq);
                        Thread.Sleep(1000);

                        /*
                        foreach (FolderInfo fi in BuissnesServiceImpl.folderjihe1)
                        {
                            this.treeView2.Nodes[(fi.pId-11)].Nodes.Add(fi.fName);
                        }
                        */
                    }
                    else if (BuissnesServiceImpl.mediafolderinfobody.hasChild == false)
                    {
                        //MessageBox.Show("文件夹" + BuissnesServiceImpl.mediafolderinfobody.folderName + "没有子项（经check此子项是指子文件夹）！！");
                    }

                    GetMediaFileListreqbody id4mediabody = new GetMediaFileListreqbody();
                    id4mediabody.id = id4confirm;
                    GetMediaFileListreq id4media = new GetMediaFileListreq();
                    id4media.body = id4mediabody;
                    string sGetMediaFileListreq = new JavaScriptSerializer().Serialize(id4media);
                    Form1.wb.send(sGetMediaFileListreq);

                    Thread.Sleep(200);

                }

                foreach (FolderInfo fi in BuissnesServiceImpl.folderjihe1)
                {
                    if (fi.pId > 10 && fi.pId < 15)
                    {
                        //this.treeView2.Nodes[(fi.pId - 11)].Nodes.Add(fi.fName, fi.fName, 1, 1);

                        TreeNode newfolder = new TreeNode(fi.fName, 1, 1);
                        newfolder.Name = Convert.ToString(fi.folderId);
                        this.treeView2.Nodes[(fi.pId - 11)].Nodes.Add(newfolder);
                        fi.treeViewIndex = newfolder.Index;                                                     //这里是大坑啊，一定要深刻理解这几句命令的意义和顺序！不然index永远为0
                    }
                }

                foreach (MediaList medial in BuissnesServiceImpl.mediajihe1)
                {
                    if (medial.pId > 10 && medial.pId < 15)
                    {
                        this.treeView2.Nodes[(medial.pId - 11)].Nodes.Add(medial.fileName, medial.fileName, 6, 6);
                    }
                }

                Thread.Sleep(200);
                //Thread.Sleep(5000);

                //MessageBox.Show(i.ToString());

                for (int devicerootint = 1; devicerootint < 8; devicerootint++)
                {
                    GetDeviceFolderInforeqbody devicefolderinforeqbodyroot = new GetDeviceFolderInforeqbody();               //发送请求，载入设备若干个根目录的基本信息
                    devicefolderinforeqbodyroot.typeid = Convert.ToUInt32(devicerootint);
                    GetDeviceFolderInforeq devicefolderinforeqroot = new GetDeviceFolderInforeq();
                    devicefolderinforeqroot.body = devicefolderinforeqbodyroot;
                    string sGetDeviceFolderInforeqroot = new JavaScriptSerializer().Serialize(devicefolderinforeqroot);
                    Form1.wb.send(sGetDeviceFolderInforeqroot);

                    Thread.Sleep(200);

                    if (BuissnesServiceImpl.devicefolderinfobody.hasChild == true)
                    {
                        GetDeviceTypeFolderListreqbody childdevicetypefolderlistreqbody = new GetDeviceTypeFolderListreqbody();
                        childdevicetypefolderlistreqbody.typeid = BuissnesServiceImpl.devicefolderinfobody.folderID;
                        GetDeviceTypeFolderListreq childdevicetypefolderlistreq = new GetDeviceTypeFolderListreq();
                        childdevicetypefolderlistreq.body = childdevicetypefolderlistreqbody;
                        string sGetDevicTypeFolderListreq = new JavaScriptSerializer().Serialize(childdevicetypefolderlistreq);
                        Form1.wb.send(sGetDevicTypeFolderListreq);
                        Thread.Sleep(1000);
                    }
                    else if (BuissnesServiceImpl.devicefolderinfobody.hasChild == false)
                    {

                    }

                    GetDeviceListreqbody devicelistreqbodyroot = new GetDeviceListreqbody();
                    devicelistreqbodyroot.id = Convert.ToUInt32(devicerootint);
                    GetDeviceListreq devicelistreqroot = new GetDeviceListreq();
                    devicelistreqroot.body = devicelistreqbodyroot;
                    string sGetDeviceListreqroot = new JavaScriptSerializer().Serialize(devicelistreqroot);
                    Form1.wb.send(sGetDeviceListreqroot);

                    Thread.Sleep(200);
                }

                foreach (DeviceFolderele dfe in BuissnesServiceImpl.devicefolderjihe1)
                {
                    if (dfe.parentID > 0 && dfe.parentID < 11)
                    {
                        TreeNode newdevicefolder = new TreeNode(dfe.deviceFolderName, 2, 2);
                        newdevicefolder.Name = Convert.ToString(dfe.folderID);
                        this.treeView1.Nodes[(Convert.ToInt16(dfe.parentID) - 1)].Nodes.Add(newdevicefolder);
                        dfe.treeViewIndex = newdevicefolder.Index;
                    }
                }
                foreach (DeviceList devicel in BuissnesServiceImpl.devicejihe1)
                {
                    if (devicel.devicepId > 0 && devicel.devicepId < 11)
                    {
                        switch (devicel.deviceType)
                        {
                            case 1:
                                TreeNode newcontroller = new TreeNode(devicel.deviceName, 5, 5);
                                newcontroller.Name = Convert.ToString(devicel.deviceId);
                                this.treeView1.Nodes[Convert.ToInt16(devicel.devicepId) - 1].Nodes.Add(newcontroller);
                                if (devicel.sourceinfo != null)
                                {
                                    for (int i1 = 0; i1 < devicel.sourceinfo.Count; i1++)
                                    {
                                        TreeNode newapp = new TreeNode(devicel.sourceinfo[i1].sourceName, 3, 3);
                                        newapp.Name = Convert.ToString(devicel.sourceinfo[i1].sourceId);
                                        newcontroller.Nodes.Add(newapp);
                                        /*
                                        foreach (TreeNode controller1 in this.treeView1.Nodes[Convert.ToInt16(devicel.devicepId) - 1].Nodes)
                                        {
                                            if (controller1.Name == devicel.deviceName)
                                            {
                                                controller1.Nodes.Add(newapp);
                                            }
                                        }
                                        */
                                    }
                                }
                                break;
                            case 2:
                                TreeNode newmatrix = new TreeNode(devicel.deviceName, 9, 9);
                                newmatrix.Name = Convert.ToString(devicel.deviceId);
                                this.treeView1.Nodes[Convert.ToInt16(devicel.devicepId) - 1].Nodes.Add(newmatrix);
                                if (devicel.sourceinfo != null)
                                {
                                    for (int i2 = 0; i2 < devicel.sourceinfo.Count; i2++)
                                    {
                                        TreeNode newmatrixsource = new TreeNode(devicel.sourceinfo[i2].sourceName, 10, 10);
                                        newmatrixsource.Name = Convert.ToString(devicel.sourceinfo[i2].sourceId);
                                        newmatrix.Nodes.Add(newmatrixsource);
                                    }
                                }
                                break;
                            case 3:
                                TreeNode newnvr = new TreeNode(devicel.deviceName, 11, 11);
                                newnvr.Name = Convert.ToString(devicel.deviceId);
                                this.treeView1.Nodes[Convert.ToInt16(devicel.devicepId) - 1].Nodes.Add(newnvr);
                                if (devicel.sourceinfo != null)
                                {
                                    for (int i3 = 0; i3 < devicel.sourceinfo.Count; i3++)
                                    {
                                        TreeNode newnvrsource = new TreeNode(devicel.sourceinfo[i3].sourceName, 6, 6);
                                        newnvrsource.Name = Convert.ToString(devicel.sourceinfo[i3].sourceId);
                                        newnvr.Nodes.Add(newnvrsource);
                                    }
                                }
                                break;
                            case 4:
                                TreeNode newipc = new TreeNode(devicel.deviceName, 7, 7);
                                newipc.Name = Convert.ToString(devicel.deviceId);
                                this.treeView1.Nodes[Convert.ToInt16(devicel.devicepId) - 1].Nodes.Add(newipc);
                                if (devicel.sourceinfo != null)
                                {
                                    for (int i4 = 0; i4 < devicel.sourceinfo.Count; i4++)
                                    {
                                        TreeNode newipcsource = new TreeNode(devicel.sourceinfo[i4].sourceName, 6, 6);
                                        newipcsource.Name = Convert.ToString(devicel.sourceinfo[i4].sourceId);
                                        newipc.Nodes.Add(newipcsource);
                                    }
                                }
                                break;
                            case 5:
                                TreeNode newstreaming = new TreeNode(devicel.deviceName, 8, 8);
                                newstreaming.Name = Convert.ToString(devicel.deviceId);
                                this.treeView1.Nodes[Convert.ToInt16(devicel.devicepId) - 1].Nodes.Add(newstreaming);
                                break;
                            case 6:
                                TreeNode newtrans = new TreeNode(devicel.deviceName, 12, 12);
                                newtrans.Name = Convert.ToString(devicel.deviceId);
                                this.treeView1.Nodes[Convert.ToInt16(devicel.devicepId) - 1].Nodes.Add(newtrans);
                                break;
                            case 7:
                                TreeNode newprojector = new TreeNode(devicel.deviceName, 1, 1);
                                newprojector.Name = Convert.ToString(devicel.deviceId);
                                this.treeView1.Nodes[Convert.ToInt16(devicel.devicepId) - 1].Nodes.Add(newprojector);
                                break;
                        }

                        //this.treeView1.Nodes[Convert.ToInt16(devicel.devicepId) - 1].Nodes.Add(devicel.deviceName, devicel.deviceName, 0, 0);
                    }
                }
                Thread.Sleep(200);

            } 
            catch (Exception e3)
            {
                MessageBox.Show(e3.Message);
            }       
        }

        private void AddPanelToTabpage(TabPage tp)
        {
            //int pianyi = 20;
            //int suoxiaobili = 10;

            foreach (MonitorInfoelement monitor in BuissnesServiceImpl.slaveinfoele[0].monitorInfo)
            {
                Panel p1 = new Panel();
                p1.BorderStyle = BorderStyle.FixedSingle;
                p1.Size = new Size(monitor.width / suoxiaobili, monitor.height / suoxiaobili);
                p1.Location = new Point(pianyi + monitor.left / suoxiaobili, pianyi + monitor.top / suoxiaobili);

                tp.Controls.Add(p1);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(listBox1.SelectedItem.ToString());
            try
            {
                for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
                {
                    if (this.tabControl1.TabPages[i].Text == listBox1.SelectedItem.ToString())
                    {
                        this.tabControl1.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch (Exception e2)
            {
                this.tabControl1.SelectedIndex = 0;
                MessageBox.Show(e2.Message);
            }           
            /*
            foreach (TabPage tp in this.tabControl1.Controls)
            {
                if (listBox1.SelectedItem.ToString() == tp.Text)
                {
                    MessageBox.Show(tp.);
                }
            }
            */
        }

        private void treeView2_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeView2.SelectedNode = e.Node;
            //MessageBox.Show(Convert.ToString(treeView2.SelectedNode.Text));
            //int temp = treeView2.SelectedNode.Index + 11;

            foreach (FolderInfo fi in BuissnesServiceImpl.folderjihe1.ToArray())                    //存疑，一般的写法有可能引发集合被修改的异常
            {
                if ((fi.pId == Convert.ToInt16(e.Node.Name)) && fi.test == false)
                {
                    fi.test = true;
                    //MessageBox.Show(fi.fName);
                    if (fi.hasChild == true)
                    {
                        GetChildMediaFolderListreqbody childmediafolderlistreqbody = new GetChildMediaFolderListreqbody();    //生成获取子文件夹的请求并发送
                        childmediafolderlistreqbody.parentID = fi.folderId;                                                   //不好理解，待定
                        GetChildMediaFolderListreq childmediafolderlistreq = new GetChildMediaFolderListreq();
                        childmediafolderlistreq.body = childmediafolderlistreqbody;
                        string sGetChildMediaFolderListreq = new JavaScriptSerializer().Serialize(childmediafolderlistreq);
                        Form1.wb.send(sGetChildMediaFolderListreq);
                        Thread.Sleep(200);

                        foreach (FolderInfo fi2 in BuissnesServiceImpl.folderjihe1)
                        {
                            if (fi2.pId > 15 && fi2.add == false)
                            {
                                //this.treeView2.Nodes[(fi2.pId - 11)].Nodes.Add(fi2.fName, fi2.fName, 1, 1);
                                TreeNode newfolder = new TreeNode(fi2.fName, 1, 1);
                                newfolder.Name = Convert.ToString(fi2.folderId);
                                e.Node.Nodes[fi.treeViewIndex].Nodes.Add(newfolder);
                                fi2.treeViewIndex = newfolder.Index;
                                fi2.add = true;
                            }
                        }

                    }
                    if (fi.hasMedia == true)
                    {
                        GetMediaFileListreqbody id4mediabody = new GetMediaFileListreqbody();
                        id4mediabody.id = fi.folderId;
                        GetMediaFileListreq id4media = new GetMediaFileListreq();
                        id4media.body = id4mediabody;
                        string sGetMediaFileListreq = new JavaScriptSerializer().Serialize(id4media);
                        Form1.wb.send(sGetMediaFileListreq);
                        Thread.Sleep(200);

                        foreach (MediaList medial1 in BuissnesServiceImpl.mediajihe1)
                        {
                            if (medial1.pId > 15 && medial1.add == false)
                            {
                                e.Node.Nodes[fi.treeViewIndex].Nodes.Add(medial1.fileName, medial1.fileName, 6, 6);
                                medial1.add = true;
                            }
                        }
                    }
                }
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeView1.SelectedNode = e.Node;

            foreach (DeviceFolderele dfe2 in BuissnesServiceImpl.devicefolderjihe1.ToArray())
            {
                if ((dfe2.parentID == Convert.ToInt16(e.Node.Name)) && dfe2.test == false)
                {
                    dfe2.test = true;
                    if (dfe2.hasChild == true)
                    {
                        GetDeviceTypeFolderListreqbody childdevicetypefolderlistreqbody = new GetDeviceTypeFolderListreqbody();
                        childdevicetypefolderlistreqbody.typeid = dfe2.folderID;
                        GetDeviceTypeFolderListreq childdevicetypefolderlistreq = new GetDeviceTypeFolderListreq();
                        childdevicetypefolderlistreq.body = childdevicetypefolderlistreqbody;
                        string sGetDeviceTypeFolderListreq = new JavaScriptSerializer().Serialize(childdevicetypefolderlistreq);
                        Form1.wb.send(sGetDeviceTypeFolderListreq);
                        Thread.Sleep(200);

                        foreach (DeviceFolderele dfe3 in BuissnesServiceImpl.devicefolderjihe1)
                        {
                            if (dfe3.parentID > 10 && dfe3.add == false)
                            {
                                TreeNode newdevicefolder = new TreeNode(dfe3.deviceFolderName, 2, 2);
                                newdevicefolder.Name = Convert.ToString(dfe3.folderID);
                                e.Node.Nodes[dfe3.treeViewIndex].Nodes.Add(newdevicefolder);
                                dfe3.treeViewIndex = newdevicefolder.Index;
                                dfe3.add = true;
                            }
                        }
                    }
                    if (dfe2.hasDevice == true)
                    {
                        GetDeviceListreqbody devicelistreqbody = new GetDeviceListreqbody();
                        devicelistreqbody.id = Convert.ToUInt32(dfe2.folderID);
                        GetDeviceListreq devicelistreq = new GetDeviceListreq();
                        devicelistreq.body = devicelistreqbody;
                        string sGetDeviceListreq = new JavaScriptSerializer().Serialize(devicelistreq);
                        Form1.wb.send(sGetDeviceListreq);

                        Thread.Sleep(200);

                        foreach (DeviceList devicel1 in BuissnesServiceImpl.devicejihe1)
                        {
                            if (devicel1.devicepId > 10 && devicel1.add == false)
                            {
                                //e.Node.Nodes[dfe2.treeViewIndex].Nodes.Add(devicel1.deviceName, devicel1.deviceName, 6, 6);
                                //devicel1.add = true;

                                switch (devicel1.deviceType)
                                {
                                    case 1:
                                        TreeNode newcontroller = new TreeNode(devicel1.deviceName, 5, 5);
                                        newcontroller.Name = Convert.ToString(devicel1.deviceId);
                                        e.Node.Nodes[dfe2.treeViewIndex].Nodes.Add(newcontroller);
                                        if (devicel1.sourceinfo != null)
                                        {
                                            for (int i1 = 0; i1 < devicel1.sourceinfo.Count; i1++)
                                            {
                                                TreeNode newapp = new TreeNode(devicel1.sourceinfo[i1].sourceName, 3, 3);
                                                newapp.Name = Convert.ToString(devicel1.sourceinfo[i1].sourceId);
                                                newcontroller.Nodes.Add(newapp);
                                            }
                                        }
                                        devicel1.add = true;
                                        break;
                                    case 2:
                                        TreeNode newmatrix = new TreeNode(devicel1.deviceName, 9, 9);
                                        newmatrix.Name = Convert.ToString(devicel1.deviceId);
                                        e.Node.Nodes[dfe2.treeViewIndex].Nodes.Add(newmatrix);
                                        if (devicel1.sourceinfo != null)
                                        {
                                            for (int i1 = 0; i1 < devicel1.sourceinfo.Count; i1++)
                                            {
                                                TreeNode newmatrixsource = new TreeNode(devicel1.sourceinfo[i1].sourceName, 10, 10);
                                                newmatrixsource.Name = Convert.ToString(devicel1.sourceinfo[i1].sourceId);
                                                newmatrix.Nodes.Add(newmatrixsource);
                                            }
                                        }
                                        devicel1.add = true;
                                        break;
                                    case 3:
                                        TreeNode newnvr = new TreeNode(devicel1.deviceName, 11, 11);
                                        newnvr.Name = Convert.ToString(devicel1.deviceId);
                                        e.Node.Nodes[dfe2.treeViewIndex].Nodes.Add(newnvr);
                                        if (devicel1.sourceinfo != null)
                                        {
                                            for (int i1 = 0; i1 < devicel1.sourceinfo.Count; i1++)
                                            {
                                                TreeNode newnvrsource = new TreeNode(devicel1.sourceinfo[i1].sourceName, 6, 6);
                                                newnvrsource.Name = Convert.ToString(devicel1.sourceinfo[i1].sourceId);
                                                newnvr.Nodes.Add(newnvrsource);
                                            }
                                        }
                                        devicel1.add = true;
                                        break;
                                    case 4:
                                        TreeNode newipc = new TreeNode(devicel1.deviceName, 7, 7);
                                        newipc.Name = Convert.ToString(devicel1.deviceId);
                                        e.Node.Nodes[dfe2.treeViewIndex].Nodes.Add(newipc);
                                        if (devicel1.sourceinfo != null)
                                        {
                                            for (int i1 = 0; i1 < devicel1.sourceinfo.Count; i1++)
                                            {
                                                TreeNode newipcsource = new TreeNode(devicel1.sourceinfo[i1].sourceName, 6, 6);
                                                newipcsource.Name = Convert.ToString(devicel1.sourceinfo[i1].sourceId);
                                                newipc.Nodes.Add(newipcsource);
                                            }
                                        }
                                        devicel1.add = true;
                                        break;
                                    case 5:
                                        TreeNode newstreaming = new TreeNode(devicel1.deviceName, 8, 8);
                                        newstreaming.Name = Convert.ToString(devicel1.deviceId);
                                        e.Node.Nodes[dfe2.treeViewIndex].Nodes.Add(newstreaming);
                                        devicel1.add = true;
                                        break;
                                    case 6:
                                        TreeNode newtrans = new TreeNode(devicel1.deviceName, 12, 12);
                                        newtrans.Name = Convert.ToString(devicel1.deviceId);
                                        e.Node.Nodes[dfe2.treeViewIndex].Nodes.Add(newtrans);
                                        devicel1.add = true;
                                        break;
                                    case 7:
                                        TreeNode newprojector = new TreeNode(devicel1.deviceName, 1, 1);
                                        newprojector.Name = Convert.ToString(devicel1.deviceId);
                                        e.Node.Nodes[dfe2.treeViewIndex].Nodes.Add(newprojector);
                                        devicel1.add = true;
                                        break;
                                }
                            }
                        }
                    }

                }
            }
        }

        private void ntc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("123");
            treeView1.SelectedNode = null;
            treeView2.SelectedNode = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.tabControl1.SelectedTab.Text);
            bool add = true;
            
            foreach (Control item in this.tabControl1.SelectedTab.Controls)
            {
                if (item is Button)
                {
                    MessageBox.Show("当前版本每个位置只允许添加一个图层。");
                    add = false;
                    break;
                }
            }
            if (add == true)
            {
                //MessageBox.Show(Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id));
                if (this.treeView2.SelectedNode != null)
                {
                    if (this.treeView2.SelectedNode.ImageIndex == 6)                                               //通过这种方式判定选中的节点是否为媒体对象
                    {
                        Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
                        Thread.Sleep(1000);
                        //MessageBox.Show(Convert.ToString(BuissnesServiceImpl.idlist[0].id)+"----"+ Convert.ToString(BuissnesServiceImpl.idlist[1].id));

                        LayerItemelement onelayeritem1 = new LayerItemelement();
                        onelayeritem1.ID = BuissnesServiceImpl.idlist[1].id;
                        onelayeritem1.description = "";
                        onelayeritem1.majorID = -1;
                        //onelayeritem1.minorID = 117;
                        //onelayeritem1.name = "圆形按钮桌面图标下载24.png";
                        onelayeritem1.playOrder = 0;
                        onelayeritem1.playTime = 30;
                        onelayeritem1.refreshTime = 1800;
                        //onelayeritem1.type = 43;
                        foreach (MediaList mediabofang in BuissnesServiceImpl.mediajihe1)
                        {
                            if (mediabofang.fileName == this.treeView2.SelectedNode.Text)
                            {
                                onelayeritem1.minorID = Convert.ToInt64(mediabofang.fileId);
                                onelayeritem1.name = mediabofang.fileName;
                                onelayeritem1.type = mediabofang.type;
                            }
                        }

                        LayerActionreqbody onelayerbody1 = new LayerActionreqbody();
                        List<LayerItemelement> onelayeritem = new List<LayerItemelement>();
                        onelayeritem.Add(onelayeritem1);

                        onelayerbody1.alpha = 1.0;
                        onelayerbody1.highlight = false;
                        onelayerbody1.layerID = BuissnesServiceImpl.idlist[0].id;
                        onelayerbody1.layerItem = onelayeritem;

                        string piecexmlhalf = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0\" slavetop=\"0\" slavewidth=\"1\" slaveheight=\"1\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
                        string slave0ID = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
                        onelayerbody1.pieceXml = piecexmlhalf.Replace("abc", slave0ID);


                        onelayerbody1.type = "Add";
                        onelayerbody1.zOrder = 10;

                        LayerActionreq onelayerreq = new LayerActionreq();
                        onelayerreq.body = onelayerbody1;

                        string onelayerreqstr = new JavaScriptSerializer().Serialize(onelayerreq);
                        string newstr = onelayerreqstr.Replace("\"alpha\":1,", "\"alpha\":1.0,");
                        //MessageBox.Show(newstr);
                        //Form1.wb.send(newstr);

                        LayerMiddle lm = new LayerMiddle();
                        lm.majorID = "-1";

                        foreach (MediaList mediabofang in BuissnesServiceImpl.mediajihe1)
                        {
                            if (mediabofang.fileName == this.treeView2.SelectedNode.Text)
                            {
                                lm.minorID = mediabofang.fileId.ToString();
                                lm.name = mediabofang.fileName;
                                lm.type = mediabofang.type.ToString();
                            }
                        }

                        string totag = new JavaScriptSerializer().Serialize(lm);

                        AddLayer(this.treeView2.SelectedNode.Text,totag);
                        //MessageBox.Show(totag);
                    }
                }
                else if (this.treeView1.SelectedNode != null)
                {
                    if (this.treeView1.SelectedNode.ImageIndex == 1 || this.treeView1.SelectedNode.ImageIndex == 7 || this.treeView1.SelectedNode.ImageIndex == 8 || this.treeView1.SelectedNode.ImageIndex == 12)          //通过这种方式判定选中的节点是否为设备对象
                    {
                        Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
                        Thread.Sleep(1000);
                        //MessageBox.Show(Convert.ToString(BuissnesServiceImpl.idlist[0].id)+"----"+ Convert.ToString(BuissnesServiceImpl.idlist[1].id));

                        LayerItemelement onelayeritem2 = new LayerItemelement();
                        onelayeritem2.ID = BuissnesServiceImpl.idlist[1].id;
                        onelayeritem2.description = "";
                        //onelayeritem2.majorID = -1;
                        onelayeritem2.minorID = -1;
                        //onelayeritem1.name = "圆形按钮桌面图标下载24.png";
                        onelayeritem2.playOrder = 0;
                        onelayeritem2.playTime = 30;
                        onelayeritem2.refreshTime = 1800;
                        //onelayeritem1.type = 43;
                        foreach (DeviceList devicebofang in BuissnesServiceImpl.devicejihe1)
                        {
                            if (devicebofang.deviceName == this.treeView1.SelectedNode.Text)
                            {
                                onelayeritem2.majorID = Convert.ToInt64(devicebofang.deviceId);
                                onelayeritem2.name = devicebofang.deviceName;
                                onelayeritem2.type = devicebofang.deviceType;
                            }
                        }

                        LayerActionreqbody onelayerbody2 = new LayerActionreqbody();
                        List<LayerItemelement> onelayeritem = new List<LayerItemelement>();
                        onelayeritem.Add(onelayeritem2);

                        onelayerbody2.alpha = 1.0;
                        onelayerbody2.highlight = false;
                        onelayerbody2.layerID = BuissnesServiceImpl.idlist[0].id;
                        onelayerbody2.layerItem = onelayeritem;

                        string piecexmlhalf = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0\" slavetop=\"0\" slavewidth=\"1\" slaveheight=\"1\" layerleft=\"0\" layertop=\"0\" layerwidth=\"1\" layerheight=\"1\" />\n </Layer>\n";
                        string slave0ID = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
                        onelayerbody2.pieceXml = piecexmlhalf.Replace("abc", slave0ID);


                        onelayerbody2.type = "Add";
                        onelayerbody2.zOrder = 10;

                        LayerActionreq onelayerreq2 = new LayerActionreq();
                        onelayerreq2.body = onelayerbody2;

                        string onelayerreqstr = new JavaScriptSerializer().Serialize(onelayerreq2);
                        string newstr = onelayerreqstr.Replace("\"alpha\":1,", "\"alpha\":1.0,");
                        //MessageBox.Show(newstr);
                        //Form1.wb.send(newstr);

                        LayerMiddle lm = new LayerMiddle();
                        lm.minorID = "-1";

                        foreach (DeviceList devicebofang in BuissnesServiceImpl.devicejihe1)
                        {
                            if (devicebofang.deviceName == this.treeView1.SelectedNode.Text)
                            {
                                lm.majorID = devicebofang.deviceId.ToString();
                                lm.name = devicebofang.deviceName;
                                lm.type = devicebofang.deviceType.ToString();
                            }
                        }

                        string totag = new JavaScriptSerializer().Serialize(lm);

                        AddLayer(this.treeView1.SelectedNode.Text,totag);
                        //MessageBox.Show(totag);
                    }
                    else if (this.treeView1.SelectedNode.ImageIndex == 3 || this.treeView1.SelectedNode.ImageIndex == 4 || this.treeView1.SelectedNode.ImageIndex == 6 || this.treeView1.SelectedNode.ImageIndex == 10)            //通过这种方式判定选中的节点是否为设备的源对象
                    {
                        Form1.wb.send(@"{ ""body"" : { ""idCount"" : 2, ""type"" : ""ADDLAYER"" }, ""guid"" : ""M-76"", ""type"" : ""GETUNIQUEIDLIST"" }");  //发送请求获取两个ID，layerID和layeritemID 
                        Thread.Sleep(1000);
                        //MessageBox.Show(Convert.ToString(BuissnesServiceImpl.idlist[0].id)+"----"+ Convert.ToString(BuissnesServiceImpl.idlist[1].id));

                        LayerItemelement onelayeritem3 = new LayerItemelement();
                        onelayeritem3.ID = BuissnesServiceImpl.idlist[1].id;
                        onelayeritem3.description = "";
                        //onelayeritem2.majorID = -1;
                        //onelayeritem2.minorID = -1;
                        //onelayeritem1.name = "圆形按钮桌面图标下载24.png";
                        onelayeritem3.playOrder = 0;
                        onelayeritem3.playTime = 30;
                        onelayeritem3.refreshTime = 1800;
                        //onelayeritem1.type = 43;
                        foreach (DeviceList devicebofang in BuissnesServiceImpl.devicejihe1)
                        {
                            if (devicebofang.sourceinfo != null)
                            {
                                foreach (GetDeviceListrespsourceinfoele devicesource in devicebofang.sourceinfo)
                                {
                                    if (devicesource.sourceName == this.treeView1.SelectedNode.Text)
                                    {
                                        onelayeritem3.majorID = Convert.ToInt64(devicebofang.deviceId);
                                        onelayeritem3.minorID = devicesource.sourceId;
                                        onelayeritem3.name = devicesource.sourceName;
                                        onelayeritem3.type = devicesource.sourceType;
                                    }
                                }
                            }
                        }

                        LayerActionreqbody onelayerbody3 = new LayerActionreqbody();
                        List<LayerItemelement> onelayeritem = new List<LayerItemelement>();
                        onelayeritem.Add(onelayeritem3);

                        onelayerbody3.alpha = 1.0;
                        onelayerbody3.highlight = false;
                        onelayerbody3.layerID = BuissnesServiceImpl.idlist[0].id;
                        onelayerbody3.layerItem = onelayeritem;

                        string piecexmlhalf = "<Layer>\n <Piece slaveid=\"abc\" slaveleft=\"0\" slavetop=\"0\" slavewidth=\"1\" slaveheight=\"1\" layerleft=\"0\" layertop=\"0\" layerwidth=\"0.5\" layerheight=\"0.5\" />\n </Layer>\n";
                        string slave0ID = Convert.ToString(BuissnesServiceImpl.slaveinfoele[0].id);
                        onelayerbody3.pieceXml = piecexmlhalf.Replace("abc", slave0ID);


                        onelayerbody3.type = "Add";
                        onelayerbody3.zOrder = 10;

                        LayerActionreq onelayerreq3 = new LayerActionreq();
                        onelayerreq3.body = onelayerbody3;

                        string onelayerreqstr = new JavaScriptSerializer().Serialize(onelayerreq3);
                        string newstr = onelayerreqstr.Replace("\"alpha\":1,", "\"alpha\":1.0,");
                        //MessageBox.Show(newstr);
                        //Form1.wb.send(newstr);

                        LayerMiddle lm = new LayerMiddle();

                        foreach (DeviceList devicebofang in BuissnesServiceImpl.devicejihe1)
                        {
                            if (devicebofang.sourceinfo != null)
                            {
                                foreach (GetDeviceListrespsourceinfoele devicesource in devicebofang.sourceinfo)
                                {
                                    if (devicesource.sourceName == this.treeView1.SelectedNode.Text)
                                    {
                                        lm.majorID = devicebofang.deviceId.ToString();
                                        lm.minorID = devicesource.sourceId.ToString();
                                        lm.name = devicesource.sourceName;
                                        lm.type = devicesource.sourceType.ToString();
                                    }
                                }
                            }
                        }

                        string totag = new JavaScriptSerializer().Serialize(lm);

                        AddLayer(this.treeView1.SelectedNode.Text,totag);
                        //MessageBox.Show(totag);
                    }
                }


                /*
                MonitorInfoelement monitortemp1 = BuissnesServiceImpl.slaveinfoele[0].monitorInfo[0];

                Button b1 = new Button();
                b1.Text = "测试";
                //b1.Tag = "";
                b1.Size = new Size(monitortemp1.width / suoxiaobili, monitortemp1.height / suoxiaobili);
                b1.Location = new Point(pianyi, pianyi);
                b1.ContextMenuStrip = contextMenuStrip1;

                b1.LocationChanged += new System.EventHandler(B1_LocationChanged);
                b1.SizeChanged += new System.EventHandler(B1_SizeChanged);
                b1.Click += new System.EventHandler(B1_Click);
                b1.Resize += new System.EventHandler(B1_Resize);

                pb.WireControl(b1);

                this.tabControl1.SelectedTab.Controls.Add(b1);

                b1.BringToFront();
                */
            }           
        }

        private void B1_Resize(object sender, EventArgs e)
        {
            Button bs = (Button)sender;
            LayerChange(bs);
        }

        private void B1_Click(object sender, EventArgs e)
        {
            Button bs = (Button)sender;
            LayerChange(bs);
        }

        private void B1_LocationChanged(object sender, EventArgs e)
        {
            Button bs = (Button)sender;
            LayerChange(bs);
        }

        private void B1_SizeChanged(object sender, EventArgs e)
        {
            Button bs = (Button)sender;
            LayerChange(bs);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void LayerChange(Button bb)
        {
            textBox2.Text = ((bb.Location.X - pianyi) * suoxiaobili).ToString();
            textBox3.Text = ((bb.Location.Y - pianyi) * suoxiaobili).ToString();
            textBox4.Text = (bb.Size.Width * suoxiaobili).ToString();
            textBox5.Text = (bb.Size.Height * suoxiaobili).ToString();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control br in this.tabControl1.SelectedTab.Controls)
                {
                    if (br is Button)
                    {
                        pb.Remove();
                        this.tabControl1.SelectedTab.Controls.Remove(br);
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                    }
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.Message);
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                int x, y, w, h;
                x = Convert.ToInt32(textBox2.Text) / suoxiaobili;
                y = Convert.ToInt32(textBox3.Text) / suoxiaobili;
                w = Convert.ToInt32(textBox4.Text) / suoxiaobili;
                h = Convert.ToInt32(textBox5.Text) / suoxiaobili;

                foreach (Control bc in this.tabControl1.SelectedTab.Controls)
                {
                    if (bc is Button)
                    {
                        bc.Location = new Point(x + pianyi, y + pianyi);
                        bc.Size = new Size(w, h);
                    }
                }
            }
        }

        private void AddLayer(string layertext, string layermiddle)
        {
            MonitorInfoelement monitortemp1 = BuissnesServiceImpl.slaveinfoele[0].monitorInfo[0];

            Button b1 = new Button();
            //b1.Text = "测试";
            b1.Text = layertext;
            b1.Tag = layermiddle;
            b1.Size = new Size(monitortemp1.width / suoxiaobili, monitortemp1.height / suoxiaobili);
            b1.Location = new Point(pianyi, pianyi);
            b1.ContextMenuStrip = contextMenuStrip1;

            b1.LocationChanged += new System.EventHandler(B1_LocationChanged);
            b1.SizeChanged += new System.EventHandler(B1_SizeChanged);
            b1.Click += new System.EventHandler(B1_Click);
            b1.Resize += new System.EventHandler(B1_Resize);

            pb.WireControl(b1);

            this.tabControl1.SelectedTab.Controls.Add(b1);

            b1.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Global.MainForm.cityinfonick.Clear();
            foreach(TabPage tp in this.tabControl1.Controls)
            {
                foreach(Control badd in tp.Controls)
                {
                    if (badd is Button)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        LayerMiddle lm = js.Deserialize<LayerMiddle>(badd.Tag.ToString());

                        CityName newcity = new CityName();
                        newcity.cityname = tp.Text;
                        newcity.majorID = lm.majorID;
                        newcity.minorID = lm.minorID;
                        newcity.name = lm.name;
                        newcity.type = lm.type;

                        newcity.slaveleft = FourDivision(badd.Location.X - pianyi, totalx);
                        newcity.slavetop = FourDivision(badd.Location.Y - pianyi, totaly);
                        newcity.slavewidth = FourDivision(badd.Size.Width, totalx);
                        newcity.slaveheight = FourDivision(badd.Size.Height, totaly);

                        Global.MainForm.cityinfonick.Add(newcity);
                        //MessageBox.Show(newcity.slaveleft);
                    }
                }
            }

            Global.MainForm.gbci.cityinfo = Global.MainForm.cityinfonick;

            string output = new JavaScriptSerializer().Serialize(Global.MainForm.gbci);

            FileStream fs = new FileStream(@"cityinfonick.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(output);
            sw.Close();
            fs.Close();
            MessageBox.Show(output + "----保存成功!");

            //MessageBox.Show(output);
        }
        private string FourDivision(int x,int total)
        {
            Double a = x * suoxiaobili;
            Double b = Math.Round(a / total, 4);
            return b.ToString();
        }
        private int FourMultiplication(string s,int total)
        {
            Double a = Convert.ToDouble(s) * total;
            int b = Convert.ToInt32(a / suoxiaobili);
            return b;
        }
    }
}
