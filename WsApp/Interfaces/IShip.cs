using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Interfaces
{
    public interface IShip
    {
        bool AddShip(int cellId, int shipTypeId, string shipType);
    }
}
