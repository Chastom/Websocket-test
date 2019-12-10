using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.ChainOfResponsibility
{
    public class CommandHandler : Handler
    {
        public override string HandleRequest(string message)
        {
            if (message.Equals("!undo"))
            {
                return "undoCommand";
            } else if (successor != null)
            {
                return successor.HandleRequest(message);
            } else
            {
                return "ERROR!";
            }
        }
    }
}
