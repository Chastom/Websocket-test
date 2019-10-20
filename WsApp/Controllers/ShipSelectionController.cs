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
            ShipType type = _context.ShipTypes.Where(s => s.ShipTypeId.Equals(selection.ShipTypeId)).FirstOrDefault();
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

        public string GetButtonId(string socketId)
        {
            ShipSelection selection = GetSelection(socketId);
            return selection.ButtonId;
        }
    }
}