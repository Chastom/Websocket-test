using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Proxy
{
    public class TurnOutcome
    {
        public string CallerTurn { get; set; }
        public string OpponetTurn { get; set; }
        public string ActiveId { get; set; }
        public string InactiveId { get; set; }

        public TurnOutcome() { }

        public TurnOutcome(string callerTurn, string opponentTurn, string active, string inactive)
        {
            CallerTurn = callerTurn;
            OpponetTurn = opponentTurn;
            ActiveId = active;
            InactiveId = inactive;
        }
    }
}
