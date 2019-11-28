using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;
using WsApp.Proxy;

namespace WsApp.Controllers
{
    public class ProxyPlayerTurn : TurnCounter
    {
        private Context _context;
        private PlayerTurn playerTurn;
        private Duel duel;

        public ProxyPlayerTurn(Context context)
        {
            _context = context;
            duel = GetDuel();
        }

        //=====================================
        // controlls access to PlayerTurn class
        public override TurnOutcome ChangeTurn()
        {
            if(playerTurn == null)
            {
                playerTurn = new PlayerTurn(duel.FirstPlayerSocketId, duel.SecondPlayerSocketId, duel.PlayerTurnId);
                return playerTurn.GetCurrentTurn();
            }
            return playerTurn.ChangeTurn();
        }

        public override TurnOutcome GetCurrentTurn()
        {
            if (playerTurn == null)
            {
                playerTurn = new PlayerTurn(duel.FirstPlayerSocketId, duel.SecondPlayerSocketId, duel.PlayerTurnId);
            }
            return playerTurn.GetCurrentTurn();
        }
        //=====================================

        private Duel GetDuel()
        {
            return _context.Duels.FirstOrDefault();
        }
    }
}
