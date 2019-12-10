using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Interpreter
{
    public class ConcreteExpression : AbstractExpression
    {
        public override string SimpleMessage()
        {
            return "message";
        }

        public override string Undo()
        {
            return "undoCommand";
        }

        public override string BadWord1()
        {
            return "keiksmazodis";
        }
    }
}
