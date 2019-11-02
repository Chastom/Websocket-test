using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WsApp.Models;

namespace WsApp.Controllers
{
    public class CellsController : Controller
    {
        private Context _context;

        public CellsController(Context context)
        {
            _context = context;
        }

        public Cell ReturnCell(int posx, int posy, int battleArenaId)
        {
            return _context.Cells.Where(s => s.PosX == posx && s.PosY == posy && s.BattleArenaId == battleArenaId).FirstOrDefault();
        }

        public AttackOutcome AttackCell(int posx, int posy, string socketId)
        {
            Cell cell = ReturnCell(posx, posy, GetOpponentArenaId(socketId));

            if (cell == null)
            {
                return AttackOutcome.Missed;
            }
            else if (cell.IsHit != true)
            {
                if (cell.IsArmored == true)
                {
                    cell.IsArmored = false;
                    _context.SaveChanges();
                    return AttackOutcome.Armor;
                }
                else
                {
                    cell.IsHit = true;
                    Ship ship = GetShip(cell.ShipId);
                    ship.RemainingTiles--;
                    _context.SaveChanges();
                    return AttackOutcome.Hit;
                }

            }
            return AttackOutcome.Invalid;
        }

        public int GetOpponentArenaId(string socketId)
        {
            Duel duel = _context.Duels.Where(s => s.FirstPlayerSocketId == socketId || s.SecondPlayerSocketId == socketId).FirstOrDefault();
            if (duel.FirstPlayerSocketId == socketId)
            {
                return duel.SecondPlayerBAId;
            }
            else
            {
                return duel.FirstPlayerBAId;
            }
        }

        public Ship GetShip(int shipId)
        {
            return _context.Ships.Where(s => s.ShipId.Equals(shipId)).FirstOrDefault();
        }

        public List<Cell> ReturnAllCells(int battleArenaId)
        {
            return _context.Cells.Where(s => s.BattleArenaId == battleArenaId).ToList();
        }

        public int ReturnCellId(int posx, int posy, int battleArenaId)
        {
            int id = -1;
            List<Cell> ba = _context.Cells.Where(s => s.PosX == posx && s.PosY == posy && s.BattleArenaId == battleArenaId).ToList();
            if (ba.Count > 0)
            {
                //id = ba[0].BattleArenaId;
                id = ba[0].CellId;
            }
            return id;
        }
    }
}