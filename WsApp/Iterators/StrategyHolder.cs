using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;
using WsApp.Strategies;

namespace WsApp.Iterators
{
    public static class StrategyHolder
    {
        //public static List<PlayerStrategy> playersStrategies = new List<PlayerStrategy>();
        public static StrategyAggregate playersStrategies = new StrategyAggregate();

        public static void AddStrategy(string socketId, Strategy strategy)
        {
            //playersStrategies.Add(new PlayerStrategy(socketId, strategy));
            playersStrategies[playersStrategies.Count] = new PlayerStrategy(socketId, strategy);
            Console.WriteLine("==================================================== strategy added ->>> ");
        }

        public static void ChangeActiveStrategy(string socketId, Strategy strategy)
        {
            Iterator i = playersStrategies.CreateIterator();
            object item = i.First();
            while (item != null)
            {
                PlayerStrategy tmp = (PlayerStrategy)item;
                if (tmp.socketId.Equals(socketId))
                {
                    tmp.activeStrategy = strategy;
                    Console.WriteLine("=========================================================== STRATEGY UPDATED TO -> " + tmp.activeStrategy.ToString());
                    break;
                }
                item = i.Next();
            }
        }

        public static void Print()
        {
            Console.WriteLine("============================================== STRATEGY --------> ");
 
        }

        public static Strategy GetPlayerStrategy(string socketId)
        {
            Iterator i = playersStrategies.CreateIterator();
            object item = i.First();
            while (item != null)
            {
                PlayerStrategy tmp = (PlayerStrategy)item;
                if (tmp.socketId == socketId)
                {
                    return tmp.activeStrategy;
                }
                item = i.Next();
            }
            return null;
        }
    }
}
