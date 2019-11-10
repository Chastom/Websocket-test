using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Commands
{
    public class CommandOutcome
    {
        public PlacementOutcome outcome;
        public int count;
        public string idToRemove;

        public CommandOutcome(PlacementOutcome outcome)
        {
            this.outcome = outcome;
            count = 0;
            idToRemove = "-1";
        }
    }
}
