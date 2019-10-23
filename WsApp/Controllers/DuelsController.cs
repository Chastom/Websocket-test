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
        //buvo planas padaryti taip, jog DUEL lenteleje butu TIK VIENAS irasas, todel ieskoma irasu kuriuose pirmas dalyvis yra NE NULL, o antras NULL
        public bool JoinDuel(string socketId, int baId)
        {
            List<Duel> duels = _context.Duels.Where(s => s.DuelId== DuelId() && s.SecondPlayerSocketId==null).ToList();
            if (duels.Count > 0)
            {
                _context.Duels.Where(s => s.DuelId == DuelId()).FirstOrDefault().SecondPlayerSocketId = socketId;
                _context.Duels.Where(s => s.DuelId == DuelId()).FirstOrDefault().SecondPlayerBAId = baId;
                _context.SaveChanges();

                Random rnd = new Random();
                int turnRND = rnd.Next(1, 3);
                if (turnRND ==1)
                {
                    List<Duel> firstPlayerID = _context.Duels.Where(s => s.FirstPlayerSocketId != null).ToList();
                    _context.Duels.Where(s => s.DuelId == DuelId()).FirstOrDefault().PlayerTurnId = firstPlayerID[0].FirstPlayerSocketId;
                    _context.SaveChanges();
                    _context.Players.Where(s => s.Socket == firstPlayerID[0].FirstPlayerSocketId).FirstOrDefault().Turn = true;
                    _context.SaveChanges();
                    return true;
                }
                if (turnRND == 2)
                {
                    
                    _context.Duels.Where(s => s.DuelId == DuelId()).FirstOrDefault().PlayerTurnId = socketId;
                    _context.SaveChanges();
                    _context.Players.Where(s => s.Socket == socketId).FirstOrDefault().Turn = true;
                    _context.SaveChanges();
                    return true;
                }
                return true;
            }
            //jeigu nera tai grazinama FALSE
            List<Duel> fullduels = _context.Duels.Where(s => s.DuelId == DuelId() && s.SecondPlayerSocketId != null).ToList();
            if (fullduels.Count >0)
            {
                return false;
            }
            return false;
        }
        public string GetOpponent(string socketId)
        {
            string opponent = "";
            List<Duel> duels = _context.Duels.Where(s => s.FirstPlayerSocketId == socketId).ToList();
            if (duels.Count ==0)
            {
                List<Duel> secondDuels = _context.Duels.Where(s => s.SecondPlayerSocketId == socketId).ToList();
                opponent = secondDuels[0].FirstPlayerSocketId;
            }
            opponent = duels[0].SecondPlayerSocketId;
            return opponent;
        }
        public bool ChangeTurns(string socketId)
        {
            string oponentsSocketId = GetOpponent(socketId);
            _context.Players.Where(s => s.Socket == socketId).FirstOrDefault().Turn = false;
            _context.SaveChanges();
            _context.Players.Where(s => s.Socket == oponentsSocketId).FirstOrDefault().Turn = true;
            _context.SaveChanges();
            return true;
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