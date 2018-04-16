using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
    
    public class MoShiplayproreq
    {
        public int id { get; set; }
    }
    public class Totalplayproreq
    {
        public MoShiplayproreq body { get; set; }
        public string guid { get { return "M-79"; } }
        public string type { get { return "PLAYPROGRAM"; } }
    }

}