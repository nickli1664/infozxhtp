using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
    public class MonitorInfoelement
    {
        public int height { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
    }
    public class SlaveInfoelement
    {
        public UInt64 captureCount { get; set; }
        public string flag { get; set; }
        public int id { get; set; }
        public string ip { get; set; }
        public int left { get; set; }
        public string mac { get; set; }
        public List<MonitorInfoelement> monitorInfo { get; set; }
        public bool online { get; set; }
        public int top { get; set; }
    }
    public class SlaveInfo
    {
        public List<SlaveInfoelement> slaveInfo { get; set; }
    }
    public class LoadVideoWallInforesp
    {
        public SlaveInfo body { get; set; }
        public int category { get { return 0; } }
        public int errorCode { get { return 0; } }
        public string errorStr { get { return "OK"; } }
        public string guid { get { return "M-44"; } }
        public string type { get { return "LOADVIDEOWALLINFO"; } }
    }
}
