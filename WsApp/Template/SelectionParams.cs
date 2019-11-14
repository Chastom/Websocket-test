using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Template
{
    public class SelectionParams
    {
        public string socketId;
        public int posX;
        public int posY;
        public int battleArenaId;

        public SelectionParams(string socketId, int posX, int posY, int battleArenaId)
        {
            this.socketId = socketId;
            this.posX = posX;
            this.posY = posY;
            this.battleArenaId = battleArenaId;
        }
    }
}
