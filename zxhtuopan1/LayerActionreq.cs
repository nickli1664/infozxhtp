using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
    public class LayerItemelement
    {
        public UInt64 ID { get; set; }
        public string description { get; set; }
        public Int64 majorID { get; set; }
        public Int64 minorID { get; set; }
        public string name { get; set; }
        public int playOrder { get; set; }
        public UInt64 playTime { get; set; }
        public UInt64 refreshTime { get; set; }
        public int type { get; set; }
        public int validSource { get { return 1; } }
    }
    public class LayerActionreqbody
    {
        public double alpha { get; set; }
        public bool highlight { get; set; }
        public UInt64 layerID { get; set; }
        public List<LayerItemelement> layerItem { get; set; }
        public string pieceXml { get; set; }
        public string type { get; set; }
        public int zOrder { get; set; }
    }
    public class LayerActionreq
    {
        public LayerActionreqbody body { get; set; }
        public string guid { get { return "M-45"; } }
        public string type { get { return "LAYERACTION"; } }
    }
}
