using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class Coordinates
    {
        public int CoordinatesId { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public int CellId { get; set; }
    }
}
