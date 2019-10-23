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
        public bool AttackShip(int cellId)
        {
            List<Ship> tmp = _context.Ships.Where(s => s.CellId == cellId).ToList();
            _context.Ships.Where(s => s.CellId == cellId).FirstOrDefault().RemainingTiles = tmp[0].RemainingTiles-1;

            return true;
        }
        // GET: Ships
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ships/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ships/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ships/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Ships/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ships/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Ships/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ships/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}