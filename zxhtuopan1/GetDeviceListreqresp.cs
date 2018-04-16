using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
    public class GetDeviceListreqbody
    {
        public UInt32 id { get; set; }
        public string keyword { get { return ""; } }
    }
    public class GetDeviceListreq
    {
        public GetDeviceListreqbody body { get; set; }
        public string guid { get { return "M-134"; } }
        public string type { get { return "GETDEVICELIST"; } }
    }
    public class GetDeviceListrespsourceinfoele
    {
        public string URL { get; set; }
        public string description { get; set; }
        public UInt32 height { get; set; }
        public Int32 sourceId { get; set; }
        public string sourceName { get; set; }
        public int sourceType { get; set; }
        public UInt32 width { get; set; }
    }
    public class GetDeviceListrespsourceinfo
    {
        public List<GetDeviceListrespsourceinfoele> sourceinfo { get; set; }
    }
    public class GetDeviceListrespinfoListele
    {
        public string URL { get; set; }
        public string addressIp { get; set; }
        public Int32 controlStreamingServerId { get; set; }
        public string description { get; set; }
        public string deviceExtraInfo { get; set; }
        public Int32 deviceFolderId { get; set; }
        public Int32 deviceId { get; set; }
        public string deviceName { get; set; }
        public int deviceStatus { get; set; }
        public string deviceStatusProto { get; set; }
        public int deviceType { get; set; }
        public string password { get; set; }
        public Int32 port { get; set; }
        public int protoType { get; set; }
        public string screenInfoProto { get; set; }
        public GetDeviceListrespsourceinfo sourceinfolist { get; set; }
        public string userName { get; set; }
    }
    public class GetDeviceListrespbody
    {
        public List<GetDeviceListrespinfoListele> infoList { get; set; }
    }
    public class GetDeviceListresp
    {
        public GetDeviceListrespbody body { get; set; }
        public int category { get { return 0; } }
        public int errorCode { get { return 0; } }
        public string errorStr { get { return "OK"; } }
        public string guid { get { return "M-134"; } }
        public string type { get { return "GETDEVICELIST"; } }
    }
}
