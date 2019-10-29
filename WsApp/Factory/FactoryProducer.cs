using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApp.Factory
{
    public class FactoryProducer
    {
        public static AbstractFactory getFactory(bool extraFeatured)
        {
            if (extraFeatured)
            {
                //This is for future releases
                //return ExtraFeaturedFactory();
                return new BattleArenaFactory();
            } else
            {
                return new BattleArenaFactory();
            }
        }
    }
}
