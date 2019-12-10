using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.ChainOfResponsibility
{
    public abstract class Handler
    {
        protected Handler successor;

        public void SetSuccessor(Handler successor)
        {
            this.successor = successor;
        }

        public abstract string HandleRequest(string message);
    }
}
