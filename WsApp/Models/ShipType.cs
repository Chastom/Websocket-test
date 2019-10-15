using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class ShipType
    {
        public int ShipTypeId { get; set; }
        public string Type { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }

        public int ShipId { get; set; }
    }

}
