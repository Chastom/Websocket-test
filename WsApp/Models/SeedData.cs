using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<Context>>()))
            {

                if (context.ShipTypes.Any())
                {
                    return;   // DB has been seeded
                }
                context.ShipTypes.AddRange(
                    new ShipType
                    {
                        //ShipTypeId = 1,
                        Type = "Bombardier",
                        Size = 8,
                        Count = 1
                        //ShipId=1
                    },

                    new ShipType
                    {
                       // ShipTypeId = 2,
                        Type = "Cruiser",
                        Size = 4,
                        Count = 2
                       // ShipId = 2

                    },

                    new ShipType
                    {
                        //ShipTypeId = 3,
                        Type = "Submarin",
                        Size = 3,
                        Count = 1
                        //ShipId = 3
                    },

                    new ShipType
                    {
                        //ShipTypeId = 4,
                        Type = "Kukuruznik",
                        Size = 2,
                        Count = 2
                        //ShipId = 4
                    },
                    new ShipType
                    {
                        //ShipTypeId = 5,
                        Type = "Schnicel",
                        Size = 1,
                        Count = 5
                        //ShipId = 5
                    }
                );
                ////Koordinaciu sukurimas
                //for (int x = 0; x < 15; x++)
                //{
                //    for (int y = 0; y < 15; y++)
                //    {
                //        int posx = x + 1;
                //        int posy = y + 1;
                //        context.Coordinatess.Add(new Coordinates { PosX = posx, PosY = posy });
                        
                //    }
                //    //bbz kodel reik issaugot kas kiekviena eilute kad teisinga tvarka surasytu
                //    context.SaveChanges();
                //}
                context.SaveChanges();
            }
        
    }
}
}
