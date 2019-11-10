using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;

namespace WsApp.Strategies
{
    public class BasicAttack : Strategy
    {
        public override List<CellOutcome> Attack(int posx, int posy, List<Cell> cells, List<Ship> ships)
        {
            this.cells = cells;
            this.ships = ships;

            List<CellOutcome> outcomes = new List<CellOutcome>();

            Cell cell = ReturnCell(posx, posy);

            if (cell == null)
            {
                outcomes.Add(new CellOutcome(AttackOutcome.Missed, posx, posy));
                return outcomes;
            }
            else if (cell.IsHit != true)
            {
                if (cell.IsArmored == true)
                {
                    cell.IsArmored = false;
                    outcomes.Add((new CellOutcome(AttackOutcome.Armor, posx, posy)));
                    return outcomes;
                }
                else
                {
                    cell.IsHit = true;
                    Ship ship = GetShip(cell.ShipId);
                    ship.RemainingTiles--;
                    outcomes.Add(new CellOutcome(AttackOutcome.Hit, posx, posy));
                    return outcomes;
                }

            }
            outcomes.Add(new CellOutcome(AttackOutcome.Invalid, posx, posy));
            return outcomes;
        }


        public override string ToString()
        {
            return "[BASIC ATTACK]";
        }
    }
}
