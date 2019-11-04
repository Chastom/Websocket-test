using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApp.Models;

namespace WsApp.Decorators
{
    public class ArmorDecorator : CellDecorator
    {
        public ArmorDecorator(Cell selectedCell) : base(selectedCell)
        {
            base.decoratedCell = selectedCell;
        }

        override
        public void AddArmor()
        {
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx Adding armor!!!!xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            base.decoratedCell.IsArmored = true;
        }
    }
}
