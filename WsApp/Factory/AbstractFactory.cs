using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;

namespace WsApp.Factory
{
    public abstract class AbstractFactory
    {
        public abstract BattleArena getBattleArena(string arenaType);
    }
}
