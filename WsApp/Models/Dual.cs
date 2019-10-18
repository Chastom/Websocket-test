using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class Dual
    {
        public int DualId { get; set; }
        public int PlayerTurnId { get; set; }
        public int FirstPlayerBAId { get; set; }
        public int SecondPlayerBAId { get; set; }
        public string FirstPlayerSocketId { get; set; }
        public string SecondPlayerSocketId { get; set; }
    }
}
