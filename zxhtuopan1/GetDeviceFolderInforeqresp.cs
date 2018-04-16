using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
    public class GetDeviceFolderInforeqbody
    {
        public UInt32 typeid { get; set; }
    }
    public class GetDeviceFolderInforeq
    {
        public GetDeviceFolderInforeqbody body { get; set; }
        public string guid { get { return "M-121"; } }
        public string type { get { return "GETDEVICEFOLDERINFO"; } }
    }
    public class GetDeviceFolderInforespbody
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public bool bDisplay { get; set; }
        public UInt32 folderID { get; set; }
        public bool hasChild { get; set; }
        public UInt32 parentID { get; set; }
        public bool readOnly { get; set; }
        public UInt32 type { get; set; }
    }
    public class GetDeviceFolderInforesp
    {
        public GetDeviceFolderInforespbody body { get; set; }
        public int category { get { return 0; } }
        public int errorCode { get { return 0; } }
        public string errorStr { get { return "OK"; } }
        public string guid { get { return "M-121"; } }
        public string type { get { return "GETDEVICEFOLDERINFO"; } }
    }
}
