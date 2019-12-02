using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Strategies;

namespace WsApp.VisitorPattern
{
    public interface Visitable
    {
        // Allows the Visitor to pass the object so
        // the right operations occur on the right
        // type of object.
        List<CellOutcome> Accept(Visitor visitor);
    }
}
