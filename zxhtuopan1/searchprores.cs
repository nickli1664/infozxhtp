using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
        public class MoShisearchprores
        {
            public int ID { get; set; }
            public string description { get; set; }
            public string name { get; set; }
            public double playtime { get; set; }
        }

        public class MoShiListsearchprores
        {
            public List<MoShisearchprores> basicInfo { get; set; }
        }
        public class Totalsearchprores
        {
            public MoShiListsearchprores body { get; set; }
            public int category { get { return 0; } }
            public int errorCode { get { return 0; } }
            public string errorStr { get { return "OK"; } }
            public string guid { get { return "M-18"; } }
            public string type { get { return "SEARCHPROGRAMBASICINFO"; } }
        }
}
