using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WsApp.Models;

namespace WsApp.Controllers
{
    public class PlayersController : Controller
    {
        private Context _context;

        public PlayersController(Context context)
        {
            _context = context;
        }

        public Player GetPlayer(string socketId)
        {
            List<Player> player = _context.Players.Where(s => s.Socket.Contains(socketId)).ToList();
            return player[0];
        }

        // GET: Players
        public ActionResult Index()
        {
            return View();
        }

        // GET: Players/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public int CreatePlayer(string socketId)
        {
            if (SearchPlayerBySocketId(socketId)==false)
            {
                Player tempPlayer = new Player();
                tempPlayer.Socket = socketId;
                
                _context.Players.Add(tempPlayer);
                _context.SaveChanges();
            }
            return GetPlayerId(socketId);
        }
         public bool AddPlayerID(int createdId, int CreatedBAId)
        {
            _context.Players.Where(s => s.PlayerId == createdId).FirstOrDefault().BattleArenaId = CreatedBAId;
            _context.SaveChanges();
            return true;
        }
        public int GetPlayerId(string socketId)
        {
            int id = -1;
            List<Player> player = _context.Players.Where(s => s.Socket.Contains(socketId)).ToList();
            if (player.Count > 0)
            {
                id = player[0].PlayerId;
            }            
            return id;
        }
        public bool SearchPlayerBySocketId( string socketId)
        {
            List<Player> play = _context.Players.Where(s => s.Socket.Contains(socketId)).ToList();
            if (play.Count==0)
            {
                return false;
            }
            return true;
        }
        public async Task<ActionResult> Create(string socketId)
        {
            Player player = new Player();
            player.Socket = socketId;

            if (ModelState.IsValid)
            {
                if (_context.Players.Count()<=2)
                {
                    _context.Players.Add(player);
                        await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
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
        public static void UseCreate(string socketID)
        {
            //Player player = new Player();
            //player.Username = socketID;
            //_context.Add(player);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ShipTypeId,Type,Size,ShipId")] ShipType shipType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(shipType);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(shipType);
        //}
        // GET: Players/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Players/Edit/5
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

        // GET: Players/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Players/Delete/5
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