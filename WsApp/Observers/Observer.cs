using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;

namespace WsApp.Observers
{
    public abstract class Observer
    {
        public Ship ship;
        public abstract void update();
    }
}
