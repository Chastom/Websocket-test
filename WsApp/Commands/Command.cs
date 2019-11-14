using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Template;

namespace WsApp.Commands
{
    public interface Command
    {
        CommandOutcome Execute(SelectionParams param);

        UndoResult Undo(string socketId);
	}
}
