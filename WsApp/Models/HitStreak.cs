using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class HitStreak
    {
        public int HitStreakId { get; set; }

        public string SocketId { get; set; }
        public int Streak { get; set; }
        public int ReachGoal { get; set; }
    }
}
