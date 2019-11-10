using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WsApp.Models;

namespace WsApp.Controllers
{
    public class ShipSelectionController : Controller
    {
        private Context _context;

        public ShipSelectionController(Context context)
        {
            _context = context;
        }

        public void MarkSelection(ShipType type, Player player, string buttonId)
        {
            ShipSelection temp = GetSelection(player.Socket);
            if (temp == null)
            {
                temp = new ShipSelection();
                temp.Player = player;
                temp.ShipTypeId = type.ShipTypeId;
                temp.Count = type.Count;
                temp.Size = type.Size;
                temp.ButtonId = buttonId;
                temp.IsSelected = true;

                _context.ShipSelections.Add(temp);
                _context.SaveChanges();
            }
            else
            {
                temp.ShipTypeId = type.ShipTypeId;
                temp.Count = type.Count;
                temp.Size = type.Size;
                temp.ButtonId = buttonId;
                temp.IsSelected = true;
                _context.SaveChanges();
            }
        }

        public ShipSelection GetSelection(string socketId)
        {
            return _context.ShipSelections.Where(s => s.Player.Socket.Contains(socketId)).FirstOrDefault();
        }

        public ShipType GetShipType(string socketId)
        {
            ShipSelection selection = GetSelection(socketId);
            return _context.ShipTypes.Where(s => s.ShipTypeId.Equals(selection.ShipTypeId)).FirstOrDefault();
        }

        /*             
           checking if ShipSelection table is generated for this player
           otherwise, it means that the selection button was not selected
           also, we check if IsSelected field is equal to true
           otherwise, it means that all ships have been placed already
           or a new ship type has not been selected
        */
        public bool IsValid(string socketId)
        {
            ShipSelection selection = GetSelection(socketId);
            if (selection == null)
            {
                return false;
            }
            else
            {
                return selection.IsSelected;
            }
        }

        public bool PlaceShip(string socketId)
        {
            ShipSelection selection = GetSelection(socketId);
            ShipType type = GetShipType(socketId);
            int count = selection.Count;
            int size = selection.Size;
            size--;
            if (size == 0)
            {
                count--;
                if (count == 0)
                {
                    selection.IsSelected = false;
                    SaveButton(selection);
                    _context.SaveChanges();                    
                    return false;
                }
                size = type.Size;
            }
            selection.Size = size;
            selection.Count = count;
            _context.SaveChanges();
            return true;
        }

