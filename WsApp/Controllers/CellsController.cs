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
        public int AttackCell(int posx, int posy, int battleArenaId)
        {
            int cellId = -1;
            Cell tmp = ReturnCell(posx, posy, battleArenaId);
            if (tmp.IsHit == true)
            {
                return -2;
            }
            if (tmp.IsHit ==false && tmp.ShipId==0)
            {
                _context.Cells.Where(s => s.PosX == posx && s.PosY == posy && s.BattleArenaId == battleArenaId).FirstOrDefault().IsHit = true;
                _context.SaveChanges();
                return -1;
            }           
            else
            {
                _context.Cells.Where(s => s.PosX == posx && s.PosY == posy && s.BattleArenaId == battleArenaId).FirstOrDefault().IsHit = true;
                _context.SaveChanges();
                cellId = _context.Cells.Where(s => s.PosX == posx && s.PosY == posy && s.BattleArenaId == battleArenaId).FirstOrDefault().CellId;
            }
            return cellId;
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