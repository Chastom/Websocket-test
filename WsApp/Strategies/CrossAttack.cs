using System;
using System.Collections.Generic;
using WsApp.Models;

namespace WsApp.Strategies
{
    public class CrossAttack : Strategy
    {
        public override List<CellOutcome> Attack(int posx, int posy, List<Cell> cells, List<Ship> ships)
        {
            this.cells = cells;
            this.ships = ships;

            List<CellOutcome> outcomes = new List<CellOutcome>();

            int tempX = 0;
            int tempY = 0;

            for (int i = 0; i < 2; i++)
            {
                //first for loop sets correct starting position for cell coordinates
                if (i == 0)
                {
                    tempX = posx - 2;
                    tempY = posy;
                }
                else
                {
                    tempX = posx;
                    tempY = posy - 2;
                }

                for (int j = 0; j < 5; j++)
                {
                    Cell cell = ReturnCell(tempX, tempY);

                    if (cell == null)
                    {
                        outcomes.Add(new CellOutcome(AttackOutcome.Missed, tempX, tempY));
                    }
                    else if (cell.IsHit != true)
                    {
                        if (cell.IsArmored == true)
                        {
                            cell.IsArmored = false;
                            outcomes.Add((new CellOutcome(AttackOutcome.Armor, tempX, tempY)));
                        }
                        else
                        {
                            cell.IsHit = true;
                            Ship ship = GetShip(cell.ShipId);
                            ship.RemainingTiles--;
                            outcomes.Add(new CellOutcome(AttackOutcome.Hit, tempX, tempY));
                        }
                    }
                    else
                    {
                        outcomes.Add(new CellOutcome(AttackOutcome.Invalid, tempX, tempY));
                    }
                    //first for loop increments X on first iteration, on second - Y (forming an X shape on the battlefield)
                    if (i == 0)
                    {
                        tempX++;
                    }
                    else
                    {
                        tempY++;
                    }

                }
            }
            return outcomes;
        }


        public override string ToString()
        {
            return "[BOMB  ATTACK]";
        }
    }
}
