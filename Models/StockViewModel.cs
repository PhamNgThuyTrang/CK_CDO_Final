using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Models
{
    public class StockViewModel
    {
        public int ID { get; set; }

        public string MA { get; set; }

        public DateTime NGAY { get; set; }

        public float GIAMOCUA { get; set; }

        public float GIATRAN { get; set; }

        public float GIASAN { get; set; }

        public float GIADONGCUA { get; set; }

        public int KHOILUONG { get; set; }
    }
}
