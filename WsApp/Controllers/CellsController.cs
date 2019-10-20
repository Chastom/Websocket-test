using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        // GET: Cells
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool CreateCells(int battleArenaId)
        {
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    Cell tempCell = new Cell();
                    
                    tempCell.BattleArenaId = battleArenaId;
                    tempCell.PosX = x;
                    tempCell.PosY = y;
                    _context.Cells.Add(tempCell);
                }
                _context.SaveChanges();
            }
            
            _context.SaveChanges();

            return true;
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
        // GET: Cells/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cells/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cells/Create
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

        // GET: Cells/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cells/Edit/5
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

        // GET: Cells/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cells/Delete/5
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