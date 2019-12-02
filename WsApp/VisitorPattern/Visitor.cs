using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Strategies;

namespace WsApp.VisitorPattern
{
    public interface Visitor
    {
        List<CellOutcome> visit(BasicAttack basicAttack);

        List<CellOutcome> visit(BombAttack bombAttack);

        List<CellOutcome> visit(CrossAttack crossAttack);

        List<CellOutcome> visit(LaserAttack laserAttack);
    }
}
