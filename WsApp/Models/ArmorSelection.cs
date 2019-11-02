using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class ArmorSelection
    {
        public int ArmorSelectionId { get; set; }

        public string SocketId { get; set; }
        public bool Selected { get; set; }
        public int ArmorSize { get; set; }
    }
}
