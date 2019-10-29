using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;

namespace WsApp.Factory
{
    public class BattleArenaFactory : AbstractFactory
    {
        override 
        public BattleArena getBattleArena(string arenaType)
        {
            if (arenaType.Equals("basic"))
            {
                return new BattleArena();
            } else if (arenaType.Equals("extraFeatured"))
            {
                //In the future this may be the lvl2 game with all extra features
                return new BattleArena();
            } else
            {
                //Because why not
                return new BattleArena();
            }
        }
    }
}
