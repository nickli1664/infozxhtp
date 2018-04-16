using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Timers;

namespace zxhtuopan1
{
    public class Cd
    {
        public string cdID;
        public string cdName;

        public Cd(string x, string y)
        {
            cdID = x;
            cdName = y;
        }
    }
    public class FolderInfo
    {
        public int pId;
        public string fName;
        public int folderId;
        public bool hasChild;
        public bool hasMedia;
        public int treeViewIndex;
        public bool add = false;
        public bool test = false;
        public FolderInfo(int x,string y,int z,bool b1,bool b2)
        {
            pId = x;
            fName = y;
            folderId = z;
            hasChild = b1;
            hasMedia = b2;
        }
    }
    public class MediaList
    {
        public int fileId;
        public string fileName;
        public int pId;
        public bool add = false;
        public int type;
        public MediaList(int x,string y,int z,int t)
        {
            fileId = x;
            fileName = y;
            pId = z;
            type = t;
        }
    }
    public class DeviceFolderele
    {
        public string deviceFolderName;
        public UInt32 folderID;
        public bool hasChild;
        public bool hasDevice;
        public UInt32 parentID;
        public UInt32 type;
        public int treeViewIndex;
        public bool add = false;
        public bool test = false;
        public DeviceFolderele(string x,UInt32 y,bool b1,bool b2,UInt32 z,UInt32 t)
        {
            deviceFolderName = x;
            folderID = y;
            hasChild = b1;
            hasDevice = b2;
            parentID = z;
            type = t;
        }
    }
    public class DeviceList
    {
        public Int32 devicepId;
        public Int32 deviceId;
        public string deviceName;
        public bool add = false;
        public int deviceType;
        public List<GetDeviceListrespsourceinfoele> sourceinfo;
        public DeviceList (Int32 x,Int32 y,string dN,int z, List<GetDeviceListrespsourceinfoele> si)
        {
            devicepId = x;
            deviceId = y;
            deviceName = dN;
            deviceType = z;
            sourceinfo = si;
        }
    }

    public class BuissnesServiceImpl:WebSocketService
    {

        //public static List<string> cdID = new List<string>();
        //public static List<string> cdName = new List<string>();

        public delegate void MyInvoke(string str);
        private System.Timers.Timer labelTimer = new System.Timers.Timer();

        public static List<Cd> cdjihe1 = new List<Cd>();
        public static List<FolderInfo> folderjihe1 = new List<FolderInfo>();
        public static List<MediaList> mediajihe1 = new List<MediaList>();
        public static List<SlaveInfoelement> slaveinfoele;
        public static List<GetUniqueIDListrespidList> idlist;
        public static List<DeviceFolderele> devicefolderjihe1 = new List<DeviceFolderele>();
        public static List<DeviceList> devicejihe1 = new List<DeviceList>();
        public static List<GetDeviceListrespsourceinfoele> devicesourceinfojihe1 = new List<GetDeviceListrespsourceinfoele>();
        public string passflag;
        static int scss = 5;
        static string scstr;

        public static GetMediaFolderInforespbody mediafolderinfobody;
        public static GetDeviceFolderInforespbody devicefolderinfobody;

