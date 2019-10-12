using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class Ship
    {
        public int ShipId { get; set; }
        public string Name { get; set; }
        public int RemainingTiles { get; set; }

        public int ShipTypeId { get; set; }
        public int CellId { get; set; }
    }
}
