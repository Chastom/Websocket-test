using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context>options)
            :base (options)
        { }

        public DbSet<Player> Players { get; set; }
        public DbSet<BattleArena> BattleArenas { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<ShipSelection> ShipSelections { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ShipType> ShipTypes { get; set; }
        public DbSet<Duel> Duels { get; set; }
        public DbSet<ArmorSelection> ArmorSelections { get; set; }
    }
}
