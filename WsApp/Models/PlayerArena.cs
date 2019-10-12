using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class PlayerArena
    {
        public int PlayerArenaId { get; set; }
        public List<Player> Players { get; set; }
        public int BattleArenaId { get; set; }
    }
}
