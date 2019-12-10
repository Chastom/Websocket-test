using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.ChainOfResponsibility
{
    public class CursewordHandler : Handler
    {
        private List<string> curseWords = new List<string> { "blet", "kurva", "naxui", "nx", "nahui", "bl", "cbb", "loxas" };
        public override string HandleRequest(string message)
        {
            bool curseword = false;
            foreach (var word in curseWords)
            {
                if (message.Contains(word))
                {
                    curseword = true;
                }
            }

            if (curseword)
            {
                return "keiksmazodis";
            } else if (successor != null)
            {
                return successor.HandleRequest(message);
            } else
            {
                return "error";
            }
        }
    }
}
