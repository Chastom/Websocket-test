using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class Duel
    {
        public int DuelId { get; set; }
        public int PlayerTurnId { get; set; }
        public int FirstPlayerBAId { get; set; }
        public int SecondPlayerBAId { get; set; }
        public string FirstPlayerSocketId { get; set; }
        public string SecondPlayerSocketId { get; set; }
    }
}
