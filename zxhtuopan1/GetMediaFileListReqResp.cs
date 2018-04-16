using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
    public class GetMediaFileListreqbody
    {
        public int id { get; set; }
        public string keyword { get { return ""; } }
    }
    public class GetMediaFileListreq
    {
        public GetMediaFileListreqbody body { get; set; }
        public string guid { get { return "M-42"; } }
        public string type { get { return "GETMEDIAFILELIST"; } }
    }
    public class GetMediaFileListrespinfolist
    {
        public string InLocalPath { get; set; }
        public string InServerPath { get; set; }
        public bool bHasThumbnailPix { get; set; }
        public string description { get; set; }
        public string durationtime { get; set; }
        public int fileId { get; set; }
        public string fileName { get; set; }
        public int folderId { get; set; }
        public string hasInfo { get; set; }
        public int height { get; set; }
        public double percent { get; set; }
        public UInt64 size { get; set; }
        public int type { get; set; }
        public string uploadtime { get; set; }
        public int width { get; set; }
    }
    public class GetMediaFileListrespbody
    {
        public int folderId { get; set; }
        public List<GetMediaFileListrespinfolist> infolist { get; set; }
    }
    public class GetMediaFileListresp
    {
        public GetMediaFileListrespbody body { get; set; }
        public int category { get { return 0; } }
        public int errorCode { get { return 0; } }
        public string errorStr { get { return "OK"; } }
        public string guid { get { return "M-42"; } }
        public string type { get { return "GETMEDIAFILELIST"; } }
    }
}
