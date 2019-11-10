using System;
using System.Collections.Generic;
using WsApp.Models;

namespace WsApp.Strategies
{
    public class LaserAttack : Strategy
    {
        public override List<CellOutcome> Attack(int posx, int posy, List<Cell> cells, List<Ship> ships)
        {
            this.cells = cells;
            this.ships = ships;

            List<CellOutcome> outcomes = new List<CellOutcome>();

            //since there are 15 cells in a row, we iterate through all of them
            //and effectively apply attack action to each one of them horizontally

            for (int i = 0; i < 15; i++)
            {
                Cell cell = ReturnCell(posx, i);

                if (cell == null)
                {
                    outcomes.Add(new CellOutcome(AttackOutcome.Missed, posx, i));
                }
                else if (cell.IsHit != true)
                {
                    if (cell.IsArmored == true)
                    {
                        cell.IsArmored = false;
                        outcomes.Add((new CellOutcome(AttackOutcome.Armor, posx, i)));
                    }
                    else
                    {
                        cell.IsHit = true;
                        Ship ship = GetShip(cell.ShipId);
                        ship.RemainingTiles--;
                        outcomes.Add(new CellOutcome(AttackOutcome.Hit, posx, i));
                    }
                }
                else
                {
                    outcomes.Add(new CellOutcome(AttackOutcome.Invalid, posx, i));
                }
            }
            return outcomes;
        }


        public override string ToString()
        {
            return "[LASER ATTACK]";
        }
    }
}
