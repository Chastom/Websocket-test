using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WsApp.Models;

namespace WsApp.Controllers
{
    public class DuelsController : Controller
    {
        private Context _context;

        public DuelsController(Context context)
        {
            _context = context;
        }
        // GET: Duels
        public ActionResult Index()
        {
            return View();
        }
        public int CountDuels()
        {
            return _context.Duels.Count();
        }
        public int DuelId()
        {
            return _context.Duels.Where(s => s.FirstPlayerSocketId != null).FirstOrDefault().DuelId; 
        }
        public bool StartDuel(string socketId, int baId)
        {
            Duel tempDuel = new Duel();
            tempDuel.FirstPlayerBAId = baId;
            tempDuel.FirstPlayerSocketId = socketId;
            _context.Duels.Add(tempDuel);
            _context.SaveChanges();
            return true;

        }
        public bool JoinDuel(string socketId, int baId)
        {
            List<Duel> duels = _context.Duels.Where(s => s.DuelId== DuelId() && s.SecondPlayerSocketId==null).ToList();
            if (duels.Count > 0)
            {
                _context.Duels.Where(s => s.DuelId == DuelId()).FirstOrDefault().SecondPlayerSocketId = socketId;
                _context.Duels.Where(s => s.DuelId == DuelId()).FirstOrDefault().SecondPlayerBAId = baId;
                _context.SaveChanges();
                return true;
            }
            List<Duel> fullduels = _context.Duels.Where(s => s.DuelId == DuelId() && s.SecondPlayerSocketId != null).ToList();
            if (fullduels.Count >0)
            {
                return false;
            }
            return false;
        }
        // GET: Duels/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Duels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Duels/Create
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

        // GET: Duels/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Duels/Edit/5
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

        // GET: Duels/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Duels/Delete/5
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