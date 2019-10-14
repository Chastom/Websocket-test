using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Username { get; set; }
        public string Socket { get; set; }
        public bool Turn { get; set; }
        public DateTime Timer { get; set; }

        public int BattleArenaId { get; set; }
    }
}
