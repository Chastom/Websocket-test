using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class BattleArena
    {
        public int BattleArenaId { get; set; }

        public int PlayerId { get; set; }
        public int BoardId { get; set; }

    }
}
