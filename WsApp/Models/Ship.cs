using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Observers;

namespace WsApp.Models
{
    public class Ship
    {
        List<Observer> observers = new List<Observer>();
        public int ShipId { get; set; }
        public string Name { get; set; }
        public int RemainingTiles { get; set; }
        public string SocketId { get; set; }

        public int ShipTypeId { get; set; }
        public int CellId { get; set; }

        //We're observing ship's id
        public int getState()
        {
            return ShipTypeId;
        }

        //This is the method to use to save ship id.
        public void setState(int state)
        {
            this.ShipTypeId = state;
            notifyAllObservers(); 
        }

        public void attach(Observer obs)
        {
            this.observers.Add(obs);
        }

        public void notifyAllObservers()
        {
            foreach (Observer a in this.observers)
            {
                a.update();
            }
        }
    }
}
