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

        public bool ValidatePlacement(string socketId, int posX, int posY, Cell cell, List<Cell> cells)
        {
            ShipSelection selection = GetSelection(socketId);
            ShipType type = GetShipType(socketId);

            if (cell.ShipId == 0)
            {
                if (NoCellsNearby(posX, posY, cell, type))
                {
                    List<Ship> ships = GetShipsByType(type.ShipTypeId, socketId);
                    int shipId = 0;
                    if (ships.Count < type.Count - selection.Count + 1)
                    {
                        if (NoShipNearby(posX, posY, cell, new Ship(), true))
                        {
                            shipId = AddShip(cell.CellId, type.ShipTypeId, type.Type, type.Size, socketId);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        shipId = ships[ships.Count - 1].ShipId;
                        if (!CorrectPlacement(type, selection, ships[ships.Count - 1], cell, posX, posY))
                        {
                            return false;
                        }
                    }
                    cell.ShipId = shipId;
                    return true;
                }
            }
            return false;

        }

        public bool CorrectPlacement(ShipType type, ShipSelection selection, Ship ship, Cell cell, int posX, int posY)
        {
            int shipPartsSelected = type.Size - selection.Size;

            //check if nearby ship is from the same sequence
            if (NoShipNearby(posX, posY, cell, ship, false))
            {
                List<string> directions = new List<string> { "above", "below", "right", "left" };
                for (int i = 0; i < 4; i++)
                {
                    int sequence = ShipSequence(shipPartsSelected, posX, posY, ship, cell, directions[i]);
                    if (sequence == shipPartsSelected)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int ShipSequence(int shipPartsSelected, int posX, int posY, Ship ship, Cell cell, string direction)
        {
            int sequence = 0;
            for (int i = 0; i < shipPartsSelected; i++)
            {
                Cell cellConnect;
                switch (direction)
                {
                    case "above":
                        cellConnect = ReturnCell(posX, posY + i + 1, cell.BattleArenaId);
                        break;
                    case "below":
                        cellConnect = ReturnCell(posX, posY - i - 1, cell.BattleArenaId);
                        break;
                    case "right":
                        cellConnect = ReturnCell(posX + i + 1, posY, cell.BattleArenaId);
                        break;
                    default:
                        cellConnect = ReturnCell(posX - i - 1, posY, cell.BattleArenaId);
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

        public bool NoCellsNearby(int posX, int posY, Cell cell, ShipType type)
        {
            Cell right = ReturnCell(posX + 1, posY, cell.BattleArenaId);
            Cell left = ReturnCell(posX - 1, posY, cell.BattleArenaId);
            Cell above = ReturnCell(posX, posY + 1, cell.BattleArenaId);
            Cell below = ReturnCell(posX, posY - 1, cell.BattleArenaId);
            Cell diag1 = ReturnCell(posX + 1, posY + 1, cell.BattleArenaId);
            Cell diag2 = ReturnCell(posX - 1, posY - 1, cell.BattleArenaId);
            Cell diag3 = ReturnCell(posX - 1, posY + 1, cell.BattleArenaId);
            Cell diag4 = ReturnCell(posX + 1, posY - 1, cell.BattleArenaId);
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

        public bool NoShipNearby(int posX, int posY, Cell cell, Ship ship, bool isNew)
        {
            Cell right = ReturnCell(posX + 1, posY, cell.BattleArenaId);
            Cell left = ReturnCell(posX - 1, posY, cell.BattleArenaId);
            Cell above = ReturnCell(posX, posY + 1, cell.BattleArenaId);
            Cell below = ReturnCell(posX, posY - 1, cell.BattleArenaId);
            Cell diag1 = ReturnCell(posX + 1, posY + 1, cell.BattleArenaId);
            Cell diag2 = ReturnCell(posX - 1, posY - 1, cell.BattleArenaId);
            Cell diag3 = ReturnCell(posX - 1, posY + 1, cell.BattleArenaId);
            Cell diag4 = ReturnCell(posX + 1, posY - 1, cell.BattleArenaId);
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

        public int AddShip(int cellId, int shipTypeId, string shipType, int size, string socketId)
        {
            Ship tempShip = new Ship();
            tempShip.CellId = cellId;
            tempShip.Name = shipType;
            tempShip.ShipTypeId = shipTypeId;
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

        public string GetButtonId(string socketId)
        {
            ShipSelection selection = GetSelection(socketId);
            return selection.ButtonId;
        }
    }
}