using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class Cell
    {
        public int CellId { get; set; }
        public bool IsHit { get; set; }
        public bool IsArmored { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int ShipId { get; set; }
        public int BattleArenaId { get; set; }
    }
}
