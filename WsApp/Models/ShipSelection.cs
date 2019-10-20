using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class ShipSelection
    {
        public int ShipSelectionId { get; set; }
        public int Count { get; set; }
        public int Size { get; set; }
        public string ButtonId { get; set; }
        public bool IsSelected { get; set; }

        public Player Player { get; set; }
        public int ShipTypeId { get; set; }
    }
}
