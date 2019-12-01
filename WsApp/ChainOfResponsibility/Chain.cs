using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.ChainOfResponsibility
{
    public interface Chain
    {
        void setNextChain(Chain nextChain);
        void executeSelection();
    }
}
