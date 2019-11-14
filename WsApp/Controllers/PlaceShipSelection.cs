using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Commands;
using WsApp.Models;
using WsApp.Template;

namespace WsApp.Controllers
{
    public class PlaceShipSelection : SelectionTemplate
    {
        private ArmorSelectionController armorSelection;
        private ShipSelectionController shipSelectionController;
        private Context _context;
        private CommandOutcome commandOutcome;

        public PlaceShipSelection(ArmorSelectionController armorSelection, ShipSelectionController shipSelectionController, Context context)
        {
            this.armorSelection = armorSelection;
            this.shipSelectionController = shipSelectionController;
            _context = context;
            commandOutcome = null;
        }

        //public CommandOutcome Execute(string socketId, int posX, int posY, int battleArenaId)
        //{
        //    //armor placement
        //    if (armorSelection.IsArmorSelected(socketId))
        //    {
        //        bool armored = armorSelection.ArmorUp(posX, posY, battleArenaId, socketId);
        //        if (armored)
        //        {
        //            int count = armorSelection.GetArmorCount(socketId);
        //            CommandOutcome outcome = new CommandOutcome(PlacementOutcome.Armor);
        //            outcome.count = count;
        //            return outcome;
        //        }
        //        else
        //        {
        //            return new CommandOutcome(PlacementOutcome.Invalid);
        //        }
        //    }
        //    //ship placement | cant place a ship if armor function is selected
        //    else
        //    {
        //        if (!shipSelectionController.IsValid(socketId))
        //        {
        //            return new CommandOutcome(PlacementOutcome.Invalid);
        //        }
        //        else
        //        {
        //            bool canPlace = shipSelectionController.ValidatePlacement(socketId, posX, posY, battleArenaId);

        //            if (canPlace)
        //            {

        //                bool placed = shipSelectionController.PlaceShip(socketId);
        //                if (placed)
        //                {
        //                    return new CommandOutcome(PlacementOutcome.Ship);
        //                }
        //                else
        //                {
        //                    string id = shipSelectionController.GetButtonId(socketId);
        //                    CommandOutcome outcome = new CommandOutcome(PlacementOutcome.LastShip);
        //                    outcome.idToRemove = id;
        //                    return outcome;
        //                }
        //            }
        //            else
        //            {
        //                return new CommandOutcome(PlacementOutcome.Invalid);
        //            }
        //        }
        //    }
        //}

        public override bool PlaceSelection(string socketId, int posX, int posY, int battleArenaId)
        {
            if (!shipSelectionController.IsValid(socketId))
            {
                commandOutcome = new CommandOutcome(PlacementOutcome.Invalid);
                return false;
            }
            else
            {
                bool canPlace = shipSelectionController.ValidatePlacement(socketId, posX, posY, battleArenaId);

                if (canPlace)
                {

                    bool placed = shipSelectionController.PlaceShip(socketId);
                    if (placed)
                    {
                        commandOutcome = new CommandOutcome(PlacementOutcome.Ship);
                        return true;
                    }
                    else
                    {
                        string id = shipSelectionController.GetButtonId(socketId);
                        CommandOutcome outcome = new CommandOutcome(PlacementOutcome.LastShip);
                        outcome.idToRemove = id;
                        commandOutcome = outcome;
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public override CommandOutcome GetSelectionOutcome()
        {
            return commandOutcome;
        }

        public override UndoResult Undo(string socketId)
        {
            PlacementCommand command = _context.Commands.Where(s => s.SocketId.Contains(socketId)).LastOrDefault();
            if (command != null)
            {
                //restoring ShipSelection to previous iteration
                ShipSelection selection = shipSelectionController.GetSelection(socketId);
                //selection.ShipSelectionId = command.SelectionShipSelectionId;
                selection.Count = command.SelectionCount;
                selection.IsSelected = command.SelectionIsSelected;
                selection.Size = command.SelectionSize;
                selection.ShipTypeId = command.SelectionShipTypeId;
                selection.ButtonId = command.SelectionButtonId;
                selection.Button0IsRemoved = command.SelectionButton0IsRemoved;
                selection.Button1IsRemoved = command.SelectionButton1IsRemoved;
                selection.Button2IsRemoved = command.SelectionButton2IsRemoved;
                selection.Button3IsRemoved = command.SelectionButton3IsRemoved;
                selection.Button4IsRemoved = command.SelectionButton4IsRemoved;
                _context.SaveChanges(); //can be removed later probably

                List<bool> buttons = new List<bool>();
                buttons.Add(command.SelectionButton0IsRemoved);
                buttons.Add(command.SelectionButton1IsRemoved);
                buttons.Add(command.SelectionButton2IsRemoved);
                buttons.Add(command.SelectionButton3IsRemoved);
                buttons.Add(command.SelectionButton4IsRemoved);

                string activeButton = command.SelectionButtonId;
                //deleting last created cell
                Cell cell = GetCell(command.CellId);
                _context.Cells.Remove(cell);

                _context.Commands.Remove(command);
                _context.SaveChanges();

                return new UndoResult(new Coordinate(cell.PosX, cell.PosY), activeButton, buttons);
            }
            return null;
        }

        public Cell GetCell(int cellId)
        {
            return _context.Cells.Where(s => s.CellId == cellId).FirstOrDefault();
        }
    }
}
