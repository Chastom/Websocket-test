using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class Board
    {
        public int BoardId { get; set; }
        public List<Cell> Cells { get; set; }
    }
}
