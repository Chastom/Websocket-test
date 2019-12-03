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
        private List<CellOutcome> outcomes;

        public AttackChangerVisitor(List<Cell> cells, List<Ship> ships)
        {
            this.cells = cells;
            this.ships = ships;
            outcomes = new List<CellOutcome>();
        }

        public List<CellOutcome> visit(BasicAttack basicAttack)
        {
            //goes through all the cells with a 50% chance to hit each one of them
            Random rnd = new Random();
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
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
            for (int i = 2; i < 15; i += 5)
            {
                for (int j = 2; j < 15; j += 5)
                {
                    outcomes.AddRange(bombAttack.Attack(i, j, cells, ships));
                }
            }
            return outcomes;
        }

        public List<CellOutcome> visit(CrossAttack crossAttack)
        {
            for (int i = 2; i < 15; i += 5)
            {
                for (int j = 2; j < 15; j += 5)
                {
                    outcomes.AddRange(crossAttack.Attack(i, j, cells, ships));
                }
            }
            return outcomes;
        }

        public List<CellOutcome> visit(LaserAttack laserAttack)
        {
            for (int i = 0; i < 6; i++)
            {
                outcomes.AddRange(laserAttack.Attack(i, 0, cells, ships));
            }
            return outcomes;
        }
    }
}
