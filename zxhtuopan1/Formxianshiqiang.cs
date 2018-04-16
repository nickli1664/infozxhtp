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
using System.Threading;

namespace zxhtuopan1
{
    public partial class Formxianshiqiang : Form
    {
        private System.Windows.Forms.Timer actimer = new System.Windows.Forms.Timer();
        //private bool acflag = true;

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
        public Formxianshiqiang()
        {
            InitializeComponent();
        }

        private void Formxianshiqiang_Load(object sender, EventArgs e)
        {
            this.actimer.Enabled = true;
            this.actimer.Interval = 8000;
            this.actimer.Tick += new EventHandler(Actimer_Tick);
            this.actimer.Start();

            Form1.wb.send(@" { ""body"" : """", ""guid"" : ""M-44"", ""type"" : ""LOADVIDEOWALLINFO"" }");

            NewTabControl ntc1 = new NewTabControl();
            ntc1.Location = new Point(556, 28);
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

            //int i = 1;

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
                    TreeNode newdevicefolder = new TreeNode(dfe.deviceFolderName,2,2);
                    newdevicefolder.Name = Convert.ToString(dfe.folderID);
                    this.treeView1.Nodes[(Convert.ToInt16(dfe.parentID) - 1)].Nodes.Add(newdevicefolder);
                    dfe.treeViewIndex = newdevicefolder.Index;
                }
            }
            foreach (DeviceList devicel in BuissnesServiceImpl.devicejihe1)
            {
                if (devicel.devicepId >0 && devicel.devicepId < 11)
                {
                    switch (devicel.deviceType)
                    {
                        case 1:
                            TreeNode newcontroller = new TreeNode(devicel.deviceName,5,5);
                            newcontroller.Name = Convert.ToString(devicel.deviceId);
                            this.treeView1.Nodes[Convert.ToInt16(devicel.devicepId) - 1].Nodes.Add(newcontroller);
                            if (devicel.sourceinfo != null)
                            {
                                for (int i1 = 0; i1 < devicel.sourceinfo.Count;i1++)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.wb.send(@"{ ""body"" : """", ""guid"" : ""M-181"", ""type"" : ""STOPEXHIBITION"" }");  //显示墙，关闭实时控制状态
            BuissnesServiceImpl.folderjihe1.Clear();
            BuissnesServiceImpl.mediajihe1.Clear();

            BuissnesServiceImpl.devicefolderjihe1.Clear();
            BuissnesServiceImpl.devicejihe1.Clear();
            /*
            foreach (FolderInfo fi in BuissnesServiceImpl.folderjihe1)
            {
                this.treeView2.Nodes[(fi.pId - 11)].Nodes.Clear();
            }
            */
            this.actimer.Stop();
            //this.Hide();
            /*
            for (int id4confirm = 11; id4confirm < 15; id4confirm++)
            {
                this.treeView2.Nodes[0] = treeView2.SelectedNode;
                this.treeView2.Nodes.RemoveByKey();
            }
            
            foreach(FolderInfo fi in BuissnesServiceImpl.folderjihe1)
            {
                this.treeView2.Nodes.RemoveByKey(fi.fName);
            }
            */

            this.Close();
            this.Dispose();

            //Global.MainForm.Show();
            //Global.MainForm.actimer.Start();

            GC.Collect();

            Formselect frms = new Formselect();
            frms.ShowDialog();
        }

        private void treeView2_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeView2.SelectedNode = e.Node;
            //MessageBox.Show(Convert.ToString(treeView2.SelectedNode.Text));
            //int temp = treeView2.SelectedNode.Index + 11;

            foreach (FolderInfo fi in BuissnesServiceImpl.folderjihe1.ToArray())                    //存疑，一般的写法有可能引发集合被修改的异常
            {
                if ( (fi.pId == Convert.ToInt16(e.Node.Name))&& fi.test == false)
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
                            if (fi2.pId > 15 && fi2.add ==false)
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

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.treeView2.SelectedNode.Name);
            /*
            foreach (FolderInfo test1 in BuissnesServiceImpl.folderjihe1)
            {
                MessageBox.Show(test1.fName + "----" + test1.pId + "----"+ test1.treeViewIndex);
            }
            */
            if (button2.ImageIndex == 0)
            {
                Form1.wb.send(@"{ ""body"" : """", ""guid"" : ""M-212"", ""type"" : ""STARTEXHIBITION"" }");  //显示墙，开启实时控制状态
                button2.ImageIndex = 1;
                this.button3.Enabled = true;
                this.button4.Enabled = true;
            }
            else
            {
                Form1.wb.send(@"{ ""body"" : """", ""guid"" : ""M-181"", ""type"" : ""STOPEXHIBITION"" }");  //显示墙，关闭实时控制状态
                button2.ImageIndex = 0;
                this.button3.Enabled = false;
                this.button4.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
                    Form1.wb.send(newstr);
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
                    Form1.wb.send(newstr);
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
                    Form1.wb.send(newstr);
                }
            }
        }

        private void Formxianshiqiang_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
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
                                TreeNode newdevicefolder = new TreeNode(dfe3.deviceFolderName,2,2);
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (button2.ImageIndex == 1)
            {
                Form1.wb.send(@" { ""body"" : """", ""guid"" : ""M-213"", ""type"" : ""CLEARSCREENCONTENT"" }");
            }
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
        private void ntc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("123");
            treeView1.SelectedNode = null;
            treeView2.SelectedNode = null;
        }
    }
}
