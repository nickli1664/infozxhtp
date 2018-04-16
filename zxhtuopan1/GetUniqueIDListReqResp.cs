using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
    public class GetUniqueIDListreqbody
    {
        public int idCount { get; set; }
        public string type { get { return "ADDLAYER"; } }
    }
    public class GetUniqueIDListreq
    {
        public GetUniqueIDListreqbody body { get; set; }
        public string guid { get { return "M-76"; } }
        public string type { get { return "GETUNIQUEIDLIST"; } }
    }
    public class GetUniqueIDListrespidList
    {
        public UInt64 id { get; set; }
    }
    public class GetUniqueIDListrespbody
    {
        public List<GetUniqueIDListrespidList> idList { get; set; }
        public string type { get { return "ADDLAYER"; } }
    }
    public class GetUniqueIDListresp
    {
        public GetUniqueIDListrespbody body { get; set; }
        public int category { get { return 0; } }
        public int errorCode { get { return 0; } }
        public string errorStr { get { return "OK"; } }
        public string guid { get { return "M-76"; } }
        public string type { get { return "GETUNIQUEIDLIST"; } }
    }
}
