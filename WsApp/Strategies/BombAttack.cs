using System;
using System.Collections.Generic;
using WsApp.Models;

namespace WsApp.Strategies
{
    public class BombAttack : Strategy
    {
        public override List<CellOutcome> Attack(int posx, int posy, List<Cell> cells, List<Ship> ships)
        {
            this.cells = cells;
            this.ships = ships;

            List<CellOutcome> outcomes = new List<CellOutcome>();

            //getting the first most left top corner cell's coordinates
            //since bomb attack diameter = 3 cells -> start coords =  - 1 from each coord
            int startX = posx - 1;
            int startY = posy - 1;

            /*
             |start |       |       |
             ------------------------
             |      |initial|       |
             ------------------------
             |      |       |       |

            initial = where client clicked
            start   = [startX : startY] coords
            */

            //all cells in radius of 2 are attacked
            for (int i = 0; i < 3; i++)
            {                         
                for (int j = 0; j < 3; j++)
                {
                    int tempY = startY + j;
                    Cell cell = ReturnCell(startX, tempY);

                    if (cell == null)
                    {
                        outcomes.Add(new CellOutcome(AttackOutcome.Missed, startX, tempY));
                    }
                    else if (cell.IsHit != true)
                    {
                        if (cell.IsArmored == true)
                        {
                            cell.IsArmored = false;
                            outcomes.Add((new CellOutcome(AttackOutcome.Armor, startX, tempY)));
                        }
                        else
                        {
                            cell.IsHit = true;
                            Ship ship = GetShip(cell.ShipId);
                            ship.RemainingTiles--;
                            outcomes.Add(new CellOutcome(AttackOutcome.Hit, startX, tempY));
                        }
                    }
                    else
                    {
                        outcomes.Add(new CellOutcome(AttackOutcome.Invalid, startX, tempY));
                    }
                }
                startX++;
            }
            return outcomes;
        }


        public override string ToString()
        {
            return "[BOMB ATTACK]";
        }
    }
}
