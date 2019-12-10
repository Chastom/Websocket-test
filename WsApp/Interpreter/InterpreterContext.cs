using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Interpreter
{
    public class InterpreterContext
    {
        private string _input;
        private string _output;
        private enum Commands { Undo }

        public InterpreterContext(string input)
        {
            this._input = input;
        }

        public string getOutput()
        {
            switch (_input)
            {
                case "!undo":
                    _output = "undoCommand";
                    return _output;
                case "blet":
                    _output = "keiksmazodis";
                    return _output;
                case "kurva":
                    _output = "keiksmazodis";
                    return _output;
                case "naxui":
                    _output = "keiksmazodis";
                    return _output;
                default:
                    _output = "message";
                    return _output;
            }
        }
    }
}
