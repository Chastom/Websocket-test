using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Commands;

namespace WsApp.Template
{
    public abstract class SelectionTemplate : Command
    {
        //template method
        public CommandOutcome Execute(string socketId, int posX, int posY, int battleArenaId)
        {
            bool placed = PlaceSelection(socketId, posX, posY, battleArenaId);

            if (placed)
            {
                CommandOutcome outcome = GetSelectionOutcome();
                return outcome;
            }
            else
            {
                return new CommandOutcome(PlacementOutcome.Invalid);
            }

        }

        public abstract UndoResult Undo(string socketId);

        public abstract bool PlaceSelection(string socketId, int posX, int posY, int battleArenaId);

        public abstract CommandOutcome GetSelectionOutcome();

    }
}
