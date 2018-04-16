using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocketSharp;
using System.Threading;
using System.IO;
using System.Timers;
using System.Web.Script.Serialization;


namespace websocket
{
    class Example
    {
        static string url = "wss://127.0.0.1:7681";

        static BuissnesServiceImpl abc = new BuissnesServiceImpl();
        static WebSocketService wss = abc;
        static WebSocketBase wb = new WebSocketBase(url, wss);
        public static void Main(string[] args)
        {
            //StreamWriter sw = new StreamWriter(@"D:\text1.txt");
            //Console.SetOut(sw);

            System.Timers.Timer aTimer = new System.Timers.Timer();

            
            wb.start();

            wb.send("{\"body\" : {\"userName\" : \"nickli\",\"userPassword\" : \"123456\"},\"guid\" : \"M-0\",\"type\" : \"QUERYUSERLOGIN\"}");

            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 2000;
            aTimer.Enabled = true;
            aTimer.AutoReset = false;

            while (true)
            {
                MoShiplayproreq ppreq1 = new MoShiplayproreq();
                ppreq1.id = Convert.ToInt32(Console.ReadLine());
                Totalplayproreq ppreq2 = new Totalplayproreq();
                ppreq2.body = ppreq1;

                if (ppreq1.id == 0)
                {
                    break;
                }
                else
                {
                    string s1 = new JavaScriptSerializer().Serialize(ppreq2);
                    Console.WriteLine(s1);
                    wb.send(s1);
                    Console.WriteLine("\n请输入您想要切换的模式的模式ID:\n输入0退出程序");
                }
            }
            //发生断开重连时，需要重新订阅
            //while (true) { 
            //    if(wb.isReconnect()){
            //        wb.send("{'event':'addChannel','channel':'ok_sub_spotcny_btc_ticker'}");
            //    }
            //    Thread.Sleep(1000);
            //}

            //Console.ReadKey();
            wb.stop();  //优雅的关闭，程序退出时需要关闭WebSocket连接
         }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            wb.send("{\"body\" : \"\",\"guid\" : \"M-18\",\"type\" : \"SEARCHPROGRAMBASICINFO\"}");
        }
    }
}
