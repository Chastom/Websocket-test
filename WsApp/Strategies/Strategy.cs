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
        public Context _context;
        public CellsController cellController;

        public Strategy(Context context)
        {
            cellController = new CellsController(context);
            _context = context;
        }

        public abstract List<AttackOutcome> AttackCell(int posx, int posy, string socketId);

    }
}
