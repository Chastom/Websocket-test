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
        public int posX;
        public int posY;

        public CommandOutcome(PlacementOutcome outcome)
        {
            this.outcome = outcome;
            count = 0;
            idToRemove = "-1";
        }

        public CommandOutcome(PlacementOutcome outcome, int posX, int posY)
        {
            this.outcome = outcome;
            this.posX = posX;
            this.posY = posY;
            count = 0;
            idToRemove = "-1";
        }
    }
}
