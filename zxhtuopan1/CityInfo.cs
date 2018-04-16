using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zxhtuopan1
{
    public class CityName
    {
        public string cityname { get; set; }
        public string slaveleft { get; set; }
        public string slavetop { get; set; }
        public string slavewidth { get; set; }
        public string slaveheight { get; set; }
        public string majorID { get; set; }
        public string minorID { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }
    public class CityInfo
    {
        public List<CityName> cityinfo { get; set; }
    }
    public class LayerMiddle
    {
        public string majorID { get; set; }
        public string minorID { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

}
