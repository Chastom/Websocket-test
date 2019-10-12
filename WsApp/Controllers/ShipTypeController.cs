using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WsApp.Models;

namespace WsApp.Controllers
{
    public class ShipTypeController : Controller
    {
        private readonly Context _context;

        public ShipTypeController(Context context)
        {
            _context = context;
        }

        // GET: ShipType
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShipTypes.ToListAsync());
        }

        // GET: ShipType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipType = await _context.ShipTypes
                .FirstOrDefaultAsync(m => m.ShipTypeId == id);
            if (shipType == null)
            {
                return NotFound();
            }

            return View(shipType);
        }

        // GET: ShipType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShipType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipTypeId,Type,Size,ShipId")] ShipType shipType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipType);
        }

        // GET: ShipType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipType = await _context.ShipTypes.FindAsync(id);
            if (shipType == null)
            {
                return NotFound();
            }
            return View(shipType);
        }

        // POST: ShipType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipTypeId,Type,Size,ShipId")] ShipType shipType)
        {
            if (id != shipType.ShipTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipTypeExists(shipType.ShipTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shipType);
        }

        // GET: ShipType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipType = await _context.ShipTypes
                .FirstOrDefaultAsync(m => m.ShipTypeId == id);
            if (shipType == null)
            {
                return NotFound();
            }

            return View(shipType);
        }

        // POST: ShipType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipType = await _context.ShipTypes.FindAsync(id);
            _context.ShipTypes.Remove(shipType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipTypeExists(int id)
        {
            return _context.ShipTypes.Any(e => e.ShipTypeId == id);
        }
    }
}
