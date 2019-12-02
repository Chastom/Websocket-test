using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;
using WsApp.Strategies;

namespace WsApp.VisitorPattern
{
    public class AttackChangerVisitor : Visitor
    {
        private List<Cell> cells;
        private List<Ship> ships;

        public AttackChangerVisitor(List<Cell> cells, List<Ship> ships)
        {
            this.cells = cells;
            this.ships = ships;
        }

        public List<CellOutcome> visit(BasicAttack basicAttack)
        {
            List<CellOutcome> outcomes = new List<CellOutcome>();
            //goes through all the cells with 50% chance to hit each one of them
            Random rnd = new Random();
            for (int i=0; i<15; i++)
            {
                for(int j=0; j<15; j++)
                {
                    if (rnd.Next(2) == 0)
                    {
                        outcomes.AddRange(basicAttack.Attack(i, j, cells, ships));
                    }                    
                }
            }
            return outcomes;
        }

        public List<CellOutcome> visit(BombAttack bombAttack)
        {
            throw new NotImplementedException();
        }

        public List<CellOutcome> visit(CrossAttack crossAttack)
        {
            throw new NotImplementedException();
        }

        public List<CellOutcome> visit(LaserAttack laserAttack)
        {
            throw new NotImplementedException();
        }
    }
}
