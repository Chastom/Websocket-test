using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Strategies
{
    public class StrategyHolder
    {
        public static List<PlayerStrategy> playersStrategies = new List<PlayerStrategy>();

        public static void AddStrategySelector(PlayerStrategy strategySelector)
        {
            playersStrategies.Add(strategySelector);
            Console.WriteLine("==================================================== strategy added ->>> ");
        }

        public static void Print()
        {
            for (int i = 0; i < playersStrategies.Count; i++)
            {
                Console.WriteLine("============================================== STRATEGY --------> " + i);
            }
        }

        public static PlayerStrategy GetPlayerStrategy(string socketId)
        {
            for (int i = 0; i < playersStrategies.Count; i++)
            {
                if(playersStrategies[i].socketId == socketId)
                {
                    return playersStrategies[i];
                }
            }
            return null;
        }
    }
}
