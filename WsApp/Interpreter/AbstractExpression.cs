using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Interpreter
{
    public abstract class AbstractExpression
    {
        public int Interpret(InterpreterContext context)
        {
            if (context.getOutput().Equals(SimpleMessage()))
            {
                return 0;
            }

            if (context.getOutput().Equals(Undo()))
            {
                return 1;
            }

            if (context.getOutput().Equals(BadWord1()))
            {
                return 2;
            }

            return -1;
        }

        public abstract string SimpleMessage();
        public abstract string Undo();
        public abstract string BadWord1();
    }
}
