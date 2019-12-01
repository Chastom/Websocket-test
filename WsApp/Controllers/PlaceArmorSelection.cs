using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Commands;
using WsApp.Models;
using WsApp.Template;

namespace WsApp.Controllers
{
    public class PlaceArmorSelection : SelectionTemplate
    {
        private ArmorSelectionController armorSelection;
        private Context _context;
        private List<CommandOutcome> commandOutcomes;

        public PlaceArmorSelection(ArmorSelectionController armorSelection, Context context)
        {
            this.armorSelection = armorSelection;
            _context = context;
            commandOutcomes = new List<CommandOutcome>();
        }

        public override bool PlaceSelection(string socketId, int posX, int posY, int battleArenaId)
        {
            bool armored = armorSelection.ArmorUp(posX, posY, battleArenaId, socketId);
            if (armored)
            {
                int count = armorSelection.GetArmorCount(socketId);
                CommandOutcome outcome = new CommandOutcome(PlacementOutcome.Armor);
                outcome.count = count;
                commandOutcomes.Add(outcome);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override List<CommandOutcome> GetSelectionOutcome()
        {
            return commandOutcomes;
        }

        public override UndoResult Undo(string socketId)
        {
            throw new NotImplementedException();
        }
    }
}
