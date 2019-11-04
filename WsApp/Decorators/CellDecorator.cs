using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Interfaces;
using WsApp.Models;

namespace WsApp.Decorators
{
    //This is an abstract decorator class
    public abstract class CellDecorator : ICell
    {
        protected Cell decoratedCell;

        public CellDecorator(Cell selectedCell)
        {
            this.decoratedCell = selectedCell;
        }

        public abstract void AddArmor();
    }
}
