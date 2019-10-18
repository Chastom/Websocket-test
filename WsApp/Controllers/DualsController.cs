using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WsApp.Models;

namespace WsApp.Controllers
{
    public class DualsController : Controller
    {
        private Context _context;

        public DualsController(Context context)
        {
            _context = context;
        }
        // GET: Duals
        public ActionResult Index()
        {
            return View();
        }
        public int CountDuals()
        {
            return _context.Duals.Count();
        }
        public int DualId()
        {
            return _context.Duals.Where(s => s.FirstPlayerSocketId != null).FirstOrDefault().DualId; 
        }
        public bool StartDual(string socketId, int baId)
        {
            Dual tempDual = new Dual();
            tempDual.FirstPlayerBAId = baId;
            tempDual.FirstPlayerSocketId = socketId;
            _context.Duals.Add(tempDual);
            _context.SaveChanges();
            return true;

        }
        public bool JoinDual(string socketId, int baId)
        {
            List<Dual> duals = _context.Duals.Where(s => s.DualId== DualId() && s.SecondPlayerSocketId==null).ToList();
            if (duals.Count > 0)
            {
                _context.Duals.Where(s => s.DualId == DualId()).FirstOrDefault().SecondPlayerSocketId = socketId;
                _context.Duals.Where(s => s.DualId == DualId()).FirstOrDefault().SecondPlayerBAId = baId;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        // GET: Duals/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Duals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Duals/Create
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

        // GET: Duals/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Duals/Edit/5
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

        // GET: Duals/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Duals/Delete/5
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