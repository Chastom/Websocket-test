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

        public int CountDuels()
        {
            return _context.Duels.Count();
        }

        public int DuelId()
        {
            return _context.Duels.Where(s => s.FirstPlayerSocketId != null).FirstOrDefault().DuelId;
        }

        public Duel CreateDuel()
        {

            if (CountDuels() == 0)
            {
                Duel duel = new Duel();
                _context.Duels.Add(duel);
                _context.SaveChanges();
                return duel;
            }
            else
                return _context.Duels.First();

        }

        public string SetTurn(Duel duel)
        {
            Random rnd = new Random();
            int turnRND = rnd.Next(1, 3);

            if (turnRND == 1)
            {
                duel.PlayerTurnId = duel.FirstPlayerSocketId;
                _context.SaveChanges();
                return duel.PlayerTurnId;

            }
            else
            {
                duel.PlayerTurnId = duel.SecondPlayerSocketId;
                _context.SaveChanges();
                return duel.PlayerTurnId;
            }
        }

        public void ChangeTurns(string socketId)
        {
            Duel duel = _context.Duels.Where(s => s.FirstPlayerSocketId == socketId || s.SecondPlayerSocketId == socketId).FirstOrDefault();
            if (duel.FirstPlayerSocketId == socketId)
            {
                duel.PlayerTurnId = duel.SecondPlayerSocketId;
                _context.SaveChanges();
            }
            else
            {
                duel.PlayerTurnId = duel.FirstPlayerSocketId;
                _context.SaveChanges();
            }
        }

        public bool isPlayerTurn(string socketId)
        {
            Duel duel = _context.Duels.Where(s => s.FirstPlayerSocketId == socketId || s.SecondPlayerSocketId == socketId).FirstOrDefault();
            return duel.PlayerTurnId == socketId;
        }

        public string GetOpponentSocketId(string socketId)
        {
            Duel duel = _context.Duels.Where(s => s.FirstPlayerSocketId == socketId || s.SecondPlayerSocketId == socketId).FirstOrDefault();
            if (duel.FirstPlayerSocketId == socketId)
            {
                return duel.SecondPlayerSocketId;
            }
            else
            {
                return duel.FirstPlayerSocketId;
            }
        }

        public void removeDuel(string socketId)
        {
            Duel duel = _context.Duels.Where(s => s.FirstPlayerSocketId == socketId || s.SecondPlayerSocketId == socketId).FirstOrDefault();
            if (duel != null)
            {
                _context.Remove(duel);
                _context.SaveChanges();
            }
        }

        public void deleteDuels()
        {
            _context.Duels.RemoveRange(_context.Duels);
            _context.SaveChanges();
        }
    }
}