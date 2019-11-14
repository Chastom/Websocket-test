﻿using System;
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
        private CommandOutcome commandOutcome;

        public PlaceArmorSelection(ArmorSelectionController armorSelection, Context context)
        {
            this.armorSelection = armorSelection;
            _context = context;
            commandOutcome = null;
        }

        public override bool PlaceSelection(string socketId, int posX, int posY, int battleArenaId)
        {
            bool armored = armorSelection.ArmorUp(posX, posY, battleArenaId, socketId);
            if (armored)
            {
                int count = armorSelection.GetArmorCount(socketId);
                CommandOutcome outcome = new CommandOutcome(PlacementOutcome.Armor);
                outcome.count = count;
                commandOutcome = outcome;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override CommandOutcome GetSelectionOutcome()
        {
            return commandOutcome;
        }

        public override UndoResult Undo(string socketId)
        {
            throw new NotImplementedException();
        }
    }
}