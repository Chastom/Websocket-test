using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WsApp.Models;

namespace WsApp.Controllers
{
    public class ShipsController : Controller
    {
        private Context _context;

        public ShipsController(Context context)
        {
            _context = context;
        }
        public bool AddShip(int cellId,int shipTypeId, string shipType)
        {
            Ship tempShip = new Ship();
            tempShip.CellId = cellId;
            tempShip.Name = shipType;
            tempShip.ShipTypeId = shipTypeId;
            
            _context.Ships.Add(tempShip);
            _context.SaveChanges();
            return true;
        }        
    }
}