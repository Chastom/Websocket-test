using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;
using WsApp.Strategies;

namespace WsApp
{

    public class StrategyController
    {
        private Context _context;

        public StrategyController(Context context)
        {
            _context = context;
        }

        public List<AttackOutcome> Attack(int posx, int posy, string socketId)
        {
            Strategy strategy = StrategyHolder.GetPlayerStrategy(socketId);
            List<Ship> ships = GetEnemyShips(GetOpponentSocketId(socketId));
            List<Cell> cells = GetEnemyCells(GetOpponentArenaId(socketId));

            List<AttackOutcome> outcomes = strategy.Attack(posx, posy, cells, ships);
            _context.SaveChanges();
            return outcomes;
        }

        public List<Ship> GetEnemyShips(string socketId)
        {
            return _context.Ships.Where(s => s.SocketId == socketId).ToList();
        }

        public List<Cell> GetEnemyCells(int battleArenaId)
        {
            return _context.Cells.Where(s => s.BattleArenaId == battleArenaId).ToList();
        }

        public int GetOpponentArenaId(string socketId)
        {
            Duel duel = _context.Duels.Where(s => s.FirstPlayerSocketId == socketId || s.SecondPlayerSocketId == socketId).FirstOrDefault();
            if (duel.FirstPlayerSocketId == socketId)
            {
                return duel.SecondPlayerBAId;
            }
            else
            {
                return duel.FirstPlayerBAId;
            }
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
    }
}
