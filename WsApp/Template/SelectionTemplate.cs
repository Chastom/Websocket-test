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
        public List<CommandOutcome> Execute(SelectionParams param)
        {
            List<CommandOutcome> outcomes = new List<CommandOutcome>();
            bool placed = PlaceSelection(param.socketId, param.posX, param.posY, param.battleArenaId);

            if (placed)
            {
                outcomes = GetSelectionOutcome();
                return outcomes;
            }
            else
            {
                outcomes.Add(new CommandOutcome(PlacementOutcome.Invalid));
                return outcomes;
            }

        }

        public abstract UndoResult Undo(string socketId);

        public abstract bool PlaceSelection(string socketId, int posX, int posY, int battleArenaId);

        //public abstract CommandOutcome GetSelectionOutcome();
        public abstract List<CommandOutcome> GetSelectionOutcome();

    }
}
