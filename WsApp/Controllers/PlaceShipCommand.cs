using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Commands;
using WsApp.Models;

namespace WsApp.Controllers
{
    public class PlaceShipCommand : Command
    {
        private ArmorSelectionController armorSelection;
        private ShipSelectionController shipSelectionController;
        private Context _context;

        public PlaceShipCommand(ArmorSelectionController armorSelection, ShipSelectionController shipSelectionController, Context context)
        {
            this.armorSelection = armorSelection;
            this.shipSelectionController = shipSelectionController;
            _context = context;
        }

        public CommandOutcome Execute(string socketId, int posX, int posY, int battleArenaId)
        {
            //armor placement
            if (armorSelection.IsArmorSelected(socketId))
            {
                bool armored = armorSelection.ArmorUp(posX, posY, battleArenaId, socketId);
                if (armored)
                {
                    int count = armorSelection.GetArmorCount(socketId);
                    CommandOutcome outcome = new CommandOutcome(PlacementOutcome.Armor);
                    outcome.count = count;
                    return outcome;
                }
                else
                {
                    return new CommandOutcome(PlacementOutcome.Invalid);
                }
            }
            //ship placement | cant place a ship if armor function is selected
            else
            {
                if (!shipSelectionController.IsValid(socketId))
                {
                    return new CommandOutcome(PlacementOutcome.Invalid);
                }
                else
                {
                    bool canPlace = shipSelectionController.ValidatePlacement(socketId, posX, posY, battleArenaId);

                    if (canPlace)
                    {

                        bool placed = shipSelectionController.PlaceShip(socketId);
                        if (placed)
                        {
                            return new CommandOutcome(PlacementOutcome.Ship);
                        }
                        else
                        {
                            string id = shipSelectionController.GetButtonId(socketId);
                            CommandOutcome outcome = new CommandOutcome(PlacementOutcome.LastShip);
                            outcome.idToRemove = id;
                            return outcome;
                        }
                    }
                    else
                    {
                        return new CommandOutcome(PlacementOutcome.Invalid);
                    }
                }
            }
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
