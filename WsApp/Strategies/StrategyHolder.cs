using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;

namespace WsApp.Strategies
{
    public static class StrategyHolder
    {
        public static Context context;
        public static List<PlayerStrategy> playersStrategies = new List<PlayerStrategy>();

        public static void AddStrategy(string socketId, Strategy strategy)
        {
            playersStrategies.Add(new PlayerStrategy(socketId, strategy));
            Console.WriteLine("==================================================== strategy added ->>> ");
        }

        public static void Print()
        {
            for (int i = 0; i < playersStrategies.Count; i++)
            {
                Console.WriteLine("============================================== STRATEGY --------> " 
                    + i  + "  player: " + playersStrategies[i].socketId + "  strategy: " + playersStrategies[i].activeStrategy.ToString());
            }
        }

        public static Strategy GetPlayerStrategy(string socketId)
        {
            for (int i = 0; i < playersStrategies.Count; i++)
            {
                if(playersStrategies[i].socketId == socketId)
                {
                    return playersStrategies[i].activeStrategy;
                }
            }
            return null;
        }
    }
}
