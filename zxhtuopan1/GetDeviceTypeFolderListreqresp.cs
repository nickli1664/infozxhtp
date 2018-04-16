using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
    public class GetDeviceTypeFolderListreqbody
    {
        public UInt32 typeid { get; set; }
    }
    public class GetDeviceTypeFolderListreq
    {
        public GetDeviceTypeFolderListreqbody body { get; set; }
        public string guid { get { return "M-117"; } }
        public string type { get { return "GETDEVICETYPEFOLDERLIST"; } }
    }
    public class GetDeviceTypeFolderListrespele
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public bool bDisplay { get; set; }
        public UInt32 folderID { get; set; }
        public bool hasChild { get; set; }
        public bool hasDevice { get; set; }
        public UInt32 parentID { get; set; }
        public bool readOnly { get; set; }
        public UInt32 type { get; set; }
    }
    public class GetDeviceTypeFolderListrespbody
    {
        public List<GetDeviceTypeFolderListrespele> deviceFolderInfo { get; set; }
        public UInt32 parentFolderId { get; set; }
    }
    public class GetDeviceTypeFolderListresp
    {
        public GetDeviceTypeFolderListrespbody body { get; set; }
        public int category { get { return 0; } }
        public int errorCode { get { return 0; } }
        public string errorStr { get { return "OK"; } }
        public string guid { get { return "M-117"; } }
        public string type { get { return "GETDEVICETYPEFOLDERLIST"; } }
    }
}
