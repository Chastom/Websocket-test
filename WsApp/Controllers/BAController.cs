using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WsApp.Models;
using WsApp.Interfaces;
using WsApp.Factory;

namespace WsApp.Controllers
{
    public class BAController : Controller, IBattleArena
    {
        private Context _context;

        public BAController(Context context)
        {
            _context = context;
        }

        // GET: BA
        public ActionResult Index()
        {
            return View();
        }

        // GET: BA/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BA/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int CreateBA(int playerId)
        {
            //BattleArena tempBA = new BattleArena();
            AbstractFactory abstractFactory = FactoryProducer.getFactory(false);
            BattleArena tempBA = abstractFactory.getBattleArena("basic");
            tempBA.PlayerId = playerId;
            _context.BattleArenas.Add(tempBA);
                _context.SaveChanges();
            
            return GetBAId(playerId);
        }
        public int GetBAId(int playerId)
        {
            int id = -1;
            List<BattleArena> ba = _context.BattleArenas.Where(s => s.PlayerId == playerId).ToList();
            if (ba.Count > 0)
            {
                id = ba[0].BattleArenaId;
            }
            return id;
        }
        //public bool AddBoardID(int baId, int boardId)
        //{
        //    _context.BattleArenas.Where(s => s.BattleArenaId == baId).FirstOrDefault().BoardId = boardId;
        //    _context.SaveChanges();
           
        //    return true;

        //}
        // POST: BA/Create
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

        // GET: BA/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BA/Edit/5
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

        // GET: BA/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BA/Delete/5
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