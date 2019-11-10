using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Commands
{
    public interface Command
    {
        CommandOutcome Execute(string socketId, int posX, int posY, int battleArenaId);

	    void Undo();
	}
}
