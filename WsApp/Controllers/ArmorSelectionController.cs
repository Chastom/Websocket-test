using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;

namespace WsApp
{

    public class ArmorSelectionController
    {
        private Context _context;

        public ArmorSelectionController(Context context)
        {
            _context = context;
        }

        public void AddArmorSelection(string socketId)
        {
            ArmorSelection armorSelection = GetArmorSelection(socketId);
            if (armorSelection == null)
            {
                Console.WriteLine("===============================================  " + "create new armor");
                CreateArmorSelection(socketId);
            }
            else
            {
                armorSelection.Selected = true;
                _context.SaveChanges();
                Console.WriteLine("===============================================  " + "activate armor -> " + armorSelection.Selected);
            }
        }

        public void Deselect(string socketId)
        {
            ArmorSelection armorSelection = GetArmorSelection(socketId);
            armorSelection.Selected = false;
            _context.SaveChanges();
            Console.WriteLine("===============================================  " + "deactivate armor -> " + armorSelection.Selected);
        }

        public bool IsArmorSelected(string socketId)
        {
            ArmorSelection armorSelection = GetArmorSelection(socketId);
            if(armorSelection != null)
            {
                if(armorSelection.Selected == true)
                {
                    return true;
                }
            }
            return false;
        }

        private ArmorSelection GetArmorSelection(string socketId)
        {
            return _context.ArmorSelections.Where(s => s.SocketId == socketId).FirstOrDefault();
        }

        private void CreateArmorSelection(string socketId)
        {
            ArmorSelection temp = new ArmorSelection();
            temp.SocketId = socketId;
            temp.Selected = true;
            temp.ArmorSize = 6;
            _context.ArmorSelections.Add(temp);
            _context.SaveChanges();
        }

        public bool ArmorUp(int x, int y, int arenaId, string socketId)
        {
            ArmorSelection armorSelection = GetArmorSelection(socketId);
            Cell cell = ReturnCell(x, y, arenaId);
            if (cell != null)
            {
                if (cell.IsArmored == false && armorSelection.ArmorSize >= 1)
                {
                    cell.IsArmored = true;
                    armorSelection.ArmorSize--;
                    _context.SaveChanges();                    
                    return true;
                }
            }
            return false;
        }

        public Cell ReturnCell(int posx, int posy, int battleArenaId)
        {
            return _context.Cells.Where(s => s.PosX == posx && s.PosY == posy && s.BattleArenaId == battleArenaId).FirstOrDefault();
        }
    }
}
