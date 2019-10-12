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
                        Size = 8
                        //ShipId=1
                    },

                    new ShipType
                    {
                       // ShipTypeId = 2,
                        Type = "Cruiser",
                        Size = 4
                       // ShipId = 2

                    },

                    new ShipType
                    {
                        //ShipTypeId = 3,
                        Type = "Submarin",
                        Size = 3,
                        //ShipId = 3
                    },

                    new ShipType
                    {
                        //ShipTypeId = 4,
                        Type = "Kukuruznik",
                        Size = 2
                        //ShipId = 4
                    },
                    new ShipType
                    {
                        //ShipTypeId = 5,
                        Type = "Schnicel",
                        Size = 1
                        //ShipId = 5
                    }
                );
                
                context.SaveChanges();
            }
        
    }
}
}
