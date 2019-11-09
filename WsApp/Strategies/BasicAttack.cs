using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;

namespace WsApp.Strategies
{
    public class BasicAttack : Strategy
    {
        public BasicAttack() : base()
        {
        }

        public override List<AttackOutcome> Attack(int posx, int posy, List<Cell> cells, List<Ship> ships)
        {
            this.cells = cells;
            this.ships = ships;

            List<AttackOutcome> outcomes = new List<AttackOutcome>();

            Cell cell = ReturnCell(posx, posy);

            if (cell == null)
            {
                Console.WriteLine("======================================================= MISSED ");
                outcomes.Add(AttackOutcome.Missed);
                return outcomes;
            }
            else if (cell.IsHit != true)
            {
                if (cell.IsArmored == true)
                {
                    Console.WriteLine("======================================================= ARMOR ");
                    cell.IsArmored = false;
                    //_context.SaveChanges();
                    outcomes.Add(AttackOutcome.Armor);
                    return outcomes;
                }
                else
                {
                    cell.IsHit = true;
                    Ship ship = GetShip(cell.ShipId);
                    ship.RemainingTiles--;
                    //_context.SaveChanges();
                    Console.WriteLine("======================================================= HIT ");
                    outcomes.Add(AttackOutcome.Hit);
                    return outcomes;
                }

            }
            Console.WriteLine("======================================================= INVALID ");
            outcomes.Add(AttackOutcome.Invalid);
            return outcomes;
        }


        public override string ToString()
        {
            return "[BASIC ATTACK]";
        }
    }
}
