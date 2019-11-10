using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Controllers;
using WsApp.Models;

namespace WsApp.Strategies
{
    public abstract class Strategy
    {
        public List<Cell> cells;
        public List<Ship> ships;

        public abstract List<CellOutcome> Attack(int posx, int posy, List<Cell> cells, List<Ship> ships);

        //-----------------------------------------------------------
        //Db context methods implementation to eliminate context dependancy       
      
        public Cell ReturnCell(int posx, int posy)
        {
            foreach(var cell in cells)
            {
                if(cell.PosX == posx && cell.PosY == posy)
                {
                    return cell;
                }
            }
            return null;
        }

        public Ship GetShip(int shipId)
        {
            foreach(var ship in ships)
            {
                if (ship.ShipId.Equals(shipId))
                {
                    return ship;
                }
            }
            return null;
        }

    }
}
