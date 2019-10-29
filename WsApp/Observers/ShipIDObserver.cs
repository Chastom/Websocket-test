using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;

namespace WsApp.Observers
{
    public class ShipIDObserver : Observer
    {
        public ShipIDObserver(Ship shipas)
        {
            this.ship = shipas;
            this.ship.attach(this);
        }

        override
        public void update()
        {
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx xxxxxxxxxxx  xxxx Ship ID has changed! New value: " + ship.getState());
        }
    }
}
