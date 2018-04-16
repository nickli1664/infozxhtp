using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
    public class GetMediaFolderInforeqbody
    {
        public int folderID { get; set; }
    }
    public class GetMediaFolderInforeq
    {
        public GetMediaFolderInforeqbody body { get; set; }
        public string guid { get { return "M-24"; } }
        public string type { get { return "GETMEDIAFOLDERINFO"; } }
    }
    public class GetMediaFolderInforespbody
    {
        public string folderDescription { get; set; }
        public int folderID { get; set; }
        public string folderName { get; set; }
        public bool hasChild { get; set; }
        public int parentID { get; set; }
        public bool readOnly { get; set; }
    }
    public class GetMediaFolderInforesp
    {
        public GetMediaFolderInforespbody body { get; set; }
        public int category { get { return 0; } }
        public int errorCode { get { return 0; } }
        public string errorStr { get { return "OK"; } }
        public string guid { get { return "M-24"; } }
        public string type { get { return "GETMEDIAFOLDERINFO"; } }
    }
}
