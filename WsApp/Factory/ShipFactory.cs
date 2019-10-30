using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;

namespace WsApp.Factory
{
    public class ShipFactory
    {
        public Ship GetShip(string shipType)
        {
            if (shipType.Equals("NormalShip"))
            {
                Console.WriteLine("Returning a normal ship");
                return new Ship();
            } else if (shipType.Equals("ArmoredShip"))
            {
                //We might need a different ship, like ArmoreShip or something.
                //Also possible to add a second constructor to ship and add armor that way.
                Console.WriteLine("Returning an armored ship");
                return new Ship();
            } else
            {
                //If there's a different ship type still return a shippy.
                //Remove this later
                Console.WriteLine("The programmer is a moron");
                return new Ship();
            }
        }
    }
}