        public bool ValidatePlacement(string socketId, int posX, int posY, int arenaId)
        {
            ShipSelection selection = GetSelection(socketId);
            ShipType type = GetShipType(socketId);
            Cell cell = ReturnCell(posX, posY, arenaId);

            if (cell == null)
            {
                if (NoCellsNearby(posX, posY, arenaId, type))
                {
                    List<Ship> ships = GetShipsByType(type.ShipTypeId, socketId);
                    int shipId = 0;
                    if (ships.Count < type.Count - selection.Count + 1)
                    {
                        if (NoShipNearby(posX, posY, arenaId, new Ship(), true))
                        {
                            shipId = AddShip(arenaId, type.ShipTypeId, type.Type, type.Size, socketId, posX, posY);
                            //cell was created when adding a new ship
                            cell = ReturnCell(posX, posY, arenaId);
                            cell.ShipId = shipId;
                            SaveCommand(cell, selection, socketId);
                            _context.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        shipId = ships[ships.Count - 1].ShipId;
                        if (!CorrectPlacement(type, selection, ships[ships.Count - 1], arenaId, posX, posY))
                        {
                            return false;
                        }
                    }
                    //creating a new cell for this ship
                    cell = CreateCell(posX, posY, arenaId);
                    cell.ShipId = shipId;
                    SaveCommand(cell, selection, socketId);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public void SaveButton(ShipSelection selection)
        {
            string buttonId = selection.ButtonId;
            switch (buttonId)
            {
                case "0":
                    selection.Button0IsRemoved = true;
                    break;
                case "1":
                    selection.Button1IsRemoved = true;
                    break;
                case "2":
                    selection.Button2IsRemoved = true;
                    break;
                case "3":
                    selection.Button3IsRemoved = true;
                    break;
                case "4":
                    selection.Button4IsRemoved = true;
                    break;
                default:
                    break;
            }
            _context.SaveChanges();
        }

        //creating a record of this command, so it can be undone later
        public void SaveCommand(Cell cell, ShipSelection selection, string socketId)
        {
            PlacementCommand command = new PlacementCommand();
            command.CellId = cell.CellId;
            command.IsArmor = false;
            command.SocketId = socketId;

            //saving selection current values
            command.SelectionShipSelectionId = selection.ShipSelectionId;
            command.SelectionCount = selection.Count;
            command.SelectionIsSelected = selection.IsSelected;
            command.SelectionSize = selection.Size;
            command.SelectionShipTypeId = selection.ShipTypeId;
            command.SelectionButtonId = selection.ButtonId;
            command.SelectionButton0IsRemoved = selection.Button0IsRemoved;
            command.SelectionButton1IsRemoved = selection.Button1IsRemoved;
            command.SelectionButton2IsRemoved = selection.Button2IsRemoved;
            command.SelectionButton3IsRemoved = selection.Button3IsRemoved;
            command.SelectionButton4IsRemoved = selection.Button4IsRemoved;

            _context.Commands.Add(command);
            _context.SaveChanges();
        }

        public bool CorrectPlacement(ShipType type, ShipSelection selection, Ship ship, int arenaId, int posX, int posY)
        {
            int shipPartsSelected = type.Size - selection.Size;

            //check if nearby ship is from the same sequence
            if (NoShipNearby(posX, posY, arenaId, ship, false))
            {
                List<string> directions = new List<string> { "above", "below", "right", "left" };
                for (int i = 0; i < 4; i++)
                {
                    int sequence = ShipSequence(shipPartsSelected, posX, posY, ship, directions[i], arenaId);
                    if (sequence == shipPartsSelected)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int ShipSequence(int shipPartsSelected, int posX, int posY, Ship ship, string direction, int arenaId)
        {
            int sequence = 0;
            for (int i = 0; i < shipPartsSelected; i++)
            {
                Cell cellConnect;
                switch (direction)
                {
                    case "above":
                        cellConnect = ReturnCell(posX, posY + i + 1, arenaId);
                        break;
                    case "below":
                        cellConnect = ReturnCell(posX, posY - i - 1, arenaId);
                        break;
                    case "right":
                        cellConnect = ReturnCell(posX + i + 1, posY, arenaId);
                        break;
                    default:
                        cellConnect = ReturnCell(posX - i - 1, posY, arenaId);
                        break;
                }
                if (cellConnect != null)
                {
                    Ship shipConnect = GetShip(cellConnect.ShipId);
                    if (shipConnect != null)
                    {
                        if (ship.ShipId == shipConnect.ShipId)
                        {
                            sequence++;
                        }
                    }
                }
            }
            return sequence;
        }

        public bool NoCellsNearby(int posX, int posY, int arenaId, ShipType type)
        {
            Cell right = ReturnCell(posX + 1, posY, arenaId);
            Cell left = ReturnCell(posX - 1, posY, arenaId);
            Cell above = ReturnCell(posX, posY + 1, arenaId);
            Cell below = ReturnCell(posX, posY - 1, arenaId);
            Cell diag1 = ReturnCell(posX + 1, posY + 1, arenaId);
            Cell diag2 = ReturnCell(posX - 1, posY - 1, arenaId);
            Cell diag3 = ReturnCell(posX - 1, posY + 1, arenaId);
            Cell diag4 = ReturnCell(posX + 1, posY - 1, arenaId);
            List<Cell> nearbyCells = new List<Cell>() { right, left, above, below, diag1, diag2, diag3, diag4 };

            for (int i = 0; i < nearbyCells.Count; i++)
            {
                if (nearbyCells[i] != null)
                {
                    Ship ship = GetShip(nearbyCells[i].ShipId);
                    if (type.Type == "Schnicel" && nearbyCells[i].ShipId != 0)
                    {
                        return false;
                    }
                    if (nearbyCells[i].ShipId != 0 && type.ShipTypeId != ship.ShipTypeId)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool NoShipNearby(int posX, int posY, int arenaId, Ship ship, bool isNew)
        {
            Cell right = ReturnCell(posX + 1, posY, arenaId);
            Cell left = ReturnCell(posX - 1, posY, arenaId);
            Cell above = ReturnCell(posX, posY + 1, arenaId);
            Cell below = ReturnCell(posX, posY - 1, arenaId);
            Cell diag1 = ReturnCell(posX + 1, posY + 1, arenaId);
            Cell diag2 = ReturnCell(posX - 1, posY - 1, arenaId);
            Cell diag3 = ReturnCell(posX - 1, posY + 1, arenaId);
            Cell diag4 = ReturnCell(posX + 1, posY - 1, arenaId);
            List<Cell> nearbyCells = new List<Cell>() { right, left, above, below, diag1, diag2, diag3, diag4 };

            for (int i = 0; i < nearbyCells.Count; i++)
            {
                if (nearbyCells[i] != null)
                {
                    if (isNew && nearbyCells[i].ShipId != 0)
                    {
                        return false;
                    }
                    else if (nearbyCells[i].ShipId != 0 && nearbyCells[i].ShipId != ship.ShipId)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<Ship> GetShipsByType(int typeId, string socketId)
        {
            return _context.Ships.Where(s => s.ShipTypeId.Equals(typeId) && s.SocketId == socketId).ToList();
        }

        public Cell ReturnCell(int posx, int posy, int battleArenaId)
        {
            return _context.Cells.Where(s => s.PosX == posx && s.PosY == posy && s.BattleArenaId == battleArenaId).FirstOrDefault();
        }

        public int AddShip(int arenaId, int shipTypeId, string shipType, int size, string socketId, int posX, int posY)
        {
            Ship tempShip = new Ship();
            tempShip.CellId = CreateCell(posX, posY, arenaId).CellId;
            tempShip.Name = shipType;
            //tempShip.ShipTypeId = shipTypeId;
            tempShip.setState(shipTypeId);
            tempShip.RemainingTiles = size;
            tempShip.SocketId = socketId;

            _context.Ships.Add(tempShip);
            _context.SaveChanges();
            return tempShip.ShipId;
        }

        public Ship GetShip(int shipId)
        {
            return _context.Ships.Where(s => s.ShipId.Equals(shipId)).FirstOrDefault();
        }

        public Cell CreateCell(int x, int y, int arenaId)
        {
            Cell temp = new Cell();
            temp.BattleArenaId = arenaId;
            temp.PosX = x;
            temp.PosY = y;
            temp.IsHit = false;
            temp.IsArmored = false;
            _context.Cells.Add(temp);
            _context.SaveChanges();
            return temp;
        }

        public string GetButtonId(string socketId)
        {
            ShipSelection selection = GetSelection(socketId);
            return selection.ButtonId;
        }
    }
}