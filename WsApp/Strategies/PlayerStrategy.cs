using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Strategies
{
    public class PlayerStrategy
    {
        public string socketId;
        public Strategy activeStrategy;

        public PlayerStrategy(string id, Strategy active)
        {
            socketId = id;
            activeStrategy = active;
        }
    }
}
