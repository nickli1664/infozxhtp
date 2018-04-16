using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
    public class GetChildMediaFolderListreqbody
    {
        public int parentID { get; set; }
    }
    public class GetChildMediaFolderListreq
    {
        public GetChildMediaFolderListreqbody body { get; set; }
        public string guid { get { return "M-36"; } }
        public string type { get { return "GETCHILDMEDIAFOLDERLIST"; } }
    }
    public class GetChildMediaFolderListrespfolderinfo
    {
        public string folderDescription { get; set; }
        public int folderID { get; set; }
        public string folderName { get; set; }
        public bool hasChild { get; set; }
        public bool hasMedia { get; set; }
        public int parentID { get; set; }
        public bool readOnly { get; set; }
    }
    public class GetChildMediaFolderListrespbody
    {
        public List<GetChildMediaFolderListrespfolderinfo> folderInfo { get; set; }
        public int parentFolderId { get; set; }
    }
    public class GetChildMediaFolderListresp
    {
        public GetChildMediaFolderListrespbody body { get; set; }
        public int category { get { return 0; } }
        public int errorCode { get { return 0; } }
        public string errorStr { get { return "OK"; } }
        public string guid { get { return "M-36"; } }
        public string type { get { return "GETCHILDMEDIAFOLDERLIST"; } }
    }
}