        public void onReceive(string msg) {
            string s1 = "SEARCHPROGRAMBASICINFO";
            //string s2 = "QUERYUSERLOGIN";
            string s3 = "\"loginSuccess\" : true";
            string s4 = "\"errorStr\" : \"Query User Login Error\",";
            string s4new1 = "\"loginSuccess\" : false";                      //update 2017/12/13 我感觉3.0.26的接口返回值有修改，有点坑。
            //string s5 = "\"errorStr\" : \"No access privilege\",";
            string s0 = "\"errorStr\" : \"OK\",";
            string s24 = "GETMEDIAFOLDERINFO";
            string s36 = "GETCHILDMEDIAFOLDERLIST";
            string s42 = "GETMEDIAFILELIST";
            string s44 = "LOADVIDEOWALLINFO";
            string s76 = "GETUNIQUEIDLIST";
            string s121 = "GETDEVICEFOLDERINFO";
            string s117 = "GETDEVICETYPEFOLDERLIST";
            string s134 = "GETDEVICELIST";

            /*
            if (msg.Contains(s2))
            {
                MessageBox.Show(msg);
            }
            */

            if (msg.Contains(s4) || msg.Contains(s4new1))
            {
                passflag = "loginfail";
            }
            if (msg.Contains(s3))
            {
                passflag = "loginwin";
            }
            if (msg.Contains(s1) && msg.Contains(s0))
            {
                /*
                string[] jsonThreeDivide = msg.Split(new char[2] { '[', ']' },StringSplitOptions.RemoveEmptyEntries);
                string jsonZhongKuoHao = jsonThreeDivide[1];
                Console.WriteLine(jsonZhongKuoHao);

                int countmoshi = (jsonZhongKuoHao.Length - jsonZhongKuoHao.Replace("\"ID\"", "").Length) / "\"ID\"".Length;
                Console.WriteLine("{0}", countmoshi);
                */

                //Console.WriteLine(s1);
                JavaScriptSerializer js = new JavaScriptSerializer();   //实例化一个能够序列化数据的类
                Totalsearchprores totallist = js.Deserialize<Totalsearchprores>(msg); //将json数据转化为对象类型并赋值给list
                List<MoShisearchprores> result = totallist.body.basicInfo;
                string guid = totallist.guid;
                string type = totallist.type;
                //Console.WriteLine(guid);
                //Console.WriteLine(type);

                //int i = 0;

                if (result != null)
                {
                    foreach (MoShisearchprores result1 in result)
                    {
                        //MessageBox.Show("模式ID：" + result1.ID.ToString()+"，描述："+result1.description+"，模式名称："+result1.name+"，持续时间："+result1.playtime.ToString());
                        //cdID.Add(result1.ID.ToString());
                        //cdName.Add(result1.name);
                        Cd cd1 = new Cd(result1.ID.ToString(), result1.name);
                        cdjihe1.Add(cd1);

                        /*MessageBox.Show(cdjihe1[i].cdID+"-------------"+cdjihe1[i].cdName);
                        i++;
                        */
                    }
                    if (cdjihe1 != null)
                    {
                        //MyInvoke mi = new MyInvoke(SetTxt);
                        labelTimer.Interval = 1000;
                        labelTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
                        labelTimer.Start();
                        //Global.MainForm.BeginInvoke(mi, new object[] { "成功载入模式" + scstr });
                    }
                }
            }
            if (msg.Contains(s0) && msg.Contains(s24))                //获取媒体文件夹返回消息的信息
            {
                JavaScriptSerializer js24 = new JavaScriptSerializer();
                GetMediaFolderInforesp mediafolderinfo = js24.Deserialize<GetMediaFolderInforesp>(msg);
                mediafolderinfobody = mediafolderinfo.body;
                /*
                if (mediafolderinfobody.hasChild == true)            //如果有子文件夹，则试图获取子文件夹
                {
                    MessageBox.Show("文件夹"+mediafolderinfobody.folderName+"还有子项（经check此子项是指子文件夹）");

                    GetChildMediaFolderListreqbody childmediafolderlistreqbody = new GetChildMediaFolderListreqbody();    //生成获取子文件夹的请求并发送
                    childmediafolderlistreqbody.parentID = mediafolderinfobody.folderID;
                    GetChildMediaFolderListreq childmediafolderlistreq = new GetChildMediaFolderListreq();
                    childmediafolderlistreq.body = childmediafolderlistreqbody;
                    string sGetChildMediaFolderListreq = new JavaScriptSerializer().Serialize(childmediafolderlistreq);
                    Form1.wb.send(sGetChildMediaFolderListreq);

                    if (msg.Contains(s0) && msg.Contains(s36))       //获取子文件夹返回消息的信息
                    {
                        JavaScriptSerializer js36 = new JavaScriptSerializer();
                        GetChildMediaFolderListresp childmediafolderlist = js36.Deserialize<GetChildMediaFolderListresp>(msg);
                        GetChildMediaFolderListrespbody childmediafolderlistbody = childmediafolderlist.body;
                        List<GetChildMediaFolderListrespfolderinfo> childfolderinfo = childmediafolderlistbody.folderInfo;

                        foreach (GetChildMediaFolderListrespfolderinfo childfolderinfo1 in childfolderinfo)
                        {
                            MessageBox.Show(childfolderinfo1.folderName);
                        }

                    }
                    
                }
                else if (mediafolderinfobody.hasChild == false)
                {
                    MessageBox.Show("文件夹" + mediafolderinfobody.folderName + "没有子项（经check此子项是指子文件夹）！！");
                }
                */
            }
            if (msg.Contains(s0) && msg.Contains(s36))       //获取子文件夹返回消息的信息
            {
                JavaScriptSerializer js36 = new JavaScriptSerializer();
                GetChildMediaFolderListresp childmediafolderlist = js36.Deserialize<GetChildMediaFolderListresp>(msg);
                GetChildMediaFolderListrespbody childmediafolderlistbody = childmediafolderlist.body;
                List<GetChildMediaFolderListrespfolderinfo> childfolderinfo = childmediafolderlistbody.folderInfo;

                if (childfolderinfo != null)
                {
                    foreach (GetChildMediaFolderListrespfolderinfo childfolderinfo1 in childfolderinfo)
                    {
                        //MessageBox.Show(childfolderinfo1.folderName);
                        //TreeNode n = new TreeNode(childfolderinfo1.folderName);
                        //Formselect.frmxsq.treeView2.Nodes[0].Nodes.Add(n);
                        FolderInfo childfolder = new FolderInfo(childmediafolderlistbody.parentFolderId, childfolderinfo1.folderName,childfolderinfo1.folderID,childfolderinfo1.hasChild,childfolderinfo1.hasMedia);
                        folderjihe1.Add(childfolder);
                    }
                }
            }
            if (msg.Contains(s0) && msg.Contains(s42))
            {
                JavaScriptSerializer js42 = new JavaScriptSerializer();
                GetMediaFileListresp medialist = js42.Deserialize<GetMediaFileListresp>(msg);
                GetMediaFileListrespbody medialistbody = medialist.body;
                List<GetMediaFileListrespinfolist> medialistinfolist = medialistbody.infolist;

                if (medialistinfolist != null)
                {
                    foreach (GetMediaFileListrespinfolist medialistinfolist1 in medialistinfolist)
                    {
                        MediaList media = new MediaList(medialistinfolist1.fileId,medialistinfolist1.fileName,medialistinfolist1.folderId,medialistinfolist1.type);
                        mediajihe1.Add(media);
                    }
                }
            }
            if (msg.Contains(s0) && msg.Contains(s44))
            {
                JavaScriptSerializer js44 = new JavaScriptSerializer();
                LoadVideoWallInforesp wallinfo = js44.Deserialize<LoadVideoWallInforesp>(msg);
                SlaveInfo slaveinfos = wallinfo.body;
                slaveinfoele = slaveinfos.slaveInfo;

                /*
                foreach (SlaveInfoelement slaveinfoele1 in slaveinfoele)
                {
                    Console.WriteLine("slaveID: {0}", slaveinfoele1.id);
                }
                */
            }
            if (msg.Contains(s0) && msg.Contains(s76))
            {
                JavaScriptSerializer js76 = new JavaScriptSerializer();
                GetUniqueIDListresp uniqueidlistresp1 = js76.Deserialize<GetUniqueIDListresp>(msg);
                GetUniqueIDListrespbody uniqueidlistbody1 = uniqueidlistresp1.body;
                idlist = uniqueidlistbody1.idList;
            }
            if (msg.Contains(s0) && msg.Contains(s121))
            {
                JavaScriptSerializer js121 = new JavaScriptSerializer();
                GetDeviceFolderInforesp devicefolderinfo = js121.Deserialize<GetDeviceFolderInforesp>(msg);
                devicefolderinfobody = devicefolderinfo.body;
            }
            if (msg.Contains(s0) && msg.Contains(s117))
            {
                JavaScriptSerializer js117 = new JavaScriptSerializer();
                GetDeviceTypeFolderListresp childdevicetypefolderlist = js117.Deserialize<GetDeviceTypeFolderListresp>(msg);
                GetDeviceTypeFolderListrespbody childdevicetypefolderlistbody = childdevicetypefolderlist.body;
                List<GetDeviceTypeFolderListrespele> childdevicetypefolderlistele = childdevicetypefolderlistbody.deviceFolderInfo;

                if (childdevicetypefolderlistele != null)
                {
                    foreach (GetDeviceTypeFolderListrespele childdevicetypefolderlistele1 in childdevicetypefolderlistele)
                    {
                        DeviceFolderele childdevicefolder = new DeviceFolderele(childdevicetypefolderlistele1.Name,childdevicetypefolderlistele1.folderID,childdevicetypefolderlistele1.hasChild,childdevicetypefolderlistele1.hasDevice,childdevicetypefolderlistele1.parentID,childdevicetypefolderlistele1.type);
                        devicefolderjihe1.Add(childdevicefolder);
                    }
                }
            }
            if (msg.Contains(s0) && msg.Contains(s134))
            {
                JavaScriptSerializer js134 = new JavaScriptSerializer();
                GetDeviceListresp devicelist = js134.Deserialize<GetDeviceListresp>(msg);
                GetDeviceListrespbody devicelistbody = devicelist.body;
                List<GetDeviceListrespinfoListele> devicelistinfolistele = devicelistbody.infoList;

                if (devicelistinfolistele != null)
                {
                    foreach (GetDeviceListrespinfoListele devicelistinfolistele1 in devicelistinfolistele)
                    {
                        GetDeviceListrespsourceinfo devicelistrespsourceinfo1 = devicelistinfolistele1.sourceinfolist;
                        List<GetDeviceListrespsourceinfoele> sourceinfo1 = devicelistrespsourceinfo1.sourceinfo;

                        if (sourceinfo1 != null)
                        {
                            foreach (GetDeviceListrespsourceinfoele sourceinfo2 in sourceinfo1)
                            {
                                //GetDeviceListrespsourceinfoele sourceinfotemp = new GetDeviceListrespsourceinfoele(sourceinfo2.URL,sourceinfo2.description,sourceinfo2.height,sourceinfo2.sourceId,sourceinfo2.sourceName,sourceinfo2.sourceType,sourceinfo2.width);
                                devicesourceinfojihe1.Add(sourceinfo2);
                            }
                        }

                        DeviceList device = new DeviceList(devicelistinfolistele1.deviceFolderId,devicelistinfolistele1.deviceId,devicelistinfolistele1.deviceName,devicelistinfolistele1.deviceType,sourceinfo1);
                        devicesourceinfojihe1.Clear();
                        devicejihe1.Add(device);
                    }
                }
            }

        }
        public void SetTxt(string str)
        {
            Global.MainForm.label1.Text = str;
        }

        private void OnTimedEvent2(object source, ElapsedEventArgs e)
        {
            MyInvoke mi = new MyInvoke(SetTxt);
            if (scss>0)
            {
                scss--;
                scstr = scss.ToString();
                Global.MainForm.BeginInvoke(mi, new object[] { "成功载入模式\n本提示"+ scstr+"秒后消失" });
            }
            else
            {
                Global.MainForm.BeginInvoke(mi, new object[] { ""});
                labelTimer.Stop();
            }
        }
    }
}
