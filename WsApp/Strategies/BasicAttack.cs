using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;

namespace WsApp.Strategies
{
    public class BasicAttack : Strategy
    {
        public BasicAttack(Context context) : base(context)
        {
        }

        public override List<AttackOutcome> AttackCell(int posx, int posy, string socketId)
        {
            List<AttackOutcome> outcomes = new List<AttackOutcome>();

            Cell cell = cellController.ReturnCell(posx, posy, cellController.GetOpponentArenaId(socketId));

            if (cell == null)
            {
                outcomes.Add(AttackOutcome.Missed);
                return outcomes;
            }
            else if (cell.IsHit != true)
            {
                if (cell.IsArmored == true)
                {
                    cell.IsArmored = false;
                    _context.SaveChanges();
                    outcomes.Add(AttackOutcome.Armor);
                    return outcomes;
                }
                else
                {
                    cell.IsHit = true;
                    Ship ship = cellController.GetShip(cell.ShipId);
                    ship.RemainingTiles--;
                    _context.SaveChanges();
                    outcomes.Add(AttackOutcome.Hit);
                    return outcomes;
                }

            }
            outcomes.Add(AttackOutcome.Invalid);
            return outcomes;
        }
    }
}
