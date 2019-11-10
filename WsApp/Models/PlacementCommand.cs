using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class PlacementCommand
    {
        public int PlacementCommandId { get; set; }
        public string SocketId { get; set; }
        public bool IsArmor { get; set; }

        //fields for saving ShipSelection state
        public int SelectionShipSelectionId { get; set; }
        public int SelectionCount { get; set; }
        public int SelectionSize { get; set; }
        public string SelectionButtonId { get; set; }
        public bool SelectionIsSelected { get; set; }
        public int SelectionShipTypeId { get; set; }
        public bool SelectionButton0IsRemoved { get; set; }
        public bool SelectionButton1IsRemoved { get; set; }
        public bool SelectionButton2IsRemoved { get; set; }
        public bool SelectionButton3IsRemoved { get; set; }
        public bool SelectionButton4IsRemoved { get; set; }

        //saving newly created cell
        public int CellId { get; set; }
    }
}
