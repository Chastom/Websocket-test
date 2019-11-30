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
        private ShipSelectionController shipSelectionController;
        private Context _context;
        private List<CommandOutcome> commandOutcomes;

        public PlaceShipSelection(ShipSelectionController shipSelectionController, Context context)
        {
            this.shipSelectionController = shipSelectionController;
            _context = context;
            //commandOutcome = null;
            commandOutcomes = new List<CommandOutcome>();
        }

        public override bool PlaceSelection(string socketId, int posX, int posY, int battleArenaId)
        {
            if (!shipSelectionController.IsValid(socketId))
            {
                commandOutcomes.Add(new CommandOutcome(PlacementOutcome.Invalid));
                return false;
            }
            else
            {
                commandOutcomes = shipSelectionController.ValidatePlacement(socketId, posX, posY, battleArenaId);
                return true;
                //bool canPlace = shipSelectionController.ValidatePlacement(socketId, posX, posY, battleArenaId);

                //if (canPlace)
                //{
                //    bool placed = shipSelectionController.PlaceShip(socketId);
                //    if (placed)
                //    {
                //        commandOutcome = new CommandOutcome(PlacementOutcome.Ship);
                //        return true;
                //    }
                //    else
                //    {
                //        string id = shipSelectionController.GetButtonId(socketId);
                //        CommandOutcome outcome = new CommandOutcome(PlacementOutcome.LastShip);
                //        outcome.idToRemove = id;
                //        commandOutcome = outcome;
                //        return true;
                //    }
                //}
                //else
                //{
                //    return false;
                //}
            }
        }

        //public override CommandOutcome GetSelectionOutcome()
        //{
        //    return commandOutcome;
        //}
        public override List<CommandOutcome> GetSelectionOutcome()
        {
            return commandOutcomes;
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
                ////deleting last created cell
                //Cell cell = GetCell(command.CellId);
                //_context.Cells.Remove(cell);


                //deleting all cells with the last place ship id
                List<Coordinate> coordinates = new List<Coordinate>();
                Ship shipToRemove = GetShip(GetCell(command.CellId).ShipId);
                List<Cell> cells = GetCellsByShip(GetCell(command.CellId).ShipId);
                foreach(var cell in cells)
                {
                    coordinates.Add(new Coordinate(cell.PosX, cell.PosY));
                    _context.Cells.Remove(cell);
                    _context.SaveChanges();
                }

                _context.Ships.Remove(shipToRemove);
                _context.Commands.Remove(command);
                _context.SaveChanges();

                return new UndoResult(coordinates, activeButton, buttons);
            }
            return null;
        }

        public List<Cell> GetCellsByShip(int shipId)
        {
            return _context.Cells.Where(s => s.ShipId == shipId).ToList();
        }

        public Ship GetShip(int shipId)
        {
            return _context.Ships.Where(s => s.ShipId == shipId).FirstOrDefault();
        }

        public Cell GetCell(int cellId)
        {
            return _context.Cells.Where(s => s.CellId == cellId).FirstOrDefault();
        }
    }
}
