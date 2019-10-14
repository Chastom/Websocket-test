using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WsApp.Models;

namespace WsApp.Controllers
{
    public class BoardsController : Controller
    {
        private Context _context;

        public BoardsController(Context context)
        {
            _context = context;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//ba ateina bet neveikia
        public int CreateBoard(int battleArenaId)
        {
            Console.WriteLine("Ateina BA ID " + battleArenaId);
            Board tempBoard = new Board();
            tempBoard.BattleArenaId = battleArenaId;
            _context.Boards.Add(tempBoard);
            _context.SaveChanges();

            return GetBoardId(battleArenaId);
        }
        public int GetBoardId(int battleArenaId)
        {
            int id = -1;
            List<Board> board = _context.Boards.Where(s => s.BattleArenaId == battleArenaId).ToList();
            if (board.Count > 0)
            {
                id = board[0].BoardId;
            }
            return id;
        }
        // GET: Boards
        public ActionResult Index()
        {
            return View();
        }

        // GET: Boards/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Boards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boards/Create
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

        // GET: Boards/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Boards/Edit/5
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

        // GET: Boards/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Boards/Delete/5
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