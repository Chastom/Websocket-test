using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Proxy
{
    public class PlayerTurn : TurnCounter
    {
        public string player1Id;
        public string player2Id;
        public string activePlayerId;

        public PlayerTurn(string p1, string p2, string active)
        {
            player1Id = p1;
            player2Id = p2;
            activePlayerId = active;
        }

        public override TurnOutcome ChangeTurn()
        {
            TurnOutcome outcome = new TurnOutcome();

            //changing the current active player's ID with his opponent's ID
            if (activePlayerId == player1Id)
            {
                activePlayerId = player2Id;

                outcome.ActiveId = player2Id;
                outcome.InactiveId = player1Id;
            }
            else
            {
                activePlayerId = player1Id;
                outcome.ActiveId = player1Id;
                outcome.InactiveId = player2Id;
            }
            outcome.CallerTurn = "right";
            outcome.OpponetTurn = "left";
            return outcome;
        }

        public override TurnOutcome GetCurrentTurn()
        {
            string inactive = activePlayerId == player1Id ? player2Id : player1Id;
            TurnOutcome outcome = new TurnOutcome("right", "left", activePlayerId, inactive);
            return outcome;
        }
    }
}
