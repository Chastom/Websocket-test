using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Commands
{
    public class UndoResult
    {
        public Coordinate coordinate;
        public string activeButton;
        public List<int> removedButtons;

        public UndoResult(Coordinate coordinate, string activeButton, List<bool> buttons)
        {
            this.coordinate = coordinate;
            this.activeButton = activeButton;

            removedButtons = new List<int>();
            for (int i = 0; i < buttons.Count; i++)
            {
                if(buttons[i] == true)
                {
                    removedButtons.Add(i);
                }
            }
        }
    }
}
