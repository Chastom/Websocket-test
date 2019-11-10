using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Strategies
{
    public class CellOutcome
    {
        public AttackOutcome attackOutcome;
        public int posX;
        public int posY;

        public CellOutcome(AttackOutcome attackOutcome, int posX, int posY)
        {
            this.attackOutcome = attackOutcome;
            this.posX = posX;
            this.posY = posY;
        }
    }
}
