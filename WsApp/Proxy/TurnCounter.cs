using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Proxy
{
    public abstract class TurnCounter
    {
        public abstract TurnOutcome ChangeTurn();
        public abstract TurnOutcome GetCurrentTurn();
    }
}
